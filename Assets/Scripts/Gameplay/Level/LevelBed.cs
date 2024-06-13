using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBed : BaseLevel
{
    [SerializeField] private GameObject[] panel;
    [Header("Toys Placement")]
    [SerializeField] private GameObject[] toys;
    [SerializeField] private GameObject[] toyTargets;
    bool isOpenToysPanel = false;

    [Header("Pillow Placement")]
    [SerializeField] private GameObject[] pillows;
    [SerializeField] private GameObject[] pillowHints;
    [SerializeField] private GameObject[] pillowTargets;
    [SerializeField] private GameObject[] pillowInitialPos;
    [SerializeField] private GameObject blanketOnBed;
    [SerializeField] private GameObject blanketFolded;
    [SerializeField] private GameObject blanketOnBedClean;
    [SerializeField] private GameObject[] debrises;
    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            if (!isOpenToysPanel && !panel[0].activeSelf)
            {
                Invoke("OpenToysPanel", 1.0f);
                isOpenToysPanel = true;
            }

            if (panel[0].activeSelf)
            {
                int index = 0;
                int poin = 0;

                foreach (GameObject toy in toys)
                {
                    DragAndDrop dnd = toy.GetComponent<DragAndDrop>();
                    dnd.enabled = true;

                    if (toy.transform.position == toyTargets[index].transform.position)
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
        }

        else if (IndexActivity == 1)
        {
            if (!isOpenToysPanel && !panel[1].activeSelf)
            {
                Invoke("OpenToysPanel", 1.0f);
                foreach(GameObject hint in pillowHints) hint.SetActive(true);
                isOpenToysPanel = true;
            }

            if (panel[1].activeSelf)
            {
                Button btnBlanket = blanketOnBed.GetComponent<Button>();
                btnBlanket.enabled = true;

                int index = 0;
                int poin = 0;

                foreach (GameObject pillow in pillows)
                {
                    DragAndDrop dnd = pillow.GetComponent<DragAndDrop>();
                    dnd.enabled = true;

                    if (pillow.transform.position == pillowTargets[index].transform.position)
                    {
                        poin += 1;
                    }

                    index += 1;
                }

                if (poin == 2 && !isActivityNext)
                {
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                }
            }
        }

        else if (IndexActivity == 2)
        {
            if (StayCollision.instance.progress >= 99 && StayCollision.instance.otherProgressPoin == 1 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;

                StayCollision.instance.progress = 0;
                StayCollision.instance.otherProgressPoin = 0;
                StayCollision.isNewActivity = true;
            }
        }

        else if (IndexActivity == 3)
        {
            foreach (GameObject debris in debrises)
            {
                debris.SetActive(false);
            }

            if (panel[1].activeSelf)
            {
                int index = 0;
                int poin = 0;

                foreach (GameObject pillow in pillows)
                {
                    DragAndDrop dnd = pillow.GetComponent<DragAndDrop>();
                    dnd.enabled = true;
                    dnd.itemTarget = pillowInitialPos[index];

                    if (pillow.transform.position == pillowInitialPos[index].transform.position)
                    {
                        if (pillow.transform.name == blanketFolded.transform.name) OnLayDownBlanket();
                        poin += 1;
                    }

                    index += 1;
                }

                if (poin == 2 && !isActivityNext)
                {
                    panel[1].SetActive(false);
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                }
            }
        }

        else if (IndexActivity == 4)
        {
            if (StepCollision.instance.step == 3 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                StepCollision.instance.step = 0;
                StepCollision.isNewActivity = true;
            }
        }
    }

    void OpenToysPanel()
    {
        panel[IndexActivity].SetActive(true);
        isOpenToysPanel = false;
    }

    public void OnClickBlanket()
    {
        pillowHints[0].SetActive(false);
        blanketOnBed.SetActive(false);
        blanketFolded.SetActive(true);
    }

    void OnLayDownBlanket()
    {
        blanketOnBedClean.SetActive(true);
        blanketFolded.SetActive(false);
    }
}
