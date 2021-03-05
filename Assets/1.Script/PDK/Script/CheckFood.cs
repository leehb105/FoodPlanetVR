using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckFood : MonoBehaviour {
    int mycustomerNum;
    int myNum;
    public TextMeshProUGUI myTimer;
    //1: 튜토리얼
    List<string> foodLevel1 = new List<string>() { "BlackDrink" };
    //2: 검정음료 초록음료 미니버거(빵 고기 빵)
    List<string> foodLevel2 = new List<string>() { "BlackDrink", "GreenDrink", "MiniBurger" };
    //3: 검정음료 초록음료 치즈버거(빵 고기 치즈 빵) 웰던스테이크
    List<string> foodLevel3 = new List<string>() { "BlackDrink", "GreenDrink", "CheeseBurger", "WelldoneSteak" };

    List<string> foodList;
    string orderName;
    float orderTime;
    public string dishFoodName;
    bool timeCheck;
    NPCCustomer npcCus;
    FoodManager parent;

    int stageLevel;

    // Start is called before the first frame update
    void Start() {
        parent = FoodManager.Instance;
        myTimer.text = "";
        myNum = transform.GetSiblingIndex();
        GetStageLevel();
    }

    // Update is called once per frame
    void Update() {
        if (timeCheck) {
            orderTime -= Time.deltaTime;
            myTimer.text = Mathf.FloorToInt(orderTime).ToString();
            if (orderTime <= 0) {
                npcCus.ChangeCustomerState(NPCCustomer.State.Bad);
                npcCus = null;
                timeCheck = false;
                myTimer.text = "";

            }
        }
    }

    void GetStageLevel() {
        stageLevel = GameManager.Instance.StageLevel;
        //Debug.Log("체크푸드 스테이지레벨: " + stageLevel);
        switch (stageLevel) {
            case 1:
                foodList = foodLevel1;
                break;
            case 2:
                foodList = foodLevel2;
                break;
            case 3:
                foodList = foodLevel3;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "CUSTOMER") {
            //Debug.Log("내이름: " + transform.name + ", 번호: " + CustomerNum);
            if (other.transform.gameObject.GetComponent<NPCCustomer>() == null) {
                //Debug.Log("NPC못가져옴");
            }
            npcCus = other.transform.gameObject.GetComponent<NPCCustomer>();
            //Debug.Log("이름: "+npcCus.name);
            mycustomerNum = npcCus.myCustomerNum;
            //해당 랜덤부분 가중치 설정 필요
            //Debug.Log("현재푸드리스트: "+foodList);
            orderName = foodList[Random.Range(0, foodList.Count)];
            //Debug.Log("주문한거:" + orderName);
            npcCus.myOrder = orderName;
            //Debug.Log(orderName);
            orderTime = parent.foodTimeDict[orderName];
            //Debug.Log("주문시간: " + orderTime);
            //Debug.Log("orderTime:" + orderTime);
            //Debug.Log(orderTime);
            parent.OnChildTriggerEnter(orderName, myNum, mycustomerNum); // pass the own collider and the one we've hit
            timeCheck = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "CUSTOMER") {
            parent.OnChildTriggerExit(myNum);
            myTimer.text = "";
            timeCheck = false;
            npcCus = null;
        }
    }
    public void foodCheck() {
        if (orderName == dishFoodName) {
            if (npcCus != null) {
                npcCus.FoodPrice = parent.foodPriceDict[orderName];
                npcCus.ChangeCustomerState(NPCCustomer.State.Good);
                myTimer.text = "";
                timeCheck = false;
                npcCus = null;
                dishFoodName = null;
                GameManager.Instance.RemainOrderTime += orderTime;
            }
        }
        else if (dishFoodName != orderName) {
            if (npcCus != null) {
                npcCus.ChangeCustomerState(NPCCustomer.State.Bad);
                myTimer.text = "";
                timeCheck = false;
                npcCus = null;
                dishFoodName = null;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "DISH" || other.transform.tag == "WATERCUP") {
            //Debug.Log("너가내놓은거이름: " + dishFoodName + ", 손님이주문한거이름: " + orderName);
            
        }
    }
}
