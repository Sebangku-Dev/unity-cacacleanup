using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSinker : BaseLevel
{
    [Header("Cutleries Spawn")]
    [SerializeField] private GameObject[] cutleries;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject hand;

    GameObject activeCutlery;
    GameObject dust;
    BoxCollider2D cDust;
    GameObject bubble;
    BoxCollider2D cBubble;
    GameObject wet;
    BoxCollider2D cWet;
    bool isNewActiveCutlery = true;
    bool isFinishCleaning = false;
    int step = 0;

    [Header("Sinker Tools")]
    [SerializeField] private GameObject sponge;
    [SerializeField] private GameObject shampoo;
    BoxCollider2D cShampoo;
    [SerializeField] private Button btnTap;
    [SerializeField] private Image waterFlow;
    [SerializeField] private Image waterFloat;
    bool isWaterFlow = false;
    bool isWaterFloat = false;
    bool endWaterFloat = false;

    [SerializeField] GameObject[] sinkerHints;

    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity >= 0 && IndexActivity < cutleries.Length)
        {
            // Debug.Log(step + " - " + IndexActivity);
            btnTap.enabled = step == 2;

            if (isNewActiveCutlery && IndexActivity < cutleries.Length - 1)
            {
                Invoke("OnSpawnCutlery", .8f);
                isNewActiveCutlery = false;
            }

            if (step < 4) foreach (GameObject hint in sinkerHints) hint.SetActive(indexActivity == 0 && hint == sinkerHints[step]);

            if (step == 0)
            {
                if (cShampoo) cShampoo.enabled = true;

                if (TriggerObject.instance.poin == 1)
                {
                    TriggerObject.instance.poin = 0;
                    TriggerObject.isNewActivity = true;
                    step = 1;
                    cShampoo.enabled = false;
                }
            }

            else if (step == 1)
            {
                cDust.enabled = true;
                cBubble.enabled = true;

                if (StayCollision.instance.progress >= 99 && StayCollision.instance.otherProgressPoin == 1)
                {
                    StayCollision.instance.progress = 0;
                    StayCollision.instance.otherProgressPoin = 0;
                    StayCollision.isNewActivity = true;

                    step = 2;

                    cDust.enabled = false;
                    cBubble.enabled = false;
                }
            }

            else if (step == 2)
            {
                if (isWaterFlow)
                {
                    StartCoroutine(FillImageOverTime(waterFlow, 0.0f, 1.0f, 1.0f, 0));
                }

                else if (!isWaterFlow && isWaterFloat)
                {
                    StartCoroutine(FillImageOverTime(waterFloat, 0.0f, 1.0f, 1.0f, 1));
                }

                else if (!isWaterFlow && !isWaterFloat && endWaterFloat)
                {
                    Invoke("ResetWaterFlow", 1.0f);
                    endWaterFloat = false;
                }
            }

            else if (step == 3)
            {
                cWet.enabled = true;

                if (StayCollision.instance.progress >= 99)
                {
                    StayCollision.instance.progress = 0;
                    StayCollision.isNewActivity = true;

                    step = 4;

                    cWet.enabled = false;
                }
            }

            else if (step == 4 && !isFinishCleaning && !isActivityNext)
            {
                hand.SetActive(false);

                Invoke("NextActivity", activityDelay);
                Invoke("FinishCleaningCutlery", 0.8f);

                isFinishCleaning = true;
                isActivityNext = true;
            }
        }
    }

    public void OnClickTap()
    {
        isWaterFlow = true;
    }

    void OnSpawnCutlery()
    {
        hand.SetActive(true);

        Vector3 position = spawnPoint.position;
        activeCutlery = Instantiate(cutleries[IndexActivity], position, Quaternion.identity, spawnParent);

        cShampoo = shampoo.GetComponent<BoxCollider2D>();

        dust = activeCutlery.transform.Find("dust").gameObject;
        cDust = dust.GetComponent<BoxCollider2D>();

        bubble = activeCutlery.transform.Find("bubble").gameObject;
        cBubble = bubble.GetComponent<BoxCollider2D>();

        wet = activeCutlery.transform.Find("wet").gameObject;
        cWet = wet.GetComponent<BoxCollider2D>();

        step = 0;
    }

    void FinishCleaningCutlery()
    {
        GameObject spongeBubble = sponge.transform.Find("bubble").gameObject;
        spongeBubble?.SetActive(false);

        Destroy(activeCutlery);
        step += 1;

        isWaterFlow = false;
        isWaterFloat = false;
        endWaterFloat = false;

        isNewActiveCutlery = true;
        isFinishCleaning = false;
    }

    void ResetWaterFlow()
    {
        waterFlow.fillAmount = 0.0f;
        waterFloat.fillAmount = 0.0f;

        dust.SetActive(false);
        bubble.SetActive(false);

        wet.SetActive(true);

        step = 3;
    }

    IEnumerator FillImageOverTime(Image fillImage, float startFillAmount, float endFillAmount, float duration, int step)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newFillAmount = Mathf.Lerp(startFillAmount, endFillAmount, elapsedTime / duration);

            fillImage.fillAmount = newFillAmount;

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        fillImage.fillAmount = endFillAmount;

        isWaterFlow = step < 0;
        isWaterFloat = step < 1;
        endWaterFloat = step == 1;
    }
}
