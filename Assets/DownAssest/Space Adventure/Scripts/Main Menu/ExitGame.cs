using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public bool rateGameOnExit;

    public RateGame rateGame;

    private bool windowOpened;

    void Update()
    {
        #if UNITY_ANDROID
        // Check if back/esc button was pressed.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            // Checks if any other window isn't opened.
            if(!windowOpened)
            {
                Exit();
            }
        }    
        #endif    
    }

    public void Exit()
    {
        // If rate game is enabled.
        if(rateGameOnExit)
        {
            // Opens rate game window.
            rateGame.OpenWindow();
        }
        else
        {
            //Closes game.
            Application.Quit();
        }
    }

    // Used to set the windowOpened value if other window is opened.
    public void WindowOpened(bool opened)
    {
        windowOpened = opened;
    }
}
