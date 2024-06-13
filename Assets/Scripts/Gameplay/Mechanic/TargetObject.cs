using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public Hint hint;
    public static TargetObject instance;
    public static bool isnewActivity = true;
    public string triggerObject;
    bool hasEnter = false;

    public CanvasGroup thisItemCanvasGroup;
    public CanvasGroup triggerItemCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (instance != null && !isnewActivity)
        {
            return;
        }
        instance = this;
        isnewActivity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == triggerObject && !hasEnter)
        {
            DragAndDrop.instance.count += 1;
            hasEnter = true;

            if (thisItemCanvasGroup != null)
            {
                thisItemCanvasGroup.alpha = 1f;
                triggerItemCanvasGroup.alpha = 0f;
            }
        }
    }
}
