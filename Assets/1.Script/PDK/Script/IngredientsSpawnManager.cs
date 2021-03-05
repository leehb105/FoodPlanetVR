using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawnManager : MonoBehaviour {
    public static IngredientsSpawnManager Instance;
    public void Awake() {
        Instance = this;
    }

    [Header("Food Ingredients")]
    public GameObject[] breads;
    public GameObject[] meats;
    //빵은 FoodType0, 고기는 FoodType1, 상추는 FoodType2, 치즈는 3, 탄거는 4, 검은눈알5, 초록눈알6
    public Dictionary<string, GameObject> breadDict = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> meatDict = new Dictionary<string, GameObject>();

    public GameObject cheese;
    public GameObject burnedFood;

    [Header("Spawn Point")]
    public GameObject breadSpawnPoint;
    public GameObject meatSpawnPoint;
    public GameObject cheeseSpawnPoint;

    int stageLevel;

    GameObject foodBread, foodMeat, foodCheese, foodBurned;

    void Start() {
        breadDict.Add("0Square_Rock", breads[0]);
        breadDict.Add("1Round_Rock", breads[1]);
        breadDict.Add("2BurgerBun_Top", breads[2]);
        breadDict.Add("3BurgerBun_Bottom", breads[3]);

        meatDict.Add("0BIGMeat", meats[0]);
        meatDict.Add("1MeatBone", meats[1]);
        meatDict.Add("2Patty", meats[2]);
        meatDict.Add("3SteakMeat", meats[3]);
        meatDict.Add("6Steak_Welldone", meats[4]);
    }

    // Update is called once per frame
    void Update() {
    }

    public void FoodPrepping(IngredientsSpawnPoint sp, Vector3 FoodPosition, string FoodName, int FoodType) {
        switch (FoodType) {
            case 0:
                foodBread = Instantiate(breadDict[FoodName]);
                foodBread.name = FoodName;
                foodBread.transform.position = FoodPosition;
                //foodBread.GetComponent<FoodScript>().linkPoint = sp;
                break;
            case 1:
                foodMeat = Instantiate(meatDict[FoodName]);
                foodMeat.name = FoodName;
                foodMeat.transform.position = FoodPosition;
                //foodBread.GetComponent<FoodScript>().linkPoint = sp;
                break;
                break;
            case 3:
                foodCheese = Instantiate(cheese);
                foodCheese.name = FoodName;
                foodCheese.transform.position = FoodPosition;
                //foodBread.GetComponent<FoodScript>().linkPoint = sp;
                break;
            case 4:
                foodBurned = Instantiate(burnedFood);
                foodBurned.name = FoodName;
                foodBurned.transform.position = FoodPosition;
                //foodBread.GetComponent<FoodScript>().linkPoint = sp;
                break;
        }
    }

    public void FoodPrepping(Vector3 FoodPosition, string FoodName, int FoodType) {
        switch (FoodType) {
            case 0:
                foodBread = Instantiate(breadDict[FoodName]);
                foodBread.name = FoodName;
                foodBread.transform.position = FoodPosition;
                break;
            case 1:
                foodMeat = Instantiate(meatDict[FoodName]);
                foodMeat.name = FoodName;
                foodMeat.transform.position = FoodPosition;
                break;
            case 3:
                foodCheese = Instantiate(cheese);
                foodCheese.name = FoodName;
                foodCheese.transform.position = FoodPosition;
                break;
            case 4:
                foodBurned = Instantiate(burnedFood);
                foodBurned.name = FoodName;
                foodBurned.transform.position = FoodPosition;
                break;
        }
    }
}
