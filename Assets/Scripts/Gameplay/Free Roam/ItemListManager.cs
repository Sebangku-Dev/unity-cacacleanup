using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemListManager : MonoBehaviour
{
    [SerializeField] GameObject hologram;
    [SerializeField] GameObject itemListCanvas;
    [SerializeField] ListOfItem listOfItem;
    [SerializeField] GameObject itemListContent;
    [SerializeField] GameObject itemCardPrefab;
    [SerializeField] Sprite locked;
    [SerializeField] GameObject itemSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (listOfItem != null)
        {
            foreach (ItemPrefab item in listOfItem.listOfItem)
            {
                GameObject itemCard = Instantiate(itemCardPrefab, itemListContent.transform);
                ItemCard newItem = itemCard.GetComponent<ItemCard>();


                if (newItem != null)
                {
                    newItem.item = item.item;
                    newItem.itemSpawnPoint = itemSpawnPoint;
                    newItem.itemListCanvas = itemListCanvas;
                    newItem.hologram = hologram;

                    GameObject objectText = itemCard.transform.Find("Text Item Name").gameObject;
                    TextMeshProUGUI textItemName = objectText?.GetComponent<TextMeshProUGUI>();
                    textItemName.text = newItem.item.name;

                    GameObject objectImage = itemCard.transform.Find("Image Item").gameObject;
                    Button btnItem = itemCard.GetComponent<Button>();

                    if (DataManager.userProfile != null)
                    {
                        int index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == newItem.item.id);
                        if (index != -1) newItem.item.isSolved = DataManager.userProfile.savedLevel[index].isSolved;
                    }

                    objectImage.SetActive(newItem.item.isSolved);

                    Image imgItem =newItem.item.isSolved ? objectImage?.GetComponent<Image>() : itemCard.GetComponent<Image>();
                    imgItem.sprite = newItem.item.isSolved ? newItem.item.image ?? null : locked;

                    btnItem.enabled = newItem.item.isSolved;
                }
            }
        }
    }

    public void ToggleItemCanvas()
    {

    }
}
