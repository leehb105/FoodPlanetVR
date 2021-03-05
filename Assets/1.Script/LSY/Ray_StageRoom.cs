using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ray_StageRoom : MonoBehaviour
{
    LineRenderer line;
    RaycastHit hitInfo;
    Ray ray;
    float raycastDistance = 10.0f;
    ButtonState btnState;
    ArrowState arrowState;
    GameObject temp;
    public GameObject gameOver;
    public GameObject gameResult;
    public GameObject gameMenu;

    // Start is called before the first frame update
    void Start() {
        line = GetComponent<LineRenderer>();

        btnState = FindObjectOfType<ButtonState>();
        arrowState = FindObjectOfType<ArrowState>();
    }

    // Update is called once per frame
    void Update() {
        int layer = 1 << LayerMask.NameToLayer("Ignore Raycast");
        line.SetPosition(0, transform.position);
        line.enabled = false;
        ray = new Ray(transform.position, transform.forward * 10);
        if (Physics.Raycast(ray, out hitInfo, 100, ~layer)) {

            if (gameOver.activeSelf || gameResult.activeSelf || gameMenu.activeSelf) {
                line.enabled = true;
                line.SetPosition(1, hitInfo.point);
                if (hitInfo.collider.gameObject.CompareTag("Button")) {
                    hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);
                    temp = hitInfo.collider.gameObject;
                }
                
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) {
                    if (hitInfo.collider.gameObject.CompareTag("Button")) {
                        SoundManager.soundMN.BtnSound();
                        hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke(); 
                        temp = hitInfo.collider.gameObject;
                    }
                }
            }





            /*if (hitInfo.collider.gameObject.CompareTag("Button")) {

                hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);
                temp = hitInfo.collider.gameObject;
            }
            if (hitInfo.collider.gameObject.CompareTag("Arrow")) {

                hitInfo.collider.gameObject.GetComponent<ArrowState>().SetButton(ArrowState.State.On);
                temp = hitInfo.collider.gameObject;
            }
            *//*else
            {
                hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.Idle);
            }*//*
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))//오른손 검지 트리거
            {
                if (hitInfo.collider.gameObject.CompareTag("Button")) {
                    //버튼 사운드를 재생한다
                    SoundManager.soundMN.BtnSound();
                    hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    temp = hitInfo.collider.gameObject;
                    //hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);

                    switch (hitInfo.transform.gameObject.name) {
                        case "Vol":
                            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                
            ("vo 999" + hitInfo.transform.gameObject.name);
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            break;
                        case "Vib":
                            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            break;
                        case "Close":
                            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            break;
                        case "Home":
                            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            break;
                        case "Restart":
                            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            break;
                        case "Exit":
                            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            break;
                    }

                }
                if (hitInfo.collider.gameObject.CompareTag("Arrow")) {
                    //버튼 사운드를 재생한다
                    SoundManager.soundMN.BtnSound();
                    hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    temp = hitInfo.collider.gameObject;
                    //hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);
                }
                switch (hitInfo.transform.gameObject.name) {
                    case "Vol":
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        *//*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                            Debug.Log("vo 999" + hitInfo.transform.gameObject.name);
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        }*//*
                        break;
                    case "Vib":
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        *//*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        }*//*
                        break;
                    case "Close":
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        *//*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        }*//*
                        break;
                    case "Home":
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        *//*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        }*//*
                        break;
                    case "Restart":
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        *//*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        }*//*
                        break;
                    case "Exit":
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        *//*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        }*//*
                        break;
                }
            }*/
                
        }

        else {
            line.SetPosition(1, transform.position + (transform.forward * raycastDistance));

            if (temp != null) {
                temp.GetComponent<ButtonState>().SetButton(ButtonState.State.Idle);

                temp = null;

            }


        }
    }
}
