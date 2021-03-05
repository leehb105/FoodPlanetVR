using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    /*public Text a;
    public Text b;
    public Text c;
    public Text d;*/

    [SerializeField] LineRenderer layser;        // 레이저
    private RaycastHit hitInfo; // 충돌된 객체
    private GameObject currentObject;   // 가장 최근에 충돌한 객체를 저장하기 위한 객체
    Ray ray;
    public float raycastDistance = 10.0f; // 레이저 포인터 감지 거리

    // Start is called before the first frame update
    void Start()
    {
        //layser = GetComponent<LineRenderer>();
    }

    Item hitItem;
    // Update is called once per frame\
    void Update()
    {
        layser.SetPosition(0, transform.position); // 첫번째 시작점 위치
                                                   // 업데이트에 넣어 줌으로써, 플레이어가 이동하면 이동을 따라가게 된다.
                                                   //  선 만들기(충돌 감지를 위한)                                                   
                                                   //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);
        ray = new Ray(transform.position, transform.forward * 100);
        // 충돌 감지 시
        int layer = 1 << LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(ray, out hitInfo, 100, ~layer))
        {
            layser.enabled = true;//충돌 잇으면 켜지게
            layser.SetPosition(1, hitInfo.point);
            //
            if (SceneManager.GetActiveScene().name == "Intro")//인트로 씬일경우 실행
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                {
                    if (hitInfo.collider.gameObject.CompareTag("Button"))
                    {
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                }
            }
            //
            //return;
            if (SceneManager.GetActiveScene().name == "WaitingRoom")
            {//대기룸 씬일 경우 실행
             // 오큘러스 퀘스트 트리거 누를 경우
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))//오른손 검지 트리거
                {
                    // 충돌 객체의 태그가 Button인 경우
                    if (hitInfo.collider.gameObject.CompareTag("Button"))
                    {
                        //버튼 사운드를 재생한다
                        SoundManager.soundMN.BtnSound();
                        // 버튼에 등록된 onClick 메소드를 실행한다.
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    switch (hitInfo.collider.gameObject.name)//버튼을 클릭하고 메뉴 버튼들을 누르면
                    {
                        //심은진
                        case "Vol":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                        case "Vib":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                        case "Close":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                        //이 부분 다르씬에서 실행해야 할 것 같음
                        case "Home":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                        case "Button":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                        case "Restart":
                            hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                            //
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
        else
        {
            //layser.enabled = false;//충돌 없으면 꺼지게
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));
            UIManager.instance.InfoMsgOff();
            UIManager.instance.MoneyInfoMsgOff();
            UIManager.instance.StageBtnTextOff();
            EquipManager.instance.OffEquip();
        }
    }


    public void compareItems(GameObject go)
    { 
    
    
    }

    ///////
    /*RaycastHit hitInfo;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Ray()
    {
        ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hitInfo, 10.0f))
        {
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Button":
                    if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        hitInfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    break;
            }
        }
    }*/
}
