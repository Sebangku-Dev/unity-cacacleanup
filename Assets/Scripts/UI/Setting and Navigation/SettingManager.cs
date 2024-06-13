using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTypes
{
    public enum type { Sound, SFX, BGM }
}
public class SettingManager : MonoBehaviour
{
    bool isInit = true;
    [SerializeField] private GameObject[] toggleBtnSound;
    [SerializeField] private Toggle toggleBtnSFX;
    [SerializeField] private Toggle toggleBtnBGM;

    [SerializeField] private Slider sliderSoundVolume;
    [SerializeField] private Slider sliderSFXVolume;
    [SerializeField] private Slider sliderBGMVolume;

    [SerializeField] private GameObject[] toggleBtnLanguage;

    [SerializeField] private AudioManager audioManager;

    void Start()
    {
        isInit = true;

        if (isInit)
        {
            toggleBtnBGM.isOn = Setting.isBGMOn;
            toggleBtnSFX.isOn = Setting.isSFXOn;
            isInit = false;
        }


        if (audioManager == null)
        {
            GameObject objAudioManager = GameObject.Find("AudioManager");
            if (objAudioManager != null) audioManager = objAudioManager.GetComponent<AudioManager>();
        }
    }

    public void OnChangeVolume(AudioTypes.type type)
    {
        switch (type)
        {
            case AudioTypes.type.Sound:
                Setting.volumeSound = sliderSoundVolume.value;
                break;
            case AudioTypes.type.SFX:
                Setting.volumeSFX = sliderSFXVolume.value;
                break;
            case AudioTypes.type.BGM:
                Setting.volumeBGM = sliderBGMVolume.value;
                break;
            default:
                break;
        }
    }

    public void OnToggleAudio(string type)
    {
        if (!isInit)
        {
            switch (type)
            {
                case "Sound":
                    Setting.isSoundOn = !Setting.isSoundOn;
                    break;
                case "SFX":
                    Setting.isSFXOn = !Setting.isSFXOn;
                    break;
                case "BGM":
                    Setting.isBGMOn = !Setting.isBGMOn;
                    audioManager.ToggleAudio("BGM");
                    break;
                default:
                    break;
            }
        }

    }

    public void OnChangeLanguage()
    {

    }
}