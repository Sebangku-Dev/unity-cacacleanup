using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTable : BaseLevel
{
    [SerializeField] private GameObject[] garbages;
    [SerializeField] private GameObject[] garbageTargets;

    bool initCutleries = true;
    [SerializeField] private GameObject[] cutleries;
    [SerializeField] private GameObject[] cutleriesHint;
    [SerializeField] private GameObject[] cutleryTargets;

    [SerializeField] private GameObject fruitBasket;
    [SerializeField] private GameObject fruitBasketTarget;
    [SerializeField] private GameObject fruitBasketOnTable;
    bool isFruitBasketOnTable = false;

    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            int index = 0;
            int poin = 0;
            foreach (GameObject garbage in garbages)
            {
                if (garbage.transform.position == garbageTargets[index].transform.position)
                {
                    poin += 1;
                }

                index += 1;
            }

            if (poin == 4 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 1)
        {
            if (initCutleries) foreach (GameObject hint in cutleriesHint) hint.SetActive(true);
            initCutleries = false;

            int index = 0;
            int poin = 0;
            foreach (GameObject cutlery in cutleries)
            {
                DragAndDrop dnd = cutlery.GetComponent<DragAndDrop>();
                dnd.enabled = true;

                if (cutlery.transform.position == cutleryTargets[index].transform.position)
                {
                    poin += 1;
                }

                index += 1;
            }

            if (poin == 6 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 2)
        {
            int i = 0;
            foreach (GameObject cutlery in cutleries)
            {
                cutlery.SetActive(false);
                cutleryTargets[i].SetActive(false);
                i += 1;
            }

            if (StayCollision.instance.progress >= 99 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;

                StayCollision.instance.progress = 0;
                StayCollision.isNewActivity = true;
            }
        }

        else if (IndexActivity == 3)
        {
            if (fruitBasket.transform.position == fruitBasketTarget.transform.position && !isActivityNext)
            {
                fruitBasketOnTable.SetActive(true);

                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }
    }
}
