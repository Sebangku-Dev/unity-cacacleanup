using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpNavigation : MonoBehaviour
{
    public GameObject overlay;
    public GameObject popUpObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPopUp()
    {
        overlay.SetActive(true);
        popUpObject.SetActive(true);
    }

    public void ClosePopUp()
    {
        overlay.SetActive(false);
        popUpObject.SetActive(false);
    }
}
