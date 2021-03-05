using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCRobber : MonoBehaviour {
    public enum State {
        Search,
        Move,
        Attack,
        Run,
        Die
    }
    public State state;
    public Animator anim;
    bool checkDie = false;

    GameObject targetObject;
    public Slider sliderHP;
    BoxCollider myboxcollider;

    public GameObject goodEffect;
    public GameObject badEffect;

    public GameObject leftAlarm, rightAlarm;


    // - 현재체력
    int curHP;
    // - 최대체력
    public int maxHP;

    public int HP {
        get { return curHP; }
        set {
            curHP = Mathf.Max(0, value);
            sliderHP.value = curHP;
        }
    }

    public float speed = 3.0f;  //이동속도(public이라 인스펙터에서 적절한값으로 수정)
    Vector3 dir;                //이동할 방향

    float runTime = 14f; //10초뒤 도망
    float currentTime; //증감시킬값

    private void Awake() {
        goodEffect.SetActive(false);
        badEffect.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        maxHP = 3;
        state = State.Search;

        curHP = maxHP;
        sliderHP.maxValue = maxHP;
        sliderHP.value = curHP;

        myboxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {

        switch (state) {
            case State.Search: UpdateSearch(); break;
            case State.Move: UpdateMove(); break;
            case State.Attack: UpdateAttack(); break;
            //case State.Run: UpdateRun(); break;
            //case State.Die: UpdateDie(); break;
            default: break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "ROBBERSPOT") {//ROBBERSPOT에 닿으면(도착하면) Avoid 상태로 전이
            //Debug.Log(other.name);
            ChangeRobberState(State.Attack);
        }
        if (other.transform.tag == "EXIT") {
            //print("exit 진입");
            for (int i = 0; i < NPCSpawnManager.Instance.emptyTableList.Count; i++) {
                NPCSpawnManager.Instance.emptyTableList[i] = true;
            }
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {

        //if (other.transform.tag == "CUP" || other.transform.tag == "DISH") {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objects")) {

            //맞는 애니메이션 필요
            //Destroy(other.gameObject, 0);
            anim.SetTrigger("Hit");
            HP -= 1;
            if (curHP <= 0) {
                ChangeRobberState(State.Die);
                myboxcollider.enabled = false;
                leftAlarm.SetActive(false);
                rightAlarm.SetActive(false);
            }
        }
    }

    private void UpdateSearch() {
        targetObject = GameObject.Find("RobberSpot");
        //타겟이 null이 아니면
        if (targetObject != null) {
            //이동상태로 전이
            state = State.Move;
        }
    }
    private void UpdateMove() {//포탈에서 입장하는거
        dir = targetObject.transform.position - transform.position;
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
    }
    private void UpdateAttack() { //입장하고나서 좌우로 피하는거
        if (!checkDie) {
            currentTime += Time.deltaTime;
            if (currentTime > runTime) {
                //Debug.Log("강도 currentTime: " + currentTime);
                ChangeRobberState(State.Run);
                currentTime = 0;
            }
        }
    }
    private void UpdateRun() {
        NPCSpawnManager.Instance.robberCheck = false;
        badEffect.SetActive(true);
        anim.SetTrigger("RobberWin");
        targetObject = GameObject.Find("EXIT2");
        //GameManager.Instance.Profit 감소
        if (GameManager.Instance.Profit >= 500) {
            GameManager.Instance.Profit -= 500;
        }
        //GameManager.Instance.Complain 감소
        GameManager.Instance.Complain--;
        if (GameManager.Instance.Complain <= 0) {
            GameManager.Instance.GameOver();
        }
        if (targetObject != null) {
            NPCSpawnManager.Instance.OrderNum--;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            state = State.Move;
        }
    }

    public void RobberDeath() {
        NPCSpawnManager.Instance.robberCheck = false;
        for (int i = 0; i < NPCSpawnManager.Instance.emptyTableList.Count; i++) {
            NPCSpawnManager.Instance.emptyTableList[i] = true;
        }
        goodEffect.SetActive(true);
    }
    public void RobberDeathFinish() {
        GameManager.Instance.KillRobberCount++;
        GameManager.Instance.Profit += 500;
        NPCSpawnManager.Instance.OrderNum--;
        Destroy(gameObject, 0);
    }

    public void ChangeRobberState(State st) {
        state = st;
        switch (state) {
            case State.Search:
                break;
            case State.Move:
                break;
            case State.Attack:
                anim.SetTrigger("Attack");
                state = State.Attack;
                break;
            case State.Run:
                UpdateRun();
                break;
            case State Die:
                checkDie = true;
                anim.SetTrigger("Die");
                break;
            default:
                break;

        }
    }
}
