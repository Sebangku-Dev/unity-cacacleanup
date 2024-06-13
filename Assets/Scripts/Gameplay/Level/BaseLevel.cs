using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] protected Navigation navigation;
    [SerializeField] protected PauseNavigation pauseNavigation;
    [SerializeField] protected DataManager dataManager;
    public static bool isStory = true;
    protected int indexActivity = -1;
    protected int IndexActivity
    {
        get { return indexActivity; }
        set
        {
            if (indexActivity < activityPanel.Length - 1)
            {
                indexActivity = value;
                OnNextActivity();
            }
            else
            {
                panelHint.SetActive(false);
                activityPanel[indexActivity].SetActive(false);
                resultPanel.SetActive(true);
                finishAnimation.Play();
                Invoke("FinishLevel", 7.0f);
            }
        }
    }
    protected Item item;
    protected List<SubLevel> subLevelList;
    public GameObject[] activityPanel;
    public GameObject resultPanel;
    [SerializeField] private MeshRenderer videoRenderer;
    public VideoPlayer finishAnimation;
    protected float activityDelay = 1f;
    protected bool isActivityNext = false;

    [SerializeField] private GameObject panelHint;
    public TextMeshProUGUI txtSubTitle;

    // Start is called before the first frame update
    protected void Start()
    {
        pauseNavigation.onReplay += OnReplayLevel;

        item = ChoosedItem.item;
        // Debug.Log("In Level: "+item.name);

        StoryLevel stroyLevel = item.storyLevel;
        subLevelList = stroyLevel.subLevelList;
        // Debug.Log(subLevelList[0].title);

        int index = -1;

        if (DataManager.userProfile != null) index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == item.id);
        IndexActivity = !isStory ? 0 : index == -1 ? 0 : DataManager.userProfile.savedLevel[index].activityState;
    }

    protected virtual void Update()
    {
        if (finishAnimation.frame > 5)
        {
            SetVideoMesh();
        }
    }

    protected void NextActivity()
    {
        IndexActivity += 1;
        isActivityNext = false;
    }

    protected void OnNextActivity()
    {
        int index = 0;
        foreach (GameObject panel in activityPanel)
        {
            panel.SetActive(index == IndexActivity ? true : false);
            index += 1;
        }

        txtSubTitle.text = subLevelList[IndexActivity].title;
        SaveStateActivity();
    }

    protected void OnReplayLevel()
    {
        IndexActivity = 0;
        pauseNavigation.onReplay -= OnReplayLevel;
    }

    protected void SetVideoMesh()
    {
        videoRenderer.enabled = true;
    }

    protected void FinishLevel()
    {
        resultPanel.SetActive(false);

        if (isStory)
        {
            bool check = item.isSolved;
            if (check == false)
            {
                item.isSolved = true;
                ScoreManager.Instance.AddScore(1); // Adding Score

                SaveProgress(1);
            }
        }

        navigation.targetScene = isStory ? "Main" : "FreeRoom";
        navigation.LoadScene();
    }

    private void SaveStateActivity()
    {
        StoryLevelSaved stateSaved = new StoryLevelSaved
        {
            storyID = item.id,
            activityState = IndexActivity,
            isSolved = item.isSolved
        };

        int index = -1;

        if (DataManager.userProfile != null) index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == stateSaved.storyID);

        if (index != -1)
        {
            DataManager.userProfile.savedLevel[index] = stateSaved;
            dataManager.SaveGame();
        }
    }

    private void SaveProgress(int savedScore)
    {
        StoryLevelSaved newSaved = new StoryLevelSaved
        {
            storyID = item.id,
            isSolved = true
        };

        int index = -1;

        if (DataManager.userProfile != null) index = DataManager.userProfile.savedLevel.FindIndex(level => level.storyID == newSaved.storyID);

        if (index != -1)
        {
            DataManager.userProfile.savedLevel[index].isSolved = newSaved.isSolved;
            DataManager.userProfile.savedScore += savedScore;
            dataManager.SaveGame();
        }
    }

}
