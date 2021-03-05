using UnityEngine;

public class RateGame : MonoBehaviour
{
    public string packageName;

    private bool rateGameOpened;
    private AnimationController animationController;

    void Start()
    {
        animationController = this.GetComponent<AnimationController>();
    }

    public void OpenWindow()
    {
        // Get value if game was rated.
        bool gameWasRated = PlayerPrefs.GetInt("GameWasRated", 0) == 1 ? true : false;
        // If game wasn't rated and rate game window wasn't opened.
        if(!gameWasRated && !rateGameOpened)
        {
            // Open rate game window.
            animationController.OpenWindow();
            rateGameOpened = true;
        }
        else
        {
            // Exit game.
            Exit();
        }
    }

    // If player selects close window button.
    public void CloseWindow()
    {
        // Close window animation.
        animationController.CloseWindow();
        rateGameOpened = false;        
    }

    // If player selects to rate game.
    public void Rate()
    {
        // Set value that game was rated.
        PlayerPrefs.SetInt("GameWasRated", 1);
        // Open game url.
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + packageName);
        // Exit game.
        Exit();
    }

    // If player selected to exit game.
    public void Exit()
    {
        Application.Quit();
    }
}
