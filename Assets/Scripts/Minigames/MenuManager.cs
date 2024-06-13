using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainContent;
    public GameObject subMiniGamesPanel;
    private GameObject subContent;
    public GameObject miniGameCard;
    public Sprite cardUnlocked;
    public Sprite cardLocked;
    public List<Place> placeList;

    public Navigation navigation;
    // Start is called before the first frame update
    void Start()
    {
        BaseLevel.isStory = false;
        foreach (Place place in placeList)
        {
            GameObject subPanel = Instantiate(subMiniGamesPanel, mainContent.transform);

            GameObject txtObj = subPanel.transform.Find("txtTitle").gameObject;
            TextMeshProUGUI txtTitle = txtObj.GetComponent<TextMeshProUGUI>();
            txtTitle.text = place.name;

            GameObject scrollView = subPanel.transform.Find("Scroll View").gameObject;
            GameObject viewport = scrollView.transform.Find("Viewport").gameObject;
            subContent = viewport.transform.Find("Content").gameObject;

            foreach (ItemPrefab item in place.itemList)
            {
                GameObject card = Instantiate(miniGameCard, subContent.transform);
                MinigamesCard itemCard = card.GetComponent<MinigamesCard>();
                itemCard.item = item.item;
                itemCard.navigation = navigation;

                GameObject txtOb = card.transform.Find("txtObject").gameObject;
                TextMeshProUGUI txtObject = txtOb.GetComponent<TextMeshProUGUI>();
                txtObject.text = item.item.name;

                if (DataManager.userProfile != null)
                {
                    int index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == item.item.id);
                    if (index != -1) item.item.isSolved = DataManager.userProfile.savedLevel[index].isSolved;
                }

                GameObject objectImage = card.transform.Find("imgObject").gameObject;
                objectImage.SetActive(item.item.isSolved);

                Image imageCard = item.item.isSolved ? objectImage.GetComponent<Image>() : card.GetComponent<Image>();
                imageCard.sprite = item.item.isSolved ? item.item.image ?? null : cardLocked;

                Button btnCard = card.GetComponent<Button>();
                btnCard.enabled = item.item.isSolved;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
