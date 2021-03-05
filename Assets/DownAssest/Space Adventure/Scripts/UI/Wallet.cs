using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    private static int amount;
    private static TextMeshProUGUI walletText;

    void Start()
    {
        amount = PlayerPrefs.GetInt("WalletAmount", 0);
        walletText = this.GetComponent<TextMeshProUGUI>();
        DisplayAmount();
    }

    // Get current player wallet amount.
    public static int GetAmount()
    {
        return amount;
    }

    // Set player amount to custom value.
    public static void SetAmount(int amountToSet)
    {
        amount = amountToSet;
        DisplayAmount();
        PlayerPrefs.SetInt("WalletAmount", amount);
    }

    // Display player amount to the screen.
    private static void DisplayAmount()
    {
        walletText.text = amount.ToString();
    }
}
