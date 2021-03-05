using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour {
    string myname;
    // Start is called before the first frame update
    void Start() {
        myname = transform.name;

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "CHECK") {
            CheckFood checkFood = other.transform.gameObject.GetComponent<CheckFood>();
            checkFood.dishFoodName = myname;
            checkFood.foodCheck();
            Destroy(gameObject, 0);
        }
    }
}
