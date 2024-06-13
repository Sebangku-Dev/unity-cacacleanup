using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static bool isPlayingContinue;
    [SerializeField] private bool isContinue;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private AudioSource audioSource;
    [SerializeField] private AudioClip mainThemeClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        if (Setting.isBGMOn) audioSource.Play();
    }

    void Start()
    {
        if (mainThemeClip != null && !AudioManager.isPlayingContinue)
        {
            AudioManager.Instance.PlayBGM(mainThemeClip);
            AudioManager.isPlayingContinue = true;
        }
    }
    public void ToggleAudio(string audioType)
    {
        if (audioType == "BGM")
        {
            Debug.Log(Setting.isBGMOn);
            if (!Setting.isBGMOn) AudioManager.Instance.audioSource.Stop();
            else AudioManager.Instance.PlayBGM(mainThemeClip);
        }
    }
}
