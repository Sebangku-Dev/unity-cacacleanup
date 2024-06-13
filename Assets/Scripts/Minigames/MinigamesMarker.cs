using System.Collections;
using UnityEngine;

public class MinigamesMarker : MonoBehaviour
{
    public Transform player; // Karakter pemain
    public float activationRange = 1f; // Jarak di mana marker akan aktif

    public GameObject navigationPanel;
    [SerializeField] GameObject hologram;
    [SerializeField] GameObject itemCanvas;
    public string txtButton;
    public string targetScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (navigationPanel != null)
            {
                navigationPanel.SetActive(true);
                navigationPanel.GetComponent<PopupAnimation>().OnLoad();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (navigationPanel != null)
            {
                navigationPanel.GetComponent<PopupAnimation>().OnClose();
                StartCoroutine(CloseOnAnimationDestroyed(PopupAnimation.duration));
            }
        }
    }

    private IEnumerator CloseOnAnimationDestroyed(float duration)
    {
        yield return new WaitForSeconds(duration);
        navigationPanel.SetActive(false);
    }

    public void ToggleHologram()
    {
        hologram.SetActive(!hologram.activeSelf);
    }

    public void ToggleItemCanvas()
    {
        itemCanvas.SetActive(!itemCanvas.activeSelf);
    }
}
