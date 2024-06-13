using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCollision : MonoBehaviour
{
    public Hint hint;
    public static SelectCollision instance;
    public static bool isNewActivity = true;
    public bool onSelected;
    public bool hasSelected = false;
    public int index;
    public CanvasGroup[] selectedItem;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (instance != null && !isNewActivity)
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

    public void setSelected()
    {
        if (onSelected)
        {
            selectedItem[index].alpha = 1.0f;
            hasSelected = true;
        }
    }
}
