using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCup_Change : MonoBehaviour {
    public GameObject BlackWaterCup;
    public GameObject GreenWaterCup;

    GameObject cupFactory;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "FOOD") {
            if (other.transform.name == "BlackEye") {
                Destroy(gameObject, 0);
                Destroy(other.gameObject, 0);
                cupFactory = Instantiate(BlackWaterCup);
                cupFactory.name = BlackWaterCup.name;
                cupFactory.transform.position = transform.position;

            }
            else if (other.transform.name == "GreenEye") {
                Destroy(gameObject, 0);
                Destroy(other.gameObject, 0);
                cupFactory = Instantiate(GreenWaterCup);
                cupFactory.name = GreenWaterCup.name;
                cupFactory.transform.position = transform.position;

            }
        }
    }
}
