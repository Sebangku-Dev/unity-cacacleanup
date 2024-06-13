using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCard : MonoBehaviour
{
    public Item item;
    public GameObject itemSpawnPoint;
    public GameObject itemListCanvas;
    public GameObject hologram;

    public void OnItemClick()
    {
        Debug.Log(item.name);
        Instantiate(item.objectItem, itemSpawnPoint.transform);
        itemListCanvas.SetActive(!itemListCanvas.activeSelf);
        hologram.SetActive(!hologram.activeSelf);
    }
}
