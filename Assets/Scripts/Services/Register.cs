using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.Events;

public class Register : MonoBehaviour
{
    [SerializeField] private Navigation navigation;
    [SerializeField] private GameObject imgProfile;
    [SerializeField] private Image imgPlaceholder;
    [SerializeField] private Sprite placeholder;
    [SerializeField] private TextMeshProUGUI ifFullName;
    [SerializeField] private TMP_InputField ifAge;
    [SerializeField] private TextMeshProUGUI ifClass;
    [SerializeField] private ModalHandler modalHandler;

    // Events
    public UnityEvent<string> OnDataError;


    User newUser;
    DataManager dataManager;


    string path;

    // Start is called before the first frame update
    void Start()
    {
        newUser = new User();

        if (OnDataError == null)
        {
            OnDataError = new UnityEvent<string>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChangeName()
    {
        newUser.fullName = ifFullName.text ?? null;
    }

    public void OnChangeAge()
    {
        if (!string.IsNullOrEmpty(ifAge.text))
        {
            string txtAge = ifAge.text;

            if (int.TryParse(txtAge, out int age))
            {
                newUser.age = age;
            }
            else
            {
                Debug.LogWarning("Invalid age input. Please enter a valid number.");
            }
        }

    }

    public void OnClassChange()
    {
        switch (ifClass.text)
        {
            case "Kelas 1":
                newUser.kelas = Education.Kelas.one;
                break;
            case "Kelas 2":
                newUser.kelas = Education.Kelas.two;
                break;
            case "Kelas 3":
                newUser.kelas = Education.Kelas.three;
                break;
            default:
                newUser.kelas = Education.Kelas.other;
                break;
        }
    }

    public void OnSelectProfile()
    {
        //file picker here
#if UNITY_ANDROID
        if (NativeFilePicker.IsFilePickerBusy())
        {
            return;
        }
        string fileTypes = "image/*";
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((paths) =>
            {
                if (paths == null)
                    Debug.Log("Operation cancelled");
                else
                    Debug.Log("Picked file: " + paths);
                path = paths;
            }, new string[] { fileTypes });
# else
        path = EditorUtility.OpenFilePanel("Select Profile Picture", "", "Image Files");
# endif
        //

        if (!String.IsNullOrEmpty(path))
        {
            var fileContent = File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(fileContent);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            imgPlaceholder.sprite = placeholder;
            Image img = imgProfile.GetComponent<Image>();
            img.sprite = sprite;
            CanvasGroup canvasGroup = imgProfile.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1.0f;
        }
    }

    public void SubmitAsGuest()
    {
        newUser.fullName = "Guest";
        newUser.age = 6;
        newUser.kelas = Education.Kelas.other;

        DateTime currentTime = DateTime.Now;
        string currentTimeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        newUser.id = newUser.fullName + currentTimeString;
        newUser.isGuest = true;

        DataManager.newUser = newUser;

        GameObject userData = GameObject.Find("UserData");
        dataManager = userData.GetComponent<DataManager>();
        dataManager.SaveGame();

        navigation.LoadScene();
    }

    public void SubmitRegister()
    {
        if (newUser.fullName != null && newUser.age != 0 && !String.IsNullOrEmpty(path))
        {
            DateTime currentTime = DateTime.Now;
            string currentTimeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

            newUser.id = newUser.fullName + currentTimeString;

            var fileContent = File.ReadAllBytes(path);
            string destinationPath = Path.Combine(Application.persistentDataPath, Path.GetFileName(path));
            File.WriteAllBytes(destinationPath, fileContent);

            newUser.profileImagePath = destinationPath;
            newUser.isGuest = false;

            DataManager.newUser = newUser;

            GameObject userData = GameObject.Find("UserData");
            dataManager = userData.GetComponent<DataManager>();
            dataManager.SaveGame();

            navigation.LoadScene();
        }
        else
        {
            // Dont forget to attach corresponding function call
            OnDataError.AddListener(modalHandler.ActivateModal);

            OnDataError?.Invoke("Data harus dilengkapi");
        }
    }
}
