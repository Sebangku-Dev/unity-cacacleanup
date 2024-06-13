using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseNavigation : MonoBehaviour
{
    string currentSceneName;
    public delegate void OnReplay();
    public OnReplay onReplay;

    public delegate void OnPause();
    public OnPause onPause;

    [SerializeField] GameObject PausePanel;
    [SerializeField] Button ButtonResume;
    [SerializeField] Button ButtonReplay;
    [SerializeField] Button ButtonHome;

    [SerializeField] Navigation navigation;
    [SerializeField] string homeScene;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        ButtonResume?.onClick.AddListener(TogglePausePanel);
        ButtonReplay?.onClick.AddListener(OnClickReplay);
        ButtonHome?.onClick.AddListener(navigation != null ? navigation.LoadScene : OnClickBackHome);
    }

    public void TogglePausePanel()
    {
        PausePanel?.SetActive(!PausePanel.activeSelf);
    }

    public void OnClickReplay()
    {
        onReplay?.Invoke();
        SceneManager.LoadScene(currentSceneName);
    }

    public void OnClickBackHome()
    {
        SceneManager.LoadScene(homeScene);
    }
}
