using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSofa : BaseLevel
{
    public static LevelSofa instance;

    public CanvasGroup tornCanvas;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (IndexActivity == 0)
        {
            if (MagnetCollision.instance.indexAnchor == 6 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                MagnetCollision.instance.indexAnchor = 0;
            }
        }

        else if (IndexActivity == 1)
        {
            if (StepCollision.instance.step == 4 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                StepCollision.instance.step = 0;
                StepCollision.isNewActivity = true;
            }
        }

        else if (IndexActivity == 2)
        {
            tornCanvas.alpha = 0f;
            if (StayCollision.instance.progress > 99 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                StayCollision.instance.progress = 0;
                StayCollision.isNewActivity = true;
            }
        }

        else if (IndexActivity == 3)
        {
            if (StepCollision.instance.step == 6 && !isActivityNext)
            {
                Invoke("NextActivity", activityDelay);
                isActivityNext = true;
                StepCollision.instance.step = 0;
                StepCollision.isNewActivity = true;
            }
        }
    }
}
