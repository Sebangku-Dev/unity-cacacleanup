using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractiveQuestSaved
{
    public string id;
    public bool isSolved;
    public int countSolved;
    public List<InteractiveQuestReport> listOfReport;
}
