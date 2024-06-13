using UnityEngine;
using TMPro;
using System;

public class MarkerController : MonoBehaviour
{
    public GameObject icon;

    public GameObject character;
    [SerializeField] private GameObject notifCanvas;
    private TextMeshProUGUI txtItemName;

    public GameObject itemObject;
    private Item item;

    // Do transition when triggerred
    [Header("Transition when Triggerred")]
    [SerializeField] private Renderer[] targetObjects = new Renderer[2];
    [SerializeField] private float fadeAmount;
    private float originalOpacity = 0.5f;

    void Start()
    {
        BaseLevel.isStory = true;
        if (notifCanvas != null)
        {
            GameObject objTxt = notifCanvas.transform.Find("txtItem").gameObject;
            txtItemName = objTxt.GetComponent<TextMeshProUGUI>();
        }

        // If assigned, then open level
        if (itemObject != null)
        {
            ItemPrefab itemPrefab = itemObject.GetComponent<ItemPrefab>();
            item = itemPrefab.item;

            int index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == item.id);
            if (index != -1) item.isSolved = DataManager.userProfile.savedLevel[index].isSolved;

            if (item.isSolved)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (notifCanvas != null)
            {
                notifCanvas.GetComponent<PopupAnimation>().OnLoad();
                txtItemName.text = item.name;
                DoFade();
            }

            if (character != null)
            {
                ChoosedItem.item = item;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (notifCanvas != null)
            {
                notifCanvas.GetComponent<PopupAnimation>().OnClose();
                ResetFade();
            }
        }
    }

    private void DoFade()
    {
        Color currentColor = targetObjects[0].material.color;
        targetObjects[0].material.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);

        currentColor = targetObjects[1].material.color;
        targetObjects[1].material.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);
    }

    private void ResetFade()
    {
        Color currentColor = targetObjects[0].material.color;
        targetObjects[0].material.color = new Color(currentColor.r, currentColor.g, currentColor.b, originalOpacity);

        currentColor = targetObjects[1].material.color;
        targetObjects[1].material.color = new Color(currentColor.r, currentColor.g, currentColor.b, originalOpacity);
    }
}
