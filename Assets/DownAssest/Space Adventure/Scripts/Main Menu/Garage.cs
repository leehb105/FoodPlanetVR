using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Garage : MonoBehaviour
{
    public GameObject buyButton, addButton, removeButton;
    public TextMeshProUGUI partName, partPrice;
    public Transform rocket, part;
    public Animation notEnough;

    private List<GameObject> rocketItems, partItems;
    
    private int partIndex = 0;

    void Start()
    {
        LoadItems();
        LoadRocket();
        LoadPart();
        LoadButton();
    }

    // When player press previous button.
    public void Previous()
    {
        // Check whether the active part is not the first.
        if(partIndex > 0)
        {
            // Load previous part.
            partIndex--;
            LoadPart();
            LoadButton();
        }
    }

    // When player press next button.
    public void Next()
    {
        // Check whether the active part is not the last.
        if(partIndex < partItems.Count - 1)
        {
            // Loads next part.
            partIndex++;
            LoadPart();
            LoadButton();
        }
    }

    // When player press buy button.
    public void Buy()
    {
        // Take part script from the active part.
        Part part = partItems[partIndex].GetComponent<Part>();
        
        // Check if player has enough money to buy a part.
        if(Wallet.GetAmount() >= part.price)
        {
            // Save bought part value.
            PlayerPrefs.SetInt("PartBought-" + partItems[partIndex].name, 1);
            // Loas add/remove button.
            LoadButton();
            // Subract part price from player wallet.
            Wallet.SetAmount(Wallet.GetAmount() - part.price);
        }
        else
        {
            //Play not enough money animation.
            notEnough.Play("Not-Enough-In");
        }
    }

    // When player press add button.
    public void Add()
    {
        // Save added part value.
        PlayerPrefs.SetInt("PartAdded-" + partItems[partIndex].name, 1);
        // Load remove button.
        LoadButton();
        // Load rocket with added part.
        LoadRocket();
    }

    // When player press remove button.
    public void Remove()
    {
        // Save removed part value.
        PlayerPrefs.SetInt("PartAdded-" + partItems[partIndex].name, 0);
        // Load add button.
        LoadButton();
        // Load rocket with removed part.
        LoadRocket();
    }

    // Loading parts
    private void LoadItems()
    {
        // Load parts for the rocket.
        rocketItems = new List<GameObject>();
        foreach(Transform item in rocket)
        {
            if(item.name != "Base")
            {
                rocketItems.Add(item.gameObject);
            }
        }

        // Load parts for the shop.
        partItems = new List<GameObject>();
        foreach(Transform item in part)
        {
            partItems.Add(item.gameObject);
        }
    }

    // Load rocket parts.
    private void LoadRocket()
    {
        // Cycle between all rocket parts.
        for(int i = 0; i < rocketItems.Count; i++)
        {
            // Get value if rocket part is added.
            bool partAdded = PlayerPrefs.GetInt("PartAdded-" + partItems[i].name, 0) == 1 ? true : false;
            // Load rocket part gameobject.
            GameObject part = rocketItems[i];      
            // Enable or disable rocket part gameobject according to partAdded value.
            part.SetActive(partAdded);
        }
    }

    // Loading shop parts.
    private void LoadPart()
    {
        // Cycle between all shop parts.
        for(int i = 0; i < partItems.Count; i++)
        {
            // Load shop part gameobject.
            GameObject part = partItems[i];

            // Check the active part.
            if(i == partIndex)
            {
                // Enable and change name for active part.
                partName.text = part.name;
                part.SetActive(true);                
            }   
            else
            {   
                // Otherwise disable part gameobject.
                part.SetActive(false);
            }         
        }
    }

    // Load button according to the part state.
    private void LoadButton()
    {
        // Get value if part is bought.
        bool partBought = PlayerPrefs.GetInt("PartBought-" + partItems[partIndex].name, 0) == 1 ? true : false;
        if(partBought)
        {
            // Get value if part is added to the rocket.
            bool partAdded = PlayerPrefs.GetInt("PartAdded-" + partItems[partIndex].name, 0) == 1 ? true : false;
            if(partAdded)
            {
                // Display remove button.
                DisplayButton(false, false, true);
            }
            else
            {
                // Display add button.
                DisplayButton(false, true, false);
            }
        }
        else
        {
            // Display buy button with part price;
            DisplayButton(true, false, false);
            Part part = partItems[partIndex].GetComponent<Part>();
            partPrice.text = part.price.ToString();
        }
    }

    // Changing between buttons.
    private void DisplayButton(bool buy, bool add, bool remove)
    {
        if(buy)
        {
            ResetButtonRect(buyButton);
        }
        buyButton.SetActive(buy);

        if(add)
        {
            ResetButtonRect(addButton);
        }
        addButton.SetActive(add);

        if(remove)
        {
            ResetButtonRect(removeButton);
        }
        removeButton.SetActive(remove);
    }

    // Each time button is loaded it's scale is reset to the default size.
    private void ResetButtonRect(GameObject button)
    {
        button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
