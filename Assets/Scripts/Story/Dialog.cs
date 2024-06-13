using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog: MonoBehaviour
{
    public string character;
    [TextArea]
    public string naration;
    public Sprite spritePose;
    public Sprite spriteBackground;
    public Sprite spriteEffect;
    public AudioClip sound;
    public AudioClip sfx;
    public AudioClip bgm;
    public bool isQuestion;
    public string[] answer;
}