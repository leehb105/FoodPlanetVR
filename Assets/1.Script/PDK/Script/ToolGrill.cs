using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolGrill : MonoBehaviour {
    float bakeTime;
    //public Rigidbody rb;
    bool collisionDetect = false;
    GameObject detectObject;
    FoodScript food;

    public GameObject smokeParticle;
    GameObject particleFactory;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision other) {
        //Debug.Log("충돌발생" + other.gameObject.name);
        if (other.transform.tag == "FOOD") {
            food = other.transform.gameObject.GetComponent<FoodScript>();
            //transform.GetComponent<Rigidbody>().useGravity = false;
            food.GrillDetect = true;

            particleFactory = Instantiate(smokeParticle);
            particleFactory.transform.position = other.transform.position + new Vector3(0, 0.1f, 0);
            food.onSmoke(particleFactory);
        }
    }

    private void OnCollisionExit(Collision other) {
        //Debug.Log("충돌해제" + other.gameObject.name);
        if (other.transform.tag == "FOOD") {
            //transform.GetComponent<Rigidbody>().useGravity = true;
            if (food != null) {
                food.disableSmoke();
                food.GrillDetect = false;
                food = null;
            }
        }
    }

}