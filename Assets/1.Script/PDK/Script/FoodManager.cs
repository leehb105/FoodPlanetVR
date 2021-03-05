using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodManager : MonoBehaviour {
    public static FoodManager Instance;
    public void Awake() {
        Instance = this;
        panel1.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/background");
        panel2.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/background");
        panel3.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/background");
        manual1.text = "";
        manual2.text = "";
        manual3.text = "";

    }
    public Dictionary<string, float> foodTimeDict = new Dictionary<string, float>() {
        {"BlackDrink", 30f },
        {"GreenDrink", 30f},
        //{"GreenToast", 10f},
        //{"PurpleToast", 10f},
        //{"GreenSalad",10f},
        //{"PurpleSalad",10f},
        //{"RareSteak", 10f},
        //{"MediumSteak", 10f},
        {"WelldoneSteak", 50f},
        {"MiniBurger", 50f},
        {"CheeseBurger", 60f}
    };

    public Dictionary<string, int> foodPriceDict = new Dictionary<string, int>() {
        {"BlackDrink", 500},
        {"GreenDrink", 500},
        //{"GreenToast", 200},
        //{"PurpleToast", 200},
        //{"GreenSalad",200},
        //{"PurpleSalad",200},
        //{"RareSteak", 500},
        //{"MediumSteak", 500},
        {"WelldoneSteak", 1500},
        {"MiniBurger", 1500},
        {"CheeseBurger", 2000},
    };
    //public Dictionary<string, float> foodRandomWeightDict = new Dictionary<string, float>() {
    //    {"BlackDrink", 9.09f },
    //    {"GreenDrink", 9.09f},
    //    {"GreenToast", 9.09f},
    //    {"PurpleToast", 9.09f},
    //    {"GreenSalad",9.09f},
    //    {"PurpleSalad",9.09f},
    //    {"RareSteak", 9.09f},
    //    {"MediumSteak", 9.09f},
    //    {"WelldoneSteak", 9.09f},
    //    {"MiniBurger", 9.09f},
    //    {"FullBurger", 9.09f},
    //};

    public GameObject panel1, panel2, panel3;
    public TextMeshProUGUI manual1, manual2, manual3;
    List<string> dishFood = new List<string> { };


    public List<string> FinishedDish {
        get { return dishFood; }
        set {
        }
    }

    void Start() {
    }
    // Update is called once per frame
    void Update() {
    }

    public void FoodTimePlus() {
        foodTimeDict.Clear();
        foodTimeDict.Add("BlackDrink", 60f);
        foodTimeDict.Add("GreenDrink", 60f);
        //foodTimeDict.Add("GreenToast", 300f);
        //foodTimeDict.Add("PurpleToast", 300f);
        //foodTimeDict.Add("GreenSalad", 300f);
        //foodTimeDict.Add("PurpleSalad", 300f);
        //foodTimeDict.Add("RareSteak", 400f);
        //foodTimeDict.Add("MediumSteak", 500f);
        foodTimeDict.Add("WelldoneSteak", 100f);
        foodTimeDict.Add("MiniBurger", 100f);
        foodTimeDict.Add("CheeseBurger", 120f);
    }

    public void OnChildTriggerEnter(string customerFood, int childNum, int customerNum) {
        switch (childNum) {
            case 0:
                manual1.text = customerNum.ToString();
                //manual1.text += "\n" + customerFood;
                panel1.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/" + customerFood.ToString());
                break;
            case 1:
                manual2.text = customerNum.ToString();
                //manual2.text += "\n" + customerFood;
                panel2.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/" + customerFood.ToString());
                break;
            case 2:
                manual3.text = customerNum.ToString();
                //manual3.text += "\n" + customerFood;
                panel3.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/" + customerFood.ToString());
                break;
        }
    }

    public void OnChildTriggerExit(int childNum) {
        switch (childNum) {
            case 0:
                manual1.text = "";
                panel1.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/background");
                break;
            case 1:
                manual2.text = "";
                panel2.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/background");
                break;
            case 2:
                manual3.text = "";
                panel3.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/background");
                break;
        }
    }
}
