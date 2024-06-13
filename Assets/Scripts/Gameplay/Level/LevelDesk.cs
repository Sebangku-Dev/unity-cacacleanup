using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesk : BaseLevel
{
    [Header("TurnOff Study Lamp")]
    [SerializeField] private GameObject studyLampLight;
    [SerializeField] private Button btnStudyLamp;
    [SerializeField] private GameObject hintLamp;
    bool isStudyLampTurnOn = true;

    [Header("Collect Garbages")]
    [SerializeField] private GameObject[] garbages;
    [SerializeField] private GameObject[] garbageTargets;
    [SerializeField] private GameObject garbagesOnDesk;

    [Header("Stationaries Placement")]
    bool initStationery = true;
    [SerializeField] private GameObject[] stationeries;
    [SerializeField] private GameObject[] stationeryHints;
    [SerializeField] private GameObject[] stationeryTargets;
    // Update is called once per frame
    protected override void Update()
    {
        foreach (GameObject item in stationeries)
        {
            DragAndDrop dnd = item.GetComponent<DragAndDrop>();
            dnd.enabled = IndexActivity == 2;
        }

        if (IndexActivity == 0)
        {
            btnStudyLamp.enabled = true;

            if (!isStudyLampTurnOn && !isActivityNext)
            {
                studyLampLight.SetActive(false);
                btnStudyLamp.enabled = false;

                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 1)
        {
            garbagesOnDesk.SetActive(false);

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

            if (poin == 3 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 2)
        {
            if (initStationery) foreach (GameObject hint in stationeryHints) hint.SetActive(true);
            initStationery = false;

            int index = 0;
            int poin = 0;

            foreach (GameObject stationery in stationeries)
            {
                if (stationery.transform.position == stationeryTargets[index].transform.position)
                {
                    poin += 1;
                }

                index += 1;
            }

            if (poin == 6 && !isActivityNext)
            {
                foreach (GameObject hint in stationeryHints) hint.SetActive(false);
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 3)
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
    }

    public void OnClickStudyLampButton()
    {
        hintLamp.SetActive(false);
        isStudyLampTurnOn = false;
    }
}
