using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepCollision : MonoBehaviour
{
    public Hint hint;
    public static StepCollision instance;
    public static bool isNewActivity = true;
    public Image progressBar;
    public GameObject effect;
    public GameObject effectParent;
    public int step;
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
}
