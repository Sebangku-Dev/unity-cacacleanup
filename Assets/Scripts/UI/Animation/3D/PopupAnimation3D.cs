using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupAnimation3D : MonoBehaviour
{
    [SerializeField] public GameObject mesh;
    private Rigidbody rb;
    private float speed = 1500f;

    private void Start()
    {
        if (mesh != null)
        {
            rb = mesh.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && rb != null)
        {
            Debug.Log("2");
            rb.AddForce(Vector3.up * speed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

}