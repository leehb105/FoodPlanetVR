using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawnPoint : MonoBehaviour {
    bool objectInCheck;
    public float objectWaitTime = 2f;
    public float objectSpawnTime;
    string myname;

    // Start is called before the first frame update
    void Start() {
        myname = transform.name;
    }

    // Update is called once per frame
    void Update() {
        if (!objectInCheck) {
            objectSpawnTime += Time.deltaTime;
            if (objectSpawnTime >= objectWaitTime) {
                ObjectSpawn();
                objectInCheck = true;
                objectSpawnTime = 0.0f;
            }
        }
    }

    void ObjectSpawn() {
        switch (myname) {
            case "DishSpawnPoint":
                ObjectManager.Instance.ObjPrepping(transform.position + new Vector3(0, 0.03f, 0), "Dish", 0);
                break;
            case "CupSpawnPoint":
                ObjectManager.Instance.ObjPrepping(transform.position + new Vector3(0, 0.03f, 0), "Cup", 1);
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "DISH" || other.transform.tag == "CUP") {
            //Debug.Log("myname: " + myname + "들어온놈: " + other.transform.name);
            objectInCheck = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.transform.tag == "DISH" || other.transform.tag == "CUP") {
            //Debug.Log("myname: " + myname + "나간놈: " + other.transform.name);
            objectInCheck = false;
        }
    }
}
