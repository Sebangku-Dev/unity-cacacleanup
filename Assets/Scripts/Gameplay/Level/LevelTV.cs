using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTV : BaseLevel
{
    public GameObject[] trashes;
    public GameObject[] trashTarget;
    public DragAndDrop[] itemObject;
    bool initBooks = true;
    public GameObject[] books;
    public GameObject[] booksHint;
    public GameObject[] bookTarget;
    public GameObject image;
    public CanvasGroup[] spiderWebs;
    // Update is called once per frame
    protected override void Update()
    {
        if (IndexActivity == 0)
        {
            int check = 0;
            int index = 0;
            foreach (GameObject trash in trashes)
            {
                check += trash.transform.position == trashTarget[index].transform.position ? 1 : 0;
                index += 1;
            }

            if (check == 4 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 1)
        {
            foreach (DragAndDrop dnd in itemObject)
            {
                dnd.enabled = true;
            }

            if (initBooks) foreach (GameObject hint in booksHint) hint.SetActive(true);
            initBooks = false;

            int check = 0;
            int index = 0;
            foreach (GameObject book in books)
            {
                check += book.transform.position == bookTarget[index].transform.position ? 1 : 0;
                index += 1;
            }

            if (check == 5 && Mathf.Abs(image.transform.rotation.eulerAngles.z) < 0.01f && !isActivityNext)
            {
                foreach (GameObject hint in booksHint)
                {
                    hint.SetActive(false);
                }
                DragRotation.isNewActivity = true;
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }

        else if (IndexActivity == 2)
        {
            int check = 0;
            int index = 0;
            foreach (CanvasGroup web in spiderWebs)
            {
                check += web.alpha < 0.1f ? 1 : 0;
                index += 1;
            }

            if (check == 3 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
            }
        }
    }
}
