using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAnimation3D : MonoBehaviour
{
    public float rotationSpeed = 50f; // Kecepatan rotasi ikon

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
