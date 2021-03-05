using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawnPoint : MonoBehaviour {

    bool foodInCheck = true;
    public float foodWaitTime = 2f;
    public float foodSpawnTime;
    string myname;

    // Start is called before the first frame update
    void Start() {
        myname = transform.name;
        FoodSpawn();
        //foodInCheck = true;
    }

    // Update is called once per frame
    void Update() {
        if (!foodInCheck) {
            foodSpawnTime += Time.deltaTime;
            if (foodSpawnTime >= foodWaitTime) {
                //음식 소환
                FoodSpawn();
                foodInCheck = true;
                foodSpawnTime = 0.0f;
            }
        }
    }

    public void grab() {
    }

    void FoodSpawn() {

        switch (myname) {
            case "BreadSpawnPoint":
                IngredientsSpawnManager.Instance.FoodPrepping(this, transform.position + new Vector3(0, 0.03f, 0), "0Square_Rock", 0);
                //foodInCheck = true;
                break;
            case "MeatSpawnPoint":
                IngredientsSpawnManager.Instance.FoodPrepping(this,transform.position + new Vector3(0, 0.03f, 0), "0BIGMeat", 1);
                //foodInCheck = true;
                break;
            //case "LettuceSpawnPoint":
            //    IngredientsSpawnManager.Instance.FoodPrepping(this, transform.position + new Vector3(0, 0.15f, 0), "1Hamburger_lettuce", 2);
                //foodInCheck = true;
                break;
            case "CheeseSpawnPoint":
                IngredientsSpawnManager.Instance.FoodPrepping(this, transform.position + new Vector3(0, 0.03f, 0), "Cheese", 3);
                //foodInCheck = true;
                break;
            //case "BlackEyeSpawnPoint":
            //    IngredientsSpawnManager.Instance.FoodPrepping(this, transform.position + new Vector3(0, 0.1f, 0), "BlackEye", 5);
            //    break;
            //case "GreenEyeSpawnPoint":
            //    IngredientsSpawnManager.Instance.FoodPrepping(this, transform.position + new Vector3(0, 0.1f, 0), "GreenEye", 6);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "FOOD") {
            //Debug.Log("myname: " + myname + "들어온놈: " + other.transform.name);
            foodInCheck = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.transform.tag == "FOOD") {
            //Debug.Log("myname: " + myname + "나간놈: " + other.transform.name);
            foodInCheck = false;
        }
    }
}