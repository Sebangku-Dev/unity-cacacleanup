using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenMissionList : MonoBehaviour
{
    [SerializeField] bool isPlayerRoaming = true;
    public GameObject missionListCanvas;
    [SerializeField] private TextMeshProUGUI txtItem;
    [SerializeField] private GameObject content;
    public GameObject panelMission;
    private Item item;
    private StoryLevel stroyLevel;
    public List<SubLevel> subLevelList;
    private string gameplayScene;

    [SerializeField] GameObject[] RegulerLevelView;
    [SerializeField] GameObject PremiumLock;

    [SerializeField] private Navigation navigation;
    // Start is called before the first frame update
    void Start()
    {
        if (missionListCanvas != null)
        {
            GameObject objPanel = missionListCanvas.transform.Find("Panel Info").gameObject;
            GameObject objTxt = objPanel.transform.Find("txtItem").gameObject;
            txtItem = objTxt.GetComponent<TextMeshProUGUI>();

            GameObject objScrollView = missionListCanvas.transform.Find("Scroll View").gameObject;
            GameObject objViewPort = objScrollView.transform.Find("Viewport").gameObject;
            content = objViewPort.transform.Find("Content").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenMissionListPanel()
    {
        if (missionListCanvas != null)
        {
            missionListCanvas.SetActive(true);
            missionListCanvas.GetComponent<PopupAnimation>().OnLoad();
        }

        item = ChoosedItem.item;

        if (RegulerLevelView != null) foreach (GameObject reguler in RegulerLevelView) reguler.SetActive(!item.isPremium);

        if (PremiumLock != null) PremiumLock?.SetActive(item.isPremium);

        if (!item.isPremium)
        {
            stroyLevel = item.storyLevel;
            subLevelList = stroyLevel.subLevelList;

            gameplayScene = stroyLevel.gameScene;

            int indexSubLevel = 0;
            foreach (SubLevel subLevel in subLevelList)
            {
                if (panelMission != null && content != null)
                {
                    // Instantiate the prefab
                    GameObject panelMissionInstance = Instantiate(panelMission, content.transform);

                    GameObject objToggle = panelMissionInstance.transform.Find("Toggle").gameObject;
                    Toggle toggle = objToggle.GetComponent<Toggle>();

                    int index = -1;
                    if (DataManager.userProfile != null)
                    {
                        index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == item.id);
                    }

                    toggle.isOn = index != -1 && (indexSubLevel < DataManager.userProfile.savedLevel[index].activityState);

                    GameObject objTxt = panelMissionInstance.transform.Find("txtTitle").gameObject;
                    TextMeshProUGUI txtTitle = objTxt.GetComponent<TextMeshProUGUI>();
                    txtTitle.text = subLevel.title;

                    indexSubLevel += 1;
                }
                else
                {
                    Debug.LogError("Prefab or parent object is not assigned.");
                }
            }
        }


        if (txtItem != null) txtItem.text = "Level " + item.name;

        if (isPlayerRoaming) Setting.isPlayerActive = false;
    }

    public void CloseMissionListPanel()
    {
        if (missionListCanvas != null)
        {
            missionListCanvas.GetComponent<PopupAnimation>().OnClose();
            StartCoroutine(CloseOnAnimationDestroyed(PopupAnimation.duration));
        }

        if (content != null)
        {
            // Loop melalui semua anak dari objek induk
            foreach (Transform child in content.transform)
            {
                // Hancurkan setiap anak
                Destroy(child.gameObject);
            }
        }

        if (isPlayerRoaming) Setting.isPlayerActive = true;
    }

    private IEnumerator CloseOnAnimationDestroyed(float duration)
    {
        yield return new WaitForSeconds(duration);
        missionListCanvas.SetActive(false);
    }

    public void OnClickPlay()
    {
        if (isPlayerRoaming)
        {
            PlayerPrefs.SetFloat("PlayerX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
            PlayerPrefs.Save();
        }
        navigation.targetScene = gameplayScene;
    }
}
