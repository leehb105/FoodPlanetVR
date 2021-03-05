using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class NPCCustomer : MonoBehaviour {
    public enum State {
        Search,
        Move,
        Wait,
        Good,
        Bad,
        Run
    }
    public State state;

    //0:enter, 1:order, 2:good, 3:bad, 4:exit
    AudioSource audioSource;

    public float speed = 5.0f;  //이동속도(public이라 인스펙터에서 적절한값으로 수정)
    Vector3 dir;                //이동할 방향

    public Animator anim;
    public GameObject targetObject;
    public Canvas myCanvas;
    public GameObject myPanel;
    public string myOrder;
    CustomerAudioManager myAudioManager;

    public TextMeshProUGUI foodPay;

    public GameObject happyParticle;
    public GameObject angryParticle;

    //
    public GameObject goodEffect;
    public GameObject badEffect;



    int foodPrice;
    public int FoodPrice {
        get { return foodPrice; }
        set {
            foodPrice = value;
        }
    }
    int myTableNum;
    public int MyTableNum {
        get { return myTableNum; }
        set {
            myTableNum = value;
        }
    }

    public int myCustomerNum;
    string exitName;

    private void Awake() {
        goodEffect.SetActive(false);
        badEffect.SetActive(false);
    }

    void Start() {
        myAudioManager = transform.GetChild(0).GetComponent<CustomerAudioManager>();
        anim = GetComponent<Animator>();
        state = State.Search;
        //emptyTableCheck.Clear();
        //내 손님번호는 NPCSpawnManager에서 가져온후 뒤의 고객을위해 증가시킴
        myCustomerNum = NPCSpawnManager.Instance.customerNum++;
        switch (myTableNum) {
            case 0:
                exitName = "EXIT1";
                break;
            case 1:
                exitName = "EXIT2";
                break;
            case 2:
                exitName = "EXIT3";
                break;
            default:
                Debug.Log("checkExit ERROR");
                break;
        }
    }
    // Update is called once per frame
    void Update() {
        //Debug.Log("myname: "+transform.name +", myState: " + state);
        switch (state) {
            case State.Search: UpdateSearch(); break;
            case State.Move: UpdateMove(); break;
            //case State.Wait: UpdateWait(); break;
            //case State.Good: UpdateGood(); break;
            case State.Run: UpdateRun(); break;
            default: break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "CHECK") {


            //checkFoodChild에서 order상태로 변경
            //state = State.Wait;
            ChangeCustomerState(State.Wait);
        }
        if (other.tag == "EXIT") {
            if (GameObject.FindWithTag("ROBBER")) {
                Destroy(gameObject, 0);
            }
            else {
                NPCSpawnManager.Instance.emptyTableList[myTableNum] = true;
                Destroy(gameObject, 0);
            }
        }
    }

    // - 목적지를 찾는 상태
    private void UpdateSearch() {
        targetObject = NPCSpawnManager.Instance.TableList[myTableNum];
        //Debug.Log("myTableNum: " + myTableNum + ", targetObject: " + targetObject.name);
        NPCSpawnManager.Instance.emptyTableList[myTableNum] = false;
        if (targetObject != null) {
            //이동상태로 전이
            state = State.Move;
        }
        myAudioManager.playSound(CustomerAudioManager.State.Enter);
    }
    private void UpdateMove() {
        //customer의 목적지를 타겟으로
        dir = targetObject.transform.position - transform.position;
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
        //움직이는 애니메이션도 필요
    }
    private void UpdateGood() {
        //돈 올리고 손님카운트 1개 제거
        //근데 음식별 돈이 또 따로 있음
        happyParticle.SetActive(true);
        myCanvas.gameObject.SetActive(false);
        //좋은 애니메이션
        foodPay.text = "+" + foodPrice.ToString();
        anim.SetTrigger("OrderGood");
        goodEffect.SetActive(true);
        myAudioManager.playSound(CustomerAudioManager.State.Good);
    }
    public void UpdateGoodFinish() {

        anim.SetTrigger("OrderFinish");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //포탈로 돌아가야하므로 타겟오브젝트 포탈로
        targetObject = GameObject.Find(exitName);
        //Debug.Log("targetObject: " + targetObject);
        GameManager.Instance.Profit += foodPrice;
        NPCSpawnManager.Instance.OrderNum--;
        //그러고 이동
        state = State.Move;
        //이동하면서 CheckFood랑 ontriggerExit되면 숫자를 초기화
    }
    private void UpdateBad() {
        //state = State.Bad;
        angryParticle.SetActive(true);
        myCanvas.gameObject.SetActive(false);
        //나쁜 애니메이션
        anim.SetTrigger("OrderBad");
        badEffect.SetActive(true);
        myAudioManager.playSound(CustomerAudioManager.State.Bad);

    }
    public void UpdateBadFinish() {
        anim.SetTrigger("OrderFinish");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        targetObject = GameObject.Find(exitName);
        //Debug.Log("targetObject: " + targetObject);
        GameManager.Instance.Complain -= 1;
        if (GameManager.Instance.Complain <= 0) {
            GameManager.Instance.GameOver();
        }
        state = State.Move;

    }

    private void UpdateRun() {
        //속도 빠르게하고
        speed = 15f;
        targetObject = GameObject.Find(exitName);
        anim.SetTrigger("Run");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        state = State.Move;
    }

    public void ChangeCustomerState(State st) {
        state = st;
        switch (state) {
            case State.Search:

                break;
            case State.Move: break;
            case State.Wait:
                anim.SetTrigger("OrderWait");
                myAudioManager.playSound(CustomerAudioManager.State.Order);
                //Debug.Log("myorder: " + myOrder);
                myCanvas.gameObject.SetActive(true);
                myPanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("OrderImage/" + myOrder.ToString());

                break;
            case State.Good:
                UpdateGood();
                break;
            case State.Bad:
                UpdateBad();
                break;
            case State.Run:
                UpdateRun();
                break;
            default:
                break;
        }
    }
}


