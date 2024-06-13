using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string id;
    public string name;
    public Sprite image;
    [TextArea]
    public string description;
    public GameObject objectItem;
    public GameObject marker;
    public bool isSolved = false;
    public bool isPremium = false;
    public int activityState;
    public StoryLevel storyLevel;
}