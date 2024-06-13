using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public Hint[] hint;
    public static TriggerObject instance;
    public static bool isNewActivity = true;
    public int poin;

    public delegate void OnTriggerEvent();
    public OnTriggerEvent triggerEvent;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && !isNewActivity)
        {
            return;
        }
        instance = this;
        isNewActivity = false;
    }

    void Update()
    {
        // triggerEvent?.Invoke();
    }
}
