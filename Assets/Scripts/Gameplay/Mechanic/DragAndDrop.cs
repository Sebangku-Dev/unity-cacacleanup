using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Hint[] hints;
    public static DragAndDrop instance;
    public static bool isNewActivity = true;
    public int count;

    private RectTransform rectTransform;
    public Canvas canvas;
    private RectTransform rectCanvas;
    private CanvasGroup canvasGroup;

    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public GameObject item;
    public GameObject itemTarget;
    public int minDistance;

    public bool isResetPosition;
    public bool isHasTarget;
    public bool isChangeRotate;
    public bool isChangeScale;

    Vector3 initialPosition;

    BoxCollider2D colliderObject;

    void Start()
    {
        colliderObject = gameObject.GetComponent<BoxCollider2D>();
        if (colliderObject != null) colliderObject.isTrigger = false;
        
        if (isHasTarget || isResetPosition)
        {
            initialPosition = transform.position;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvas != null) rectCanvas = canvas.GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();

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
        if (canvas != null) rectCanvas = canvas.GetComponent<RectTransform>();
        if (rectCanvas != null) rectTransform.anchoredPosition = ClampToCanvas(rectTransform.anchoredPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isHasTarget)
        {
            parentAfterDrag = transform.parent;
            image.raycastTarget = false;
        }

        if (colliderObject != null) colliderObject.isTrigger = true;
        // canvasGroup.alpha = .6f;
        // canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (hints != null) foreach (Hint hint in hints) hint.hintCanvasGroup.alpha = 0.0f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float distance = Vector3.Distance(item.transform.localPosition, itemTarget.transform.localPosition);

        if (isHasTarget)
        {

            if (distance < minDistance)
            {
                item.transform.localPosition = itemTarget.transform.localPosition;
                transform.SetParent(parentAfterDrag);

                if (isChangeRotate)
                {
                    item.transform.rotation = Quaternion.Euler(0f, 0f, itemTarget.transform.rotation.eulerAngles.z);
                }

                if (isChangeScale)
                {
                    RectTransform rectItem = item.GetComponent<RectTransform>();
                    RectTransform rectTarget = itemTarget.GetComponent<RectTransform>();

                    rectItem.localScale = rectTarget.localScale;
                }
            }

            else
            {
                transform.position = initialPosition;
                if (hints != null) foreach (Hint hint in hints) hint.hintCanvasGroup.alpha = 1.0f;
            }
        }
        else if (isResetPosition)
        {
            transform.position = initialPosition;
            if (hints != null) foreach (Hint hint in hints) hint.hintCanvasGroup.alpha = 1.0f;
        }

        image.raycastTarget = true;
        // canvasGroup.alpha = 1f;
        // canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (colliderObject != null) colliderObject.isTrigger = false;
    }

    private Vector2 ClampToCanvas(Vector2 position)
    {
        // Hitung batas-batas di mana objek bisa berada
        float minX = (rectCanvas.rect.width * -0.5f) + (rectTransform.rect.width * rectTransform.pivot.x);
        float maxX = (rectCanvas.rect.width * 0.5f) - (rectTransform.rect.width * (1 - rectTransform.pivot.x));

        // Perbaikan: Menggunakan pivot.y untuk menghitung minY dan maxY
        float minY = (rectCanvas.rect.height * -0.5f) + (rectTransform.rect.height * rectTransform.pivot.y);
        float maxY = (rectCanvas.rect.height * 0.5f) - (rectTransform.rect.height * (1 - rectTransform.pivot.y));

        // Batasi posisi objek agar tidak keluar dari Canvas
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(position.x, minX, maxX),
            Mathf.Clamp(position.y, minY, maxY)
        );

        return clampedPosition;
    }
}
