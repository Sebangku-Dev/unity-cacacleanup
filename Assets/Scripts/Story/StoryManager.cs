using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private GameObject chapterPrefab;
    private Chapter chapter;

    private int activeDialog;
    private int ActiveDialog
    {
        get
        {
            return activeDialog;
        }
        set
        {
            activeDialog += value;
            SetDialog();
        }
    }

    void Start()
    {
        chapter = chapterPrefab.GetComponent<Chapter>();
    }

    void SetDialog()
    {

    }

    public void onClickDialog()
    {
        
    }
}