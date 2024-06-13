using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    AsyncOperation loadingOperation;
    float progressPercentage;
    bool isLoadScene = false;
    bool isStartLoad = false;
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] GameObject tipsContent;
    [SerializeField] TextMeshProUGUI txtTips;
    [SerializeField] TextMeshProUGUI txtPercentage;

    public string targetScene;

    private int previousSceneIndex;


    void Update()
    {
        if (isStartLoad)
        {
            progressPercentage = Mathf.Clamp01(loadingOperation.progress / 0.9f) * 100;
            txtPercentage.text = progressPercentage.ToString("n2") + "%";
        }
    }

    public void SavePreviosSceneIndex()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt($"previousScene", previousSceneIndex);
    }

    public int GetPreviousSceneIndex()
    {
        return PlayerPrefs.GetInt($"previousScene");
    }

    public void LoadScene()
    {
        SavePreviosSceneIndex();

        if (loadingCanvas)
        {
            loadingCanvas.SetActive(true);

            int index = Random.Range(0, 24);

            Tips tips = tipsContent.GetComponent<Tips>();

            txtTips.text = "Asal kamu tahu " + tips.listOfTips[index].id + ":\n" + tips.listOfTips[index].text;

            if (!isLoadScene)
            {
                Invoke("ToNextScene", 0.8f);
                isLoadScene = true;
            }
        }
        else
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    void ToNextScene()
    {
        loadingOperation = SceneManager.LoadSceneAsync(targetScene);
        isStartLoad = true;
        isLoadScene = false;
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(GetPreviousSceneIndex());
    }
}
