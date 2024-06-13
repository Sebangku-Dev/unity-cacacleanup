using UnityEngine;

public class GameType
{
    public enum types{DragAndDrop, Trivia, Puzzle, Slide, Catch, Find}
}

[System.Serializable]
public class SubLevel
{
    public string id;
    public string title;
    [TextArea]
    public string description;
    public GameType.types type;
}