using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IAnimation
{
    public static float duration = 0.3f;
    private Vector3 startingScale;

    public void OnLoad()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, duration).setEaseOutQuart();
    }

    public void OnClose()
    {
        transform.localScale = Vector3.one;
        int id = LeanTween.scale(gameObject, Vector3.zero, duration).id;
        LTDescr d = LeanTween.descr(id);

        if (d != null)
        {
            d.setOnComplete(() => gameObject.SetActive(false)).setEase(LeanTweenType.easeInOutQuart);
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (!GetComponent<Button>())
        {
            return;
        }
        transform.LeanScale(new Vector3(1.1f, 1.1f, 1.1f), duration).setEaseOutQuart();
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData e)
    {
        if (!GetComponent<Button>())
        {
            return;
        }

        transform.LeanScale(Vector3.one, duration).setEaseOutQuart();
    }
}
