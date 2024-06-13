using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTrigger : MonoBehaviour
{
    public Hint hint;
    public int index;
    public string target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == target)
        {
            SelectCollision.instance.selectedItem[index].alpha = 0.4f;
            SelectCollision.instance.index = index;
            SelectCollision.instance.onSelected = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!SelectCollision.instance.hasSelected)
        {
            if (other.gameObject.name == target)
            {
                SelectCollision.instance.selectedItem[index].alpha = 0.0f;
                SelectCollision.instance.onSelected = false;
            }
        }
    }
}
