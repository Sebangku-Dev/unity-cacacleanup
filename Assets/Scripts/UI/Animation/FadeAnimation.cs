using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] public float duration = 0.2f;

    private delegate void LeanAnimation();

    private CanvasGroup overlay;
    void Awake()
    {
        overlay = GetComponent<CanvasGroup>();
    }

    public void OnLoad()
    {
        gameObject.SetActive(true);
        overlay.alpha = 0f;
        overlay.LeanAlpha(0.5f, duration);
    }

    public void OnClose()
    {
        StartCoroutine(DelayAnimate(duration, () => overlay.LeanAlpha(0f, duration)));
    }

    private IEnumerator DelayAnimate(float duration, LeanAnimation anim)
    {
        anim?.Invoke(); // anim invocation
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }


}
