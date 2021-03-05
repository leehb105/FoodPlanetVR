using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public bool displayWithText;

    private static bool displayWithTextStatic;
    private static int amount;
    private static TextMeshProUGUI highscoreText;

    void Start()
    {
        displayWithTextStatic = displayWithText;
        amount = PlayerPrefs.GetInt("HighscoreAmount", 0);
        highscoreText = this.GetComponent<TextMeshProUGUI>();
        DisplayAmount();
    }

    // Get highscore amount.
    public static int GetAmount()
    {
        return amount;
    }

    // Set highscore amount.
    public static void SetAmount(int amountToSet)
    {
        amount = amountToSet;
        DisplayAmount();
        PlayerPrefs.SetInt("HighscoreAmount", amount);
    }

    // Display highscore to the screen.
    private static void DisplayAmount()
    {
        // Check if display with "TOP: " text or without it.
        if(displayWithTextStatic)
        {
            highscoreText.text = "TOP: " + amount.ToString();
        }
        else
        {
            highscoreText.text = amount.ToString();

        }
    }
}
