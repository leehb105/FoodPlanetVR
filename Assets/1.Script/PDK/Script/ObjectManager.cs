using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour {
    public static ObjectManager Instance;
    public void Awake() {
        Instance = this;
    }
    public GameObject dishs, cups;
    public GameObject dishSpawnPoint, cupSpawnPoint;
    GameObject dish, cup;

    // Start is called before the first frame update
    void Start() {
        dish = Instantiate(dishs);
        dish.name = "Dish";
        dish.transform.position = dishSpawnPoint.transform.position + new Vector3(0, 0.03f, 0);

        cup = Instantiate(cups);
        cup.name = "Cup";
        cup.transform.position = cupSpawnPoint.transform.position + new Vector3(0, 0.03f, 0);
    }

    // Update is called once per frame
    void Update() {
    }

    public void ObjPrepping(Vector3 ObjPosition, string ObjName, int ObjType) {
        switch (ObjType) {
            case 0:
                dish = Instantiate(dishs);
                dish.name = ObjName;
                dish.transform.position = ObjPosition;
                break;
            case 1:
                cup = Instantiate(cups);
                cup.name = ObjName;
                cup.transform.position = ObjPosition;
                break;
        }
    }
}
