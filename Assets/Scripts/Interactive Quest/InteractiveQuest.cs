using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStep
{
    public string stepTitle;
    [TextArea]
    public string detailStep;
}

public class Difficulties
{
    public enum Difficulty { Easy, Medium, Hard }
}

[System.Serializable]
public class InteractiveQuest
{
    public string id;
    public string title;
    [TextArea]
    public string description;
    public Sprite icon;
    public Difficulties.Difficulty difficulty;

    public List<QuestStep> listStep = new();
    public int stepCount;

    public int poin;
    public float hourLimit;

    public bool isSolved;
}
