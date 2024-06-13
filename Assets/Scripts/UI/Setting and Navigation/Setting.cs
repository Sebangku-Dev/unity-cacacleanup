using UnityEngine;

public class Languages
{
    public enum Language{ID, ENG}
}
public class Setting : MonoBehaviour
{
    public static bool isPlayerActive;
    
    public static bool isSoundOn = true;
    public static bool isSFXOn = true;
    public static bool isBGMOn = true;

    public static float volumeSound;
    public static float volumeSFX;
    public static float volumeBGM;

    public static Languages.Language language;
}