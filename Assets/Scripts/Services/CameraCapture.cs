using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{
    // Mendefinisikan delegate
    public delegate void SaveAction();

    // Membuat event berdasarkan delegate
    public event SaveAction onSave;

    WebCamTexture webCamTexture;
    [SerializeField] private Button captureButton;
    [SerializeField] private Button saveButton;

    public static string filePath;
    public static int step;
    byte[] imageBytes;
    // Start is called before the first frame update
    void Start()
    {
         // Inisialisasi WebCamTexture dan mulai menampilkan preview kamera
        webCamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();

        // Tambahkan listener untuk button
        captureButton.onClick.AddListener(CaptureImage);
        saveButton.onClick.AddListener(SaveImage);
    }

    void CaptureImage()
    {
        // Mengambil gambar dari WebCamTexture
        Texture2D snap = new Texture2D(webCamTexture.width, webCamTexture.height);
        snap.SetPixels(webCamTexture.GetPixels());
        snap.Apply();

        // Konversi Texture2D ke array byte
        imageBytes = snap.EncodeToPNG();
    }

    void SaveImage()
    {
        // Menentukan path penyimpanan file
        CameraCapture.filePath = Path.Combine(Application.persistentDataPath, "reportImage.png");

        // Menyimpan byte gambar ke file
        File.WriteAllBytes(CameraCapture.filePath, imageBytes);

        // Menampilkan path file di log (opsional)
        Debug.Log("Saved Image to: " + CameraCapture.filePath);

        onSave?.Invoke();
    }
}
