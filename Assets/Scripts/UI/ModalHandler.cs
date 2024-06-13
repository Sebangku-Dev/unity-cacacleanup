using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ModalHandler : MonoBehaviour
{
    [SerializeField] string modalName;
    [SerializeField] UnityEvent OnModalActivate;
    [SerializeField] UnityEvent OnModalDeactivate;


    public void ActivateModal(string message = "")
    {
        OnModalActivate?.Invoke();

    }

    public void DeactivateModal()
    {
        OnModalDeactivate?.Invoke();
    }
}