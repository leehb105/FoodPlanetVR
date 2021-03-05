using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCSpawnManager : MonoBehaviour {

    public static NPCSpawnManager Instance;
    public void Awake() {
        Instance = this;
    }

    public float currentTime;
    public float createtTime;
    public float startRandomCreateTime = 1f; //(public이라 인스펙터에서 적절한값으로 수정)
    public float startRandomEndTime = 1f; //(public이라 인스펙터에서 적절한값으로 수정)
    public List<int> emptyTableCheck = new List<int>();


    public List<GameObject> TableList = new List<GameObject>();
    public List<bool> emptyTableList = new List<bool>() { true, true, true };
    public GameObject npcCustomer;
    public GameObject npcRobber;
    int robberCount = 2;
    int customerTableNum;

    public bool checkRobberSpawn;
    public bool robberCheck = false;

    NPCCustomer npcCus;

    //orderNum은 스테이지에서 처리해야될 고객의 숫자
    int orderNum;

    //customerNum은 1부터 게임끝날때까지 쭉 늘어나는 숫자
    public int customerNum = 1;
    int stageLevel;
    public TextMeshProUGUI orderNumText;

    public GameObject npcSpawnPoint1, npcSpawnPoint2, npcSpawnPoint3;

    public int OrderNum {
        get { return orderNum; }
        set {
            orderNum = value;
            orderNumText.text = "x" + orderNum.ToString();
            if (orderNum == 5 || orderNum == 15) {
                robberCheck = true;
            }
            if (orderNum <= 0) {
                //Debug.Log("게임끝났다");
                GameManager.Instance.GameClear();
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        stageLevel = GameManager.Instance.StageLevel;
        createtTime = 4f;
        //혹시모를 빈테이블리스트 초기화
        for (int i = 0; i < emptyTableList.Count; i++) {
            emptyTableList[i] = true;
        }
        orderNum = GameManager.Instance.orderNumber;
        //Debug.Log("orderNum: " + orderNum + "GameManagerNumber: " + GameManager.Instance.orderNumber);
        OrderNum = orderNum;
    }

    //빈 테이블을 찾기위해 돌리는 랜덤함수
    private int GetRandomNumber() {
        var exclude = new HashSet<int>(emptyTableCheck);
        var range = Enumerable.Range(0, 3).Where(i => !exclude.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, 3 - exclude.Count);
        return range.ElementAt(index);
    }

    void SpawnCustomer() {
        //시작하자마자 빈 테이블을 찾음
        for (int i = 0; i < emptyTableList.Count; i++) {
            if (emptyTableList[i] == false) {
                emptyTableCheck.Add(i);
            }
        }
        customerTableNum = GetRandomNumber();

        GameObject customer = Instantiate(npcCustomer);
        npcCus = customer.transform.gameObject.GetComponent<NPCCustomer>();
        npcCus.MyTableNum = customerTableNum;
        switch (customerTableNum) {
            case 0:
                customer.transform.position = npcSpawnPoint1.transform.position;
                break;
            case 1:
                customer.transform.position = npcSpawnPoint2.transform.position;
                break;
            case 2:
                customer.transform.position = npcSpawnPoint3.transform.position;
                break;
            default:
                Debug.Log("spawnCustomer ERROR");
                break;
        }
        //customer.transform.position = transform.position;
        customer.transform.SetParent(transform);
        currentTime = 0;
        emptyTableCheck.Clear();
        createtTime = Random.Range(startRandomCreateTime, startRandomEndTime);
    }
    void SpawnRobber() {
        for (int i = 0; i < emptyTableList.Count; i++) {
            emptyTableList[i] = false;
        }
        //강도가 나오면 모든 customer의 state를 Run으로 변경
        for (int i = 0; i < transform.childCount; i++) {
            //Debug.Log("자식이름: " + transform.GetChild(i).name);
            if (transform.GetChild(i).name.Contains("Robber")) {
                return;
            }
            NPCCustomer customer = transform.GetChild(i).gameObject.GetComponent<NPCCustomer>();
            customer.state = NPCCustomer.State.Run;
        }
        GameObject robber = Instantiate(npcRobber);
        robber.transform.position = transform.position;
        robber.transform.parent = transform;
        GameManager.Instance.RobberCount++;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update() {
        if (stageLevel == 3) {
            if (robberCheck) {
                //if (robberCount>=1 && orderNum == 1 || orderNum == 4) {
                checkRobberSpawn = true;
            }
            else {
                checkRobberSpawn = false;
            }
        }
        if (emptyTableList.Contains(true) && orderNum > 0) {
            currentTime += Time.deltaTime;
            if (currentTime >= createtTime) {
                if (checkRobberSpawn) {
                    SpawnRobber();
                }
                //Debug.Log("currentTime: " + currentTime);
                else
                    SpawnCustomer();
            }
        }

    }
}
