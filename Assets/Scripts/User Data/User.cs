using System.Collections;
using System.Collections.Generic;

public class Education
{
    public enum Kelas { one, two, three, other }
}

[System.Serializable]
public class User
{
    public string id;
    public string fullName;
    public string profileImagePath;
    public int age;
    public Education.Kelas kelas;

    public List<StoryLevelSaved> savedLevel = new();
    public List<InteractiveQuestSaved> savedQuest = new();
    public List<AchievementSaved> savedAchievement = new();

    public TodaysIQState todaysIQState = new();

    public int savedScore;
    public bool isGuest;
}