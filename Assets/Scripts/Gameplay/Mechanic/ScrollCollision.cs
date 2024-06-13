using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCollision : MonoBehaviour
{
    public Hint hint;
    public static ScrollCollision instance;
    public static bool isNewActivity = true;

    public GameObject[] cloneContent;
    public CanvasGroup[] contents;
    GameObject clone;
    CanvasGroup content;
    public GameObject parent;
    [SerializeField] private Canvas canvas;
    Vector3 clickPosition;
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
        if (clone != null)
        {
            clickPosition = Input.mousePosition;

            clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            clickPosition.z = transform.position.z;
            clone.transform.position = clickPosition;
        }
    }

    public void CreateClone(int index)
    {
        clickPosition = Input.mousePosition;

        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
        clickPosition.z = transform.position.z;

        clone = Instantiate(cloneContent[index], clickPosition, Quaternion.identity, parent.transform);
        DragAndDrop cloneDragAndDrop = clone.GetComponent<DragAndDrop>();
        cloneDragAndDrop.canvas = canvas;

        content = contents[index];
        content.alpha = 0.2f;
    }

    public void DestroyClone()
    {
        // Destroy(clone);
        content.alpha = 1.0f;
    }
}
