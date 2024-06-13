using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; // Karakter atau objek yang ingin diikuti kamera
    public float rotateSpeed = 5f; // Kecepatan rotasi kamera

    private Vector2 lastTouchPos; // Posisi sentuhan terakhir

    void Update()
    {
        // Memeriksa input dari mouse pada PC
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // Menghitung perubahan posisi drag kursor
            float rotateAmount = (lastTouchPos.x - Input.mousePosition.x) * rotateSpeed * Time.deltaTime;

            // Mengatur rotasi kamera berdasarkan perubahan posisi drag kursor
            transform.RotateAround(target.position, Vector3.up, rotateAmount);

            // Memperbarui posisi drag kursor terakhir
            lastTouchPos = Input.mousePosition;
        }

        // Memeriksa input sentuhan pada layar perangkat mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Memeriksa apakah sentuhan dimulai
            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPos = touch.position;
            }
            // Memeriksa apakah sentuhan sedang berlangsung
            else if (touch.phase == TouchPhase.Moved)
            {
                // Menghitung perubahan posisi sentuhan
                float rotateAmount = (lastTouchPos.x - touch.position.x) * rotateSpeed * Time.deltaTime;

                // Mengatur rotasi kamera berdasarkan perubahan posisi sentuhan
                transform.RotateAround(target.position, Vector3.up, rotateAmount);

                // Memperbarui posisi sentuhan terakhir
                lastTouchPos = touch.position;
            }
        }
    }
}
