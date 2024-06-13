using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBathHub : BaseLevel
{
    [SerializeField] private GameObject hint;
    [SerializeField] private GameObject panelTub;
    bool isOpenPanelTub = false;
    bool isWaterFlow = false;

    bool startWaterFlow = true;
    bool startFadePuddle = true;
    int step = 0;

    [SerializeField] private GameObject closedTub;
    [SerializeField] private Image waterFlow;
    [SerializeField] private CanvasGroup puddle;
    [SerializeField] private Image waterFill;
    [SerializeField] private GameObject bathDirt;
    [SerializeField] private GameObject bathBrushOverlay;

    bool isOnClickTap = false;
    [SerializeField] private GameObject hintTap;
    [SerializeField] private Image tapWaterFlow;
    [SerializeField] private GameObject bathBubble;
    CanvasGroup bathBubbleCG;

    [SerializeField] private Transform bathHole;
    [SerializeField] private Transform draggedCloseBath;
    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            hint.SetActive(isOpenPanelTub == false);
            panelTub.SetActive(isOpenPanelTub == true);

            if (isOpenPanelTub)
            {
                if (!isWaterFlow)
                {
                    if (DragRotation.instance.totalRotations > 3.0f)
                    {
                        DragRotation.instance.totalRotations = 0;
                        DragRotation.isNewActivity = true;

                        isWaterFlow = true;
                    }
                }
                if (isWaterFlow)
                {
                    GameObject panel = panelTub.transform.Find("panel").gameObject;
                    GameObject closedTub = panel.transform.Find("Closed Tub").gameObject;
                    closedTub.SetActive(false);
                    GameObject hint = panel.transform.Find("hint").gameObject;
                    hint.SetActive(false);
                    GameObject waterFlow = panel.transform.Find("Water Flow").gameObject;
                    Image imagePanel = waterFlow.GetComponent<Image>();

                    if (startWaterFlow)
                    {
                        StartCoroutine(FillImageOverTime(imagePanel, 0f, 1f, 1f));
                    }

                    if (!startWaterFlow && !isActivityNext)
                    {
                        Invoke("NextActivity", activityDelay);
                        isActivityNext = true;
                    }

                }
            }
        }

        else if (IndexActivity == 1)
        {
            closedTub.SetActive(false);

            if (step == 0)
            {
                StartCoroutine(FillImageOverTime(waterFlow, 0f, 1f, 1f));
                StartCoroutine(FillImageOverTime(waterFill, 1f, 0f, 2f));
                StartCoroutine(FadeCanvasGroup(puddle, 0f, 1f, 2f));
                step = 1;
            }
            else if (!startFadePuddle && !startWaterFlow && step == 1)
            {
                Invoke("ResetWaterFlow", 1f);
                bathBrushOverlay.SetActive(true);
                step = 2;
            }
            else if (step == 2)
            {
                if (StayCollision.instance.progress > 99 && !isActivityNext)
                {
                    isWaterFlow = true;
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                    StayCollision.instance.progress = 0;
                    StayCollision.isNewActivity = true;
                }
            }
        }
        else if (IndexActivity == 2)
        {
            bathDirt.SetActive(false);
            bathBubble.SetActive(true);
            bathBubbleCG = bathBubble.GetComponent<CanvasGroup>();

            hintTap.SetActive(true);

            if (!isOnClickTap)
            {
                step = 0;
                startFadePuddle = true;
                startWaterFlow = true;
            }

            if (isWaterFlow && isOnClickTap)
            {
                if (step == 0)
                {
                    StartCoroutine(FillImageOverTime(tapWaterFlow, 0f, 1f, 1f));
                    step = 1;
                }
                else if(step==1){
                    StartCoroutine(FillImageOverTime(waterFlow, 0f, 1f, 1f));
                    StartCoroutine(FadeCanvasGroup(bathBubbleCG, 1f, 0f, 2f));
                    StartCoroutine(FadeCanvasGroup(puddle, 0f, 1f, 2f));
                    step = 2;
                }
                else if (!startFadePuddle && !startWaterFlow && step == 2)
                {
                    Invoke("ResetWaterFlow", 1.5f);
                    step = 3;
                }
                else if (step == 3 && !isActivityNext)
                {
                    isOnClickTap = false;
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                }
            }
        }
        else if (IndexActivity == 3)
        {
            if (draggedCloseBath.position == bathHole.position && !isActivityNext)
            {
                closedTub.SetActive(true);
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }
        else if (IndexActivity == 4)
        {
            hintTap.SetActive(true);
            if (!isOnClickTap)
            {
                step = 0;
                startFadePuddle = true;
                startWaterFlow = true;
            }
            else if (isOnClickTap)
            {
                if (step == 0)
                {
                    StartCoroutine(FillImageOverTime(tapWaterFlow, 0f, 1f, 1f));
                    step = 1;
                }
                else if (step == 1)
                {
                    StartCoroutine(FillImageOverTime(waterFill, 0f, 1f, 2f));
                    step = 2;
                }
                else if (!startWaterFlow && step == 2)
                {
                    Invoke("ResetWaterFlow", 1f);
                    step = 3;
                }
                else if (step == 3 && !isActivityNext)
                {
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                }
            }
        }
    }

    void ResetWaterFlow()
    {
        waterFlow.fillAmount = 0f;
        tapWaterFlow.fillAmount = 0f;
        puddle.alpha = 0f;
    }

    public void OnClickHintTub()
    {
        if (IndexActivity == 0) isOpenPanelTub = true;
    }

    public void OnClickTap()
    {
        hintTap.SetActive(false);
        if (IndexActivity == 2 || IndexActivity == 4) isOnClickTap = true;
    }

    IEnumerator FillImageOverTime(Image fillImage, float startFillAmount, float endFillAmount, float duration)
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
        startWaterFlow = false;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;

            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);

            canvasGroup.alpha = alpha;

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        startFadePuddle = false;
    }
}
