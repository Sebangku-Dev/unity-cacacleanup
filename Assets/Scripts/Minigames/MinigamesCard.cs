using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesCard : MonoBehaviour
{
    public Item item;
    public Navigation navigation;

    public void OnClickCard()
    {
        Debug.Log(item.storyLevel.gameScene);
        ChoosedItem.item = item;
        navigation.targetScene = item.storyLevel.gameScene;
        navigation.LoadScene();
    }
}
