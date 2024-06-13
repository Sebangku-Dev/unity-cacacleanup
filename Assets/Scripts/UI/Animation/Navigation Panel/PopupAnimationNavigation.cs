using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupAnimationNavigation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float duration = 0.2f;

    private void Start()
    {
        
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
