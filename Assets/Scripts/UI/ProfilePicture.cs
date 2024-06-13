using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfilePicture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.userProfile != null && !String.IsNullOrEmpty(DataManager.userProfile.profileImagePath))
        {
            var fileContent = File.ReadAllBytes(DataManager.userProfile.profileImagePath);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(fileContent);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            Image img =  gameObject.GetComponent<Image>();
            img.sprite = sprite;
        }
    }

}
