
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GPGServices : MonoBehaviour
{
    // private bool isStandBy = true;
    private string Status { get; set; }
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(OnSignInResult);
    }

    private void OnSignInResult(SignInStatus signInStatus)
    {
        // isStandBy = false;
        if (signInStatus == SignInStatus.Success)
        {
            Status = "Authenticated. Hello, " + Social.localUser.userName + " (" + Social.localUser.id + ")";
        }
        else
        {
            Status = "*** Failed to authenticate with " + signInStatus;
        }
    }

    public void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

}
