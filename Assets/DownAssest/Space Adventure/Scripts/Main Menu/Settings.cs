using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Sprite soundOn, soundOff, vibrationOn, vibrationOff;
    public Image soundImage, vibrationImage;

    void Start()
    {
        // When game starts set the settings to the game.
        SetSounds();
        SetVibration();
    }

    // When player selected to change sound state.
    public void ChangeSounds()
    {
        // Get sounds state.
        bool active = GetSetting("Sounds");
        
        // If sounds is active
        if(active)
        {
            // Disable sounds in the game.
            soundImage.sprite = soundOff;
            AudioListener.volume = 0.0f;
            ChangeSetting("Sounds", 0);
        }
        else
        {
            // Enable sounds in the game.
            soundImage.sprite = soundOn;
            AudioListener.volume = 1.0f;
            ChangeSetting("Sounds", 1);
        }
    }

    // When player selected to change vibration state.
    public void ChangeVibration()
    {
        // Get vibration state.
        bool active = GetSetting("Vibration");
        
        // If vibration is active
        if(active)
        {
            // Disable vibrations in the game.
            vibrationImage.sprite = vibrationOff;
            ChangeSetting("Vibration", 0);
        }
        else
        {
            // Enable vibrations in the game.
            vibrationImage.sprite = vibrationOn;
            ChangeSetting("Vibration", 1);
        }
    }

    // If player selected to reset all game progress.
    public void Reset()
    {
        // Delete all saved player prefs.
        PlayerPrefs.DeleteAll();
    }

    // Used to get setting value.
    public static bool GetSetting(string name)
    {
        return PlayerPrefs.GetInt(name, 1) == 1 ? true : false;
    }

    // Used to change setting value.
    private void ChangeSetting(string name, int state)
    {
        PlayerPrefs.SetInt(name, state);
    }

    // Setting sounds at the start of the game.
    private void SetSounds()
    {
        // Get sound state.
        bool active = GetSetting("Sounds");
        
        // If sounds is active.
        if(active)
        {
            // Enable sounds.
            soundImage.sprite = soundOn;
            AudioListener.volume = 1.0f;
        }
        else
        {
            // Disable sounds.
            soundImage.sprite = soundOff;
            AudioListener.volume = 0.0f;
        }
    }

    private void SetVibration()
    {
        // Get vibration state.
        bool active = GetSetting("Vibration");
        
        // If vibration is active.
        if(active)
        {
            // Enable vibration.
            vibrationImage.sprite = vibrationOn;
        }
        else
        {
            // Disable vibration.
            vibrationImage.sprite = vibrationOff;
        }
    }
}
