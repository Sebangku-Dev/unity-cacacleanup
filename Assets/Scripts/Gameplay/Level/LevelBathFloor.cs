using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelBathFloor : BaseLevel
{
    [SerializeField] private Button btnTap;
    [SerializeField] private GameObject hintTap;
    [SerializeField] private GameObject tapWaterFlow;
    [SerializeField] private GameObject waterPuddle;
    [SerializeField] private GameObject floorBubble;
    [SerializeField] private GameObject waterDrain;

    bool isOnClickTap = false;
    int step = 0;
    bool isStartWaterFlow = true;
    bool isStartWaterPuddle = true;
    bool isResetWaterPuddle = false;

    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            if (TriggerObject.instance.poin == 3 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;

                TriggerObject.instance.poin = 0;
                TriggerObject.isNewActivity = true;
            }
        }

        else if (IndexActivity == 1)
        {
            if (StayCollision.instance.progress >= 99 && StayCollision.instance.otherProgressPoin == 4 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;

                StayCollision.instance.progress = 0;
                StayCollision.instance.otherProgressPoin = 0;
                StayCollision.isNewActivity = true;
            }
        }

        else if (IndexActivity == 2)
        {
            btnTap.enabled = true;
            hintTap.SetActive(true);

            if (isOnClickTap)
            {
                if (step == 0 && isStartWaterFlow)
                {
                    Image imgTapWaterFlow = tapWaterFlow.GetComponent<Image>();
                    StartCoroutine(FillImageOverTime(imgTapWaterFlow, 0f, 1.0f, 1.0f, 0));
                    step += 1;
                }
                if (step == 1 && isStartWaterPuddle && !isStartWaterFlow)
                {
                    Image imgWaterPuddle = waterPuddle.GetComponent<Image>();
                    StartCoroutine(FillImageOverTime(imgWaterPuddle, 0f, 1.0f, 1.0f, 1));
                    step += 1;
                }
                if (step == 2 && !isStartWaterFlow && !isStartWaterPuddle)
                {
                    Invoke("ResetWaterFlow", 1.1f);
                    RectTransform rectFloorBubble = floorBubble.GetComponent<RectTransform>();
                    RectTransform rectWaterPuddle = waterPuddle.GetComponent<RectTransform>();
                    RectTransform rectWaterDrain = waterDrain.GetComponent<RectTransform>();

                    StartCoroutine(TransformOverTime(rectWaterPuddle, rectWaterDrain, 2.2f));
                    StartCoroutine(TransformOverTime(rectFloorBubble, rectWaterDrain, 2.0f));

                    step += 1;
                }

                if (step == 3 && !isResetWaterPuddle)
                {
                    Invoke("ResetWaterPuddle", 2.5f);
                    isResetWaterPuddle = true;
                }

                if (step == 4 && !isActivityNext)
                {
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                }
            }
        }
    }

    public void OnClickTap()
    {
        if (IndexActivity == 2) isOnClickTap = true;
    }

    void ResetWaterFlow()
    {
        Image imgTapWaterFlow = tapWaterFlow.GetComponent<Image>();
        imgTapWaterFlow.fillAmount = 0.0f;
    }

    void ResetWaterPuddle()
    {
        CanvasGroup cgWaterPuddle = waterPuddle.GetComponent<CanvasGroup>();
        cgWaterPuddle.alpha = 0f;

        CanvasGroup cgFloorBubble = floorBubble.GetComponent<CanvasGroup>();
        cgFloorBubble.alpha = 0f;

        step += 1;
        isResetWaterPuddle = false;
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

        isStartWaterFlow = step < 0;
        isStartWaterPuddle = step < 1;
    }

    IEnumerator TransformOverTime(RectTransform objectToTransform, RectTransform targetObject, float duration)
    {
        Vector2 initialPosition = objectToTransform.anchoredPosition;
        Vector2 targetPosition = targetObject.anchoredPosition;
        Vector2 initialSize = objectToTransform.sizeDelta;
        Vector2 targetSize = targetObject.sizeDelta;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            objectToTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);
            objectToTransform.sizeDelta = Vector2.Lerp(initialSize, targetSize, t);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        objectToTransform.anchoredPosition = targetPosition;
        objectToTransform.sizeDelta = targetSize;
    }
}
