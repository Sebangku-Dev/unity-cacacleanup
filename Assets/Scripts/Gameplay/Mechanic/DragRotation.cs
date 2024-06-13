using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRotation : MonoBehaviour, IDragHandler
{
    public Hint[] hints;
    public static DragRotation instance;
    public static bool isNewActivity = true;
    public float rotationSpeed = 1f;
    public float targetRotation = 0f;
    public bool hasTarget = false;
    public float totalRotations = 0f;
    public void Awake()
    {
        if (instance == null && !isNewActivity)
        {
            return;
        }
        instance = this;
        isNewActivity = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        float delta = eventData.delta.x;

        float newRotationZ = transform.rotation.eulerAngles.z - delta * rotationSpeed;

        totalRotations += hasTarget ? Mathf.Abs(delta * rotationSpeed) / 360f : 0f;

        newRotationZ = hasTarget ? Mathf.Repeat(newRotationZ, 360f) : Mathf.Clamp(newRotationZ, targetRotation, 360f);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotationZ);
        if (hints != null) foreach (Hint hint in hints) hint.hintCanvasGroup.alpha = 0.0f;
    }
}
