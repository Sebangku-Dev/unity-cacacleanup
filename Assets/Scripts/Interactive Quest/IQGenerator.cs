using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IQGenerator : MonoBehaviour
{
    InteractiveQuest iq;

    [SerializeField] private ListOfInteractiveQuest listOfIQ;
    int index;

    public InteractiveQuest IQGenerate()
    {
        index = Random.Range(0, listOfIQ.listOfInteractiveQuest.Count);
        iq = listOfIQ.listOfInteractiveQuest[index];

        System.DateTime currentDate = System.DateTime.Now;
        
        IQManager.iqReport = new() {
            id = "iqr" + iq.id + currentDate.ToString("dd/mm/yyyy"),
            stepImageReportPath = new string[iq.listStep.Count]
        };

        if (DataManager.userProfile != null)
        {
            DataManager.userProfile.todaysIQState.todaysIQID = iq.id;
            DataManager.userProfile.todaysIQState.startAt = System.DateTime.Now.ToString("o");
        }

        return iq;
    }

    public InteractiveQuest GetTodayTask()
    {
        InteractiveQuest loadQuest;

        if (DataManager.userProfile.todaysIQState != null)
        {
            InteractiveQuest questByID = GetQuestByID(DataManager.userProfile.todaysIQState.todaysIQID);
            return questByID;
        }
        else
        {
            loadQuest = IQGenerate();
            return loadQuest;
        }
    }

    InteractiveQuest GetQuestByID(string id)
    {
        foreach (InteractiveQuest iq in listOfIQ.listOfInteractiveQuest)
        {
            if (iq.id == id)
            {
                IQManager.iqReport = DataManager.userProfile.todaysIQState.report;
                return iq;
            };
        }

        return IQGenerate();
    }
}
