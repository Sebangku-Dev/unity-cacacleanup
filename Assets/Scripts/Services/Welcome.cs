using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    [SerializeField] GameObject welcome;
    [SerializeField] GameObject register;

    public static event Action OnRegisterButtonClick;
    public static event Action OnBackButtonClick;

    public void ActivateRegisterPanel()
    {
        if (welcome.activeSelf)
        {
            OnRegisterButtonClick?.Invoke();

            welcome.SetActive(false);
            register.SetActive(true);
        }
    }

    public void ActivateWelcomePanel()
    {
        if (register.activeSelf)
        {
            OnBackButtonClick?.Invoke();

            welcome.SetActive(true);
            register.SetActive(false);
        }
    }
}
