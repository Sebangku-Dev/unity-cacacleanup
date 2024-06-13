using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tips : MonoBehaviour
{
    [System.Serializable]
    public class TipsContent
    {
        public string id;
        [TextArea]
        public string text;
        public Sprite illustration;
    }

    public List<TipsContent> listOfTips;
}
