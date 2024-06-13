using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameText : MonoBehaviour
{
    private TextMeshProUGUI usernameText;

    void Start()
    {

        usernameText = GetComponent<TextMeshProUGUI>();
        usernameText.text = $"Nama: {GetSavedUsername()}";
    }

    private string GetSavedUsername()
    {
        if (DataManager.userProfile == null)
        {
            return "-";
        }

        return DataManager.userProfile.fullName;

        // return DataManager.userProfile.savedScore;
    }
}
