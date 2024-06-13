using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelToileteries : BaseLevel
{
    private bool isOpenPanelDraw = false;
    [SerializeField] private GameObject dirtOnDraw;
    [SerializeField] private GameObject panelDraw;

    [SerializeField] private GameObject[] toileteries;
    [SerializeField] private GameObject[] toileteriesTarget;
    [SerializeField] private Sprite toileteriesContainerWithToothPaste;

    [SerializeField] private GameObject[] toileteriesDraw;
    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            panelDraw.SetActive(isOpenPanelDraw);

            if (panelDraw.activeSelf)
            {
                if (StayCollision.instance.progress > 99 && !isActivityNext)
                {
                    dirtOnDraw.SetActive(false);

                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                    StayCollision.instance.progress = 0;
                    StayCollision.isNewActivity = true;
                }
            }
        }

        else if (IndexActivity == 1)
        {
            int check = 0;
            int index = 0;

            foreach (GameObject item in toileteries)
            {
                check += item.transform.position == toileteriesTarget[index].transform.position ? 1 : 0;
                index++;
            }

            if (toileteries[4].transform.position == toileteriesTarget[4].transform.position)
            {
                Image toioleteriesContainerImage = toileteries[0].GetComponent<Image>();
                toioleteriesContainerImage.sprite = toileteriesContainerWithToothPaste;

                CanvasGroup toothPasteCG = toileteries[4].GetComponent<CanvasGroup>();
                toothPasteCG.alpha = 0.0f;
            }

            if (check == 5 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 2)
        {
            toileteriesDraw[0].SetActive(false);
            toileteriesDraw[1].SetActive(true);

            Invoke("NextActivity", activityDelay);
            isActivityNext = true;
        }
    }

    public void OnClickDraw()
    {
        if (IndexActivity == 0) isOpenPanelDraw = true;
    }
}
