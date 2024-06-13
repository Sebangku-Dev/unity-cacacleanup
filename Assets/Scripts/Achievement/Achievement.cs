using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement: MonoBehaviour
{
    public string id;
    public string title;
    [TextArea]
    public string description;
    [TextArea]
    public string message;
    public Sprite badgeIcon;
    public bool isAchieved;
}
