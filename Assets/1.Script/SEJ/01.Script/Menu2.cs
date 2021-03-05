using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour {
    
    public static Menu2 instance;
    private void Awake() {
        instance = this;
    }

    bool menuYN;
    public GameObject menu2;
    //public GameObject uiHelpers;
    //public GameObject pos;

    OVRGrabber ovrGrab;



    void Start() {
        menu2.SetActive(false);
    }

    void Update() {
        if (GameManager.Instance.isGameEnd == true) {
            return;
        }

        transform.position = Camera.main.transform.position + Vector3.forward;
        //print("pos" + transform.position);
        //print("MenuPos" + pos.transform.position);
        if (SceneManager.GetActiveScene().name == "StageRoom") {
            if (OVRInput.GetDown(OVRInput.Button.Start)) {
                //메뉴 UI가 false일 때 
                if (!menuYN) {
                    //uiHelpers.SetActive(true);
                    menu2.SetActive(true);
                    print("메뉴켜짐");
                    menuYN = true;
                }
                //메뉴 UI가 true일 때
                else {
                    //uiHelpers.SetActive(false);
                    
                    menu2.SetActive(false);
                    print("메뉴꺼짐");
                    menuYN = false;
                }
            }
        }
    }
}
