using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class DataManager : MonoBehaviour
{
    public static User userProfile;
    public static User newUser;

    public GameObject prefabListOfItem;
    public GameObject prefabListOfQuest;
    public GameObject prefabListOfAchievement;

    public static event Action OnDataLoaded;

    [SerializeField] private bool isTesting = false;
    [SerializeField] private bool isLoadOnStart = false;
    [SerializeField] private bool isCheckData = false;
    void Start()
    {
        if (isLoadOnStart) LoadGame();
    }

    void Update()
    {
        if (isTesting)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
        }
    }
    private User CreateUserFile()
    {
        User user = newUser ?? new User();

        ListOfItem listOfItem = prefabListOfItem.GetComponent<ListOfItem>();
        foreach (ItemPrefab item in listOfItem.listOfItem)
        {
            StoryLevelSaved saved = new()
            {
                storyID = item.item.id,
                isSolved = false
            };

            user.savedLevel.Add(saved);
        }

        ListOfInteractiveQuest listOfInteractiveQuest = prefabListOfQuest.GetComponent<ListOfInteractiveQuest>();
        foreach (InteractiveQuest quest in listOfInteractiveQuest.listOfInteractiveQuest)
        {
            InteractiveQuestSaved saved = new()
            {
                id = quest.id,
                isSolved = false,
                countSolved = 0,
                listOfReport = new()
            };

            user.savedQuest.Add(saved);
        }

        ListOfAchievement listOfAchievement = prefabListOfAchievement.GetComponent<ListOfAchievement>();
        foreach (Achievement achievement in listOfAchievement.listOfAchievement)
        {
            AchievementSaved saved = new()
            {
                id = achievement.id,
                isAchieved = achievement.isAchieved
            };

            user.savedAchievement.Add(saved);
        }

        return user;
    }

    public void SaveGame()
    {
        User user = userProfile ?? CreateUserFile();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.save");
        binaryFormatter.Serialize(file, user);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/game.save"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game.save", FileMode.Open);
            User user = (User)binaryFormatter.Deserialize(file);
            file.Close();

            userProfile = user;

            if (isCheckData)
            {
                SceneManager.LoadScene("Home");
                OnDataLoaded?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning("File save isn't exist");
        }
    }

    void OnApplicationQuit()
    {
        if (DataManager.userProfile == null)
        {
            if (DataManager.userProfile.isGuest == true)
            {
                if (File.Exists(Application.persistentDataPath + "/game.save"))
                {
                    File.Delete(Application.persistentDataPath + "/game.save");
                }
                else
                {
                    Debug.LogWarning("File game.save tidak ditemukan.");
                }
            }
        }

        PlayerPrefs.DeleteAll();
    }
}