using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareScore : MonoBehaviour
{
    int index;
    string path;

    [SerializeField] GameObject Env;
    [SerializeField] GameObject[] UIs;

    bool isDoneShare = false;

    void Start()
    {
        if (PlayerPrefs.HasKey("indexShare")) index = PlayerPrefs.GetInt("indexShare");
    }

    public void OnClickShare()
    {
        foreach (GameObject ui in UIs) ui.SetActive(false);
        Env.SetActive(true);

        StartCoroutine( TakeScreenshot() );
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Saya menyelesaikan misi di Marica Kerja Bakti").SetText("Ayo, bantu Mari mendapatkan penghargaan rumah terbersih!").SetUrl("https://teman.marica.id/")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
        
        Invoke("ResetUI", 1.0f);
    }

    void ResetUI()
    {
        foreach (GameObject ui in UIs) ui.SetActive(true);
        Env.SetActive(false);
    }
}
