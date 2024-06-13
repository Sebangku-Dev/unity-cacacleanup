using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayCollision : MonoBehaviour
{
    public Hint hint;
    public static StayCollision instance;
    public static bool isNewActivity = true;
    public bool isOtherProgress = false;
    public int otherProgressPoin = 0;
    private bool thisProgressIsDone = false;
    public string triggerObjectName;
    public CanvasGroup progressObject;
    public GameObject progressTarget;
    public bool isIncrease = false;
    public int progress;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if ((instance != null && !isNewActivity) || isOtherProgress)
        {
            return;
        }
        instance = this;
        isNewActivity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == triggerObjectName)
        {
            if (progressObject != null)
            {
                if (progress <= 99)
                {
                    progressObject.alpha += isIncrease ? 0.01f : -0.01f;
                    progress += 1;
                }
                else if (progress >= 99 && !thisProgressIsDone && isOtherProgress)
                {
                    instance.otherProgressPoin += 1;
                    thisProgressIsDone = true;
                }
            }

            if (progressTarget != null)
            {
                Vector3 direction = progressTarget.transform.position - transform.position;

                direction.Normalize();

                transform.position += direction * 1.0f * Time.deltaTime;
            }

        }
    }
}
