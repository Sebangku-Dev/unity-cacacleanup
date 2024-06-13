using UnityEngine;

public class RevealAnimation : MonoBehaviour
{
    private Vector3 startingLocation;

    private void Start()
    {
        this.startingLocation = transform.localPosition;

        Welcome.OnRegisterButtonClick += OnLoad;
        Welcome.OnBackButtonClick += OnClose;
    }
    private void Update()
    {
    }

    public void OnLoad()
    {
        transform.LeanMoveLocalY(startingLocation.y + 861, 0.3f).setEaseOutExpo();
    }

    public void OnClose()
    {
        transform.LeanMoveLocalY(startingLocation.y, 0.3f).setEaseOutExpo();
    }

    private void OnDestroy()
    {
        Welcome.OnRegisterButtonClick -= OnLoad;
        Welcome.OnBackButtonClick -= OnClose;
    }
}