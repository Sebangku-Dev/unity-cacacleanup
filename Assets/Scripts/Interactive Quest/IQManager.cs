using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IQManager : MonoBehaviour
{
    public static InteractiveQuest interactiveQuest;
    public static InteractiveQuestReport iqReport;
    [SerializeField] IQGenerator iqGenerator;
    int statePanel = -1;

    int StatePanel
    {
        get
        {
            return statePanel;
        }
        set
        {
            if (statePanel < 2)
            {
                statePanel = value;
                OnChangeStatePanel();
            }
            else
            {
                StateStep = 0;
            }
        }
    }

    int stateStep = -1;
    int StateStep
    {
        get { return stateStep; }
        set
        {
            if (stateStep < interactiveQuest.listStep.Count - 1)
            {
                stateStep += 1;
                OnChangeStepPanel();
            }
            else
            {
                OnFinishIQ();
            }
        }
    }

    [SerializeField] private Image icon;
    [SerializeField] private Sprite good;
    [SerializeField] private GameObject[] panels;

    [SerializeField] private Button btnNextStep;

    [SerializeField] private TextMeshProUGUI txtStepTitle;
    [SerializeField] private TextMeshProUGUI txtStepDetail;
    [SerializeField] private GameObject ifExperience;

    [SerializeField] CameraCapture cameraCapture;
    [SerializeField] private GameObject camCanvas;
    [SerializeField] private GameObject panelCamButton;
    [SerializeField] private GameObject panelDetailStep;

    void Start()
    {
        interactiveQuest = DataManager.userProfile != null ? iqGenerator.GetTodayTask() : iqGenerator.IQGenerate();

        stateStep = interactiveQuest.stepCount;
        icon.sprite = interactiveQuest.icon != null ? interactiveQuest.icon : null;

        if (interactiveQuest.isSolved)
        {
            StatePanel = 3;
        }
        else
        {
            StatePanel = stateStep <= 0 ? 0 : 2;
        }
    }

    void Update()
    {
        if (interactiveQuest != null && iqReport != null)
        {
            CameraCapture.step = StateStep;
            btnNextStep.interactable = !String.IsNullOrEmpty(iqReport.stepImageReportPath[StateStep]);
            // Debug.Log("report path: "+iqReport.stepImageReportPath[StateStep]);
        }
    }


    private void OnChangeStatePanel()
    {
        int index = 0;

        foreach (GameObject panel in panels)
        {
            panel.SetActive(index == StatePanel);
            index += 1;
        }
    }

    public void OnToggleTakePhoto()
    {
        camCanvas.SetActive(!camCanvas.activeSelf);
        panelCamButton.SetActive(!panelCamButton.activeSelf);
        panelDetailStep.SetActive(!panelDetailStep.activeSelf);

        cameraCapture.onSave += SaveFilePath;
    }

    void SaveFilePath()
    {
        OnToggleTakePhoto();
        iqReport.stepImageReportPath[StateStep] = CameraCapture.filePath;
        cameraCapture.onSave -= SaveFilePath;
    }

    private void OnChangeStepPanel()
    {
        txtStepTitle.text = interactiveQuest.listStep[stateStep].stepTitle;
        txtStepDetail.text = interactiveQuest.listStep[stateStep].detailStep;
    }

    public void OnClickNextState()
    {
        if (StatePanel <= 2) StatePanel += 1;
        else StateStep += 1;
    }

    private void OnFinishIQ()
    {
        statePanel += 1;
        icon.sprite = good;
        OnChangeStatePanel();
    }
}
