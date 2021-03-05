using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RayTest : MonoBehaviour {
    LineRenderer line;
    RaycastHit hitInfo;
    Ray ray;
    float raycastDistance = 10.0f;
    ButtonState btnState;
    ArrowState arrowState;
    GameObject temp;
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
        ray = new Ray(transform.position, transform.forward * 10);
        if (Physics.Raycast(ray, out hitInfo, 100, ~layer)) {
            line.enabled = true;
            line.SetPosition(1, hitInfo.point);
            //if (SceneManager.GetActiveScene().name.Equals("Intro")) {//씬이 인트로 일때
            //    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))//오른손 검지 트리거 클릭하면
            //{
            //        if (hitInfo.collider.gameObject.CompareTag("Button")) {//누른 물체 태그가 버튼이면
            //            //버튼 사운드를 재생한다
            //            BtnClickSound.instance.ClickSound();
            //            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
            //            temp = hitInfo.collider.gameObject;


            //        }
            //    }
            //}
            if (SceneManager.GetActiveScene().name.Equals("WaitingRoom")) {
                if (hitInfo.collider.gameObject.CompareTag("Button")) {

                    hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.On);
                    temp = hitInfo.collider.gameObject;
                }
                if (hitInfo.collider.gameObject.CompareTag("Arrow")) {

                    hitInfo.collider.gameObject.GetComponent<ArrowState>().SetButton(ArrowState.State.On);
                    temp = hitInfo.collider.gameObject;
                }
                /*else
                {
                    hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.Idle);
                }*/
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
                                    //Debug.Log("vo 999" + hitInfo.transform.gameObject.name);
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
                            /*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                Debug.Log("vo 999" + hitInfo.transform.gameObject.name);
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }*/
                            break;
                        case "Vib":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            /*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }*/
                            break;
                        case "Close":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            /*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }*/
                            break;
                        case "Home":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            /*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }*/
                            break;
                        case "Restart":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            /*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }*/
                            break;
                        case "Exit":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            /*if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                                hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            }*/
                            break;
                    }
                }
                switch (hitInfo.collider.gameObject.name)//닿은 오브젝트가 아이템, 장비이면 상세정보 ui창 팝업
                {
                    case "Heart":
                        //Debug.Log("heart on");
                        ItemManager.instance.onHeart();
                        ItemManager.instance.offClock();
                        UIManager.instance.InfoMsgOn(hitInfo.collider.gameObject.name);
                        //UIManager.instance.HitItem(hitInfo.collider.gameObject.name);
                        break;
                    case "Timer":
                        ItemManager.instance.onClock();
                        ItemManager.instance.offHeart();

                        UIManager.instance.InfoMsgOn(hitInfo.collider.gameObject.name);
                        //UIManager.instance.HitItem(hitInfo.collider.gameObject.name);
                        break;
                    case "HammerPos":
                        EquipManager.instance.OnEquip(hitInfo.collider.gameObject.name);
                        UIManager.instance.InfoMsgOn(hitInfo.collider.gameObject.name);
                        break;
                    case "KnifePos":
                        EquipManager.instance.OnEquip(hitInfo.collider.gameObject.name);
                        UIManager.instance.InfoMsgOn(hitInfo.collider.gameObject.name);
                        break;
                    case "GrillPos":
                        EquipManager.instance.OnEquip(hitInfo.collider.gameObject.name);
                        UIManager.instance.InfoMsgOn(hitInfo.collider.gameObject.name);
                        break;
                    case "Exit_Btn":
                        UIManager.instance.InfoMsgOn(hitInfo.collider.gameObject.name);
                        break;
                    case "Buy_Heart_Btn":
                        UIManager.instance.MoneyInfoMsg(hitInfo.collider.gameObject.name);
                        break;
                    case "Buy_Timer_Btn":
                        UIManager.instance.MoneyInfoMsg(hitInfo.collider.gameObject.name);
                        break;
                    case "Upgrade_Hammer_Btn":
                        UIManager.instance.MoneyInfoMsg(hitInfo.collider.gameObject.name);
                        break;
                    case "Upgrade_Knife_Btn":
                        UIManager.instance.MoneyInfoMsg(hitInfo.collider.gameObject.name);
                        break;
                    case "Upgrade_Grill_Btn":
                        UIManager.instance.MoneyInfoMsg(hitInfo.collider.gameObject.name);
                        break;
                    case "Right_Btn":
                        UIManager.instance.StageBtnText(hitInfo.collider.gameObject.name);
                        break;
                    case "Left_Btn":
                        UIManager.instance.StageBtnText(hitInfo.collider.gameObject.name);
                        break;
                    default:
                        //UIManager.instance.InfoMsgOff();

                        break;
                }
            }
        }

        else {
            line.SetPosition(1, transform.position + (transform.forward * raycastDistance));
            /*if (hitInfo.collider.gameObject.CompareTag("Button"))
            {
                hitInfo.collider.gameObject.GetComponent<ButtonState>().SetButton(ButtonState.State.Idle);
            }*/
            //btnState.SetButton(ButtonState.State.Idle);
            /*btnState.SetStateIdle();
            arrowState.SetStateIdle();*/
            if (temp != null) {
                /*btnState.SetStateIdle();
                arrowState.SetStateIdle();*/
                switch (temp.gameObject.tag) {
                    case "Button":
                        temp.GetComponent<ButtonState>().SetButton(ButtonState.State.Idle);
                        break;
                    case "Arrow":
                        temp.GetComponent<ArrowState>().SetButton(ArrowState.State.Idle);
                        break;
                }
                temp = null;

            }
            if (SceneManager.GetActiveScene().name.Equals("WaitingRoom")) {
                UIManager.instance.InfoMsgOff();
                UIManager.instance.MoneyInfoMsgOff();
                UIManager.instance.StageBtnTextOff();
                EquipManager.instance.OffEquip();
            }


        }
    }
}
