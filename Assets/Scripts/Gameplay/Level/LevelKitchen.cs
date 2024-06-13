using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelKitchen : BaseLevel
{
    [Header("Spice Rack Cleaning Activity")]
    [SerializeField] private GameObject spiceRack;
    bool isOpenPanel = false;
    [SerializeField] private GameObject[] spiceRackPanel;
    [SerializeField] private GameObject[] garbages;
    [SerializeField] private GameObject[] garbageTargets;
    [SerializeField] private GameObject[] flies;
    [SerializeField] private GameObject trashOnRack;

    [Header("Spice Rack Placement Activity")]
    [SerializeField] private GameObject[] spices;
    [SerializeField] private GameObject[] spiceTarget;
    [SerializeField] private GameObject spiceOnRack;
    [SerializeField] private GameObject[] spicesOnTable;

    [Header("Cooking Ware Placement")]
    [SerializeField] GameObject cookingWareHanger;
    [SerializeField] private GameObject[] cookingWares;
    [SerializeField] private GameObject[] cookingWareTargets;
    [SerializeField] private GameObject[] cookingWaresOnHanger;
    [SerializeField] private GameObject[] cookingWaresOnTable;

    // Update is called once per frame
    protected override void Update()
    {
        Button btnSpiceRack = spiceRack.GetComponent<Button>();
        btnSpiceRack.enabled = IndexActivity == 0 || IndexActivity == 1;

        Button btnCookingWareHanger = cookingWareHanger.GetComponent<Button>();
        btnCookingWareHanger.enabled = IndexActivity == 2;

        if (IndexActivity == 0)
        {
            spiceRackPanel[indexActivity].SetActive(isOpenPanel);
            int index = 0;
            int poin = 0;

            foreach (GameObject garbage in garbages)
            {
                if (garbage.transform.position == garbageTargets[index].transform.position)
                {
                    poin += 1;
                    flies[index].SetActive(false);
                }

                index += 1;
            }

            if (poin == 4 && !isActivityNext)
            {
                trashOnRack.SetActive(false);
                isOpenPanel = false;

                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 1)
        {
            spiceRackPanel[indexActivity].SetActive(isOpenPanel);
            int index = 0;
            int poin = 0;

            foreach (GameObject spice in spices)
            {
                if (spice.transform.position == spiceTarget[index].transform.position)
                {
                    poin += 1;
                }

                index += 1;
            }

            if (poin == 10 && !isActivityNext)
            {
                foreach (GameObject spiceOnTable in spicesOnTable)
                {
                    spiceOnTable.SetActive(false);
                }
                spiceOnRack.SetActive(true);
                isOpenPanel = false;

                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 2)
        {
            spiceRackPanel[indexActivity].SetActive(isOpenPanel);
            int index = 0;
            int poin = 0;
            foreach (GameObject cookingWare in cookingWares)
            {
                if (cookingWare.transform.position == cookingWareTargets[index].transform.position)
                {
                    poin += 1;
                }

                index += 1;
            }

            if (poin == 3 && !isActivityNext)
            {
                isOpenPanel = false;
                int i = 0;
                foreach (GameObject cookingWare in cookingWaresOnTable)
                {
                    cookingWare.SetActive(false);
                    cookingWaresOnHanger[i].SetActive(true);
                    i += 1;
                }

                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 3)
        {
            if (StayCollision.instance.progress >= 99 && StayCollision.instance.otherProgressPoin == 3 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;

                StayCollision.instance.progress = 0;
                StayCollision.instance.otherProgressPoin = 0;
                StayCollision.isNewActivity = true;
            }
        }
    }

    public void OnClickToOpenPanel()
    {
        isOpenPanel = true;
    }
}
