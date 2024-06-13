using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelCarpet1 : BaseLevel
{

    public GameObject[] trash;
    public bool isDestroyTrash = false;
    public GameObject[] dust;
    public GameObject[] bucket;
    public GameObject bubble;
    public GameObject scrollView;
    int step = 0;
    [SerializeField] GameObject[] hintPel;

    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            if (DragAndDrop.instance.count == 6 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                StayCollision.isNewActivity = true;
                DragAndDrop.instance.count = 0;
                DragAndDrop.isNewActivity = true;
            }
        }
        else if (IndexActivity == 1)
        {
            if (DragAndDrop.instance.count == 1 && !isActivityNext && !isDestroyTrash)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                StayCollision.isNewActivity = true;
                DragAndDrop.instance.count = 0;
                DragAndDrop.isNewActivity = true;

                Invoke("DestroyTrash", activityDelay);
                isDestroyTrash = true;
            }
        }
        else if (IndexActivity == 2)
        {
            foreach (GameObject hint in hintPel) hint.SetActive(hint == hintPel[step]);
            
            if (step == 0)
            {
                StayCollision stayCollisionBucket = bucket[0].GetComponent<StayCollision>();

                if (stayCollisionBucket.progress > 99)
                {
                    step += 1;

                    StayCollision.instance.progress = 0;
                    StayCollision.isNewActivity = true;
                }
            }

            if (step == 1)
            {
                bucket[0].SetActive(false);
                bucket[1].SetActive(true);

                dust[0].SetActive(false);
                dust[1].SetActive(true);

                StayCollision stayCollisionDust = dust[1].GetComponent<StayCollision>();

                if (stayCollisionDust.progress > 99 && !isActivityNext)
                {
                    Invoke("NextActivity", activityDelay);
                    isActivityNext = true;
                    StayCollision.instance.progress = 0;
                    StayCollision.isNewActivity = true;
                }
            }
        }

        else if (IndexActivity == 3)
        {
            if (SelectCollision.instance.hasSelected && !isActivityNext)
            {
                scrollView.SetActive(false);
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                SelectCollision.isNewActivity = true;
            }
        }
    }

    void DestroyTrash()
    {
        foreach (GameObject t in trash)
        {
            t.SetActive(false);
            StayCollision.isNewActivity = true;
        }
        isDestroyTrash = false;
    }
}
