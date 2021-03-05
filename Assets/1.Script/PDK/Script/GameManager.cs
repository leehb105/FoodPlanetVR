using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public void Awake() {
        Instance = this;
    }

    public List<int> equipLevel = new List<int>();
    public List<int> itemStock = new List<int>();

    IDbConnection dbconn;
    IDbCommand dbcmd;
    IDataReader reader;
    public string conn;

    public float nowTime;

    public bool isGameEnd;

    public GameObject ToolManager;
    public GameObject NPCSpawnManager;
    public GameObject ItemManager;
    public GameObject IngredientsSpawnManager;
    public GameObject ObjectSpawnManager;
    public GameObject OrderTable;
    public GameObject FoodManual;

    public GameObject gameClearUI;
    public GameObject gameOverUI;

    public GameObject orderTable;

    public GameObject TutorialManager;

    public AudioSource audioSource;

    public GameObject startButton;


    //해당 스테이지레벨은 추후 start에서 송이꺼에서 값을 가져와야함
    int stageLevel;
    int complainCount = 5;
    int dailyProfit = 0;
    public int orderNumber;
    public float customerTime;
    float startTime;
    float endTime;
    public float playTime;
    float remainOrderTime = 0;
    public int startComplainCount;
    int endComplainCount;
    int checkComplainCount;
    public int compareScore;


    public TextMeshProUGUI complainText;
    public TextMeshProUGUI profitText;

    public GameObject stageEasy, stageHard;
    GameObject levelCheckObj;

    int robberCount;
    int killRobberCount;

    public int RobberCount {
        get { return robberCount; }
        set {
            robberCount = value;
        }
    }
    public int KillRobberCount {
        get { return killRobberCount; }
        set {
            killRobberCount = value;
        }
    }
    public float RemainOrderTime {
        get { return remainOrderTime; }
        set {
            remainOrderTime = value;
        }
    }
    public int StageLevel {
        get { return stageLevel; }
        set {
            stageLevel = value;
        }
    }
    public int Complain {
        get { return complainCount; }
        set {
            complainCount = value;
            complainText.text = "x" + complainCount.ToString();
        }
    }
    public int ComplainCheck {
        get { return checkComplainCount; }
    }
    //매출
    public int Profit {
        get { return dailyProfit; }
        set {
            dailyProfit = value;
            profitText.text = string.Format("{0:#,0}", dailyProfit);

        }
    }
    // Start is called before the first frame update
    void Start() {
        audioSource.GetComponent<AudioSource>();
        //스테이지레벨은 송이꺼에서 값을 가져와야함
        //StageLevel = 3;
        
        levelCheckObj = GameObject.Find("StageNum");
        StageLevel = levelCheckObj.GetComponent<StageNum>().StageNumber;
        
        //Debug.Log("스테이지레벨: " + stageLevel);
        //equipLevel.Clear();
        //itemStock.Clear();
        AccessDB();
        StageMenu(stageLevel);
        GetItemDB(conn, "SELECT ItemStock FROM Item");
        GetEquipDB(conn, "SELECT EquipLevel FROM Equip WHERE EquipLock = 1");
        GetOrderNumDB(conn, "SELECT OrderNum FROM StageOrderNum WHERE StageLevel=" + stageLevel);
        CompareScore(conn, "SELECT StageScore FROM Stage WHERE StageLevel=" + StageLevel);
        //Debug.Log("stageLevel: " + stageLevel);

        //스테이지 레벨 가져와야함
        //해당 Manager들은 버튼 클릭후 활성화로 변경필요

        //시작시간 기록
        startTime = Time.realtimeSinceStartup;
        if (stageLevel == 1) {
            ToolManager.SetActive(true);
            ItemManager.SetActive(false);
            IngredientsSpawnManager.SetActive(true);
            ObjectSpawnManager.SetActive(true);
            OrderTable.SetActive(true);
            FoodManual.SetActive(true);
            NPCSpawnManager.SetActive(false);
            TutorialManager.SetActive(true);
            startButton.SetActive(false);
            orderTable.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        //테스트용 시간제어
        //Time.timeScale = nowTime;
    }
    void StageMenu(int stageLevel) {
        switch (stageLevel) {
            case 1:
                break;
            case 2:
                stageEasy.SetActive(true);
                break;
            case 3:
                stageHard.SetActive(true);
                break;
        }
    }
    void CheckGameOver() {
        ToolManager.SetActive(false);
        NPCSpawnManager.SetActive(false);
        ItemManager.SetActive(false);
        IngredientsSpawnManager.SetActive(false);
        ObjectSpawnManager.SetActive(false);
        OrderTable.SetActive(false);
        FoodManual.SetActive(false);
        orderTable.SetActive(false);
    }
    public void CheckGameStart() {
        audioSource.Play();
        ToolManager.SetActive(true);
        ItemManager.SetActive(true);
        IngredientsSpawnManager.SetActive(true);
        ObjectSpawnManager.SetActive(true);
        OrderTable.SetActive(true);
        FoodManual.SetActive(true);
        NPCSpawnManager.SetActive(true);
        TutorialManager.SetActive(false);
        orderTable.SetActive(true);
    }

    public void GameOver() {
        isGameEnd = true;
        //테스트용 시간측정
        endTime = Time.realtimeSinceStartup;
        //나머지 기능들 다 FALSE로 변경
        CheckGameOver();
        //셔터 내리고

        //값 전달은 필요없음
        //은진이 게임오버 UI 호출
        gameOverUI.SetActive(true);
        playTime = endTime - startTime;
    }
    public void GameClear() {
        isGameEnd = true;
        endTime = Time.realtimeSinceStartup;
        //나머지 기능들 다 FALSE로 변경
        CheckGameOver();
        //셔터 내리고 

        //컴플레인갯수, 강도수, 퇴치한 강도수, 전체 플레이타임, 주문시간 전달
        playTime = endTime - startTime;
        endComplainCount = Complain;
        checkComplainCount = startComplainCount - endComplainCount;
        gameClearUI.SetActive(true);
        //complainCount
        //remainOrderTime
        //은진이 게임클리어 UI 호출
    }


    void AccessDB() {
        string filePath = string.Empty;
        //DB 주소 가져오기
        if (Application.platform == RuntimePlatform.Android) {//안드로이드
            filePath = Application.persistentDataPath + "/DB.db";
            if (!File.Exists(filePath)) {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DB.db");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }//안씀, 단 무한루프가 날수있으므로 타이머나 예외처리를 해야한다는데 뭘넣어야할까
                File.WriteAllBytes(filePath, loadDB.bytes);
            }
        }
        else { //플랫폼이 여러가지 경우가 있겠지만 지금은 테스트를 위해 안드로이드(오큘러스) 아니면 윈도우인거로 상정
            filePath = Application.dataPath + "/StreamingAssets/DB.db";
            if (!File.Exists(filePath)) {
                File.Copy(Application.streamingAssetsPath + "/DB.db", filePath);
            }
        }

        try {
            conn = "URI=file:" + filePath;
            dbconn = (IDbConnection)new SqliteConnection(conn);
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    private void GetOrderNumDB(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            while (reader.Read()) {
                //int level = reader.GetInt32(0);
                //Debug.Log("EquipLevel =" + level);
                orderNumber = reader.GetInt32(0);
                //Debug.Log("orderNumber =" + orderNumber);

            }
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }
    void GetEquipDB(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            while (reader.Read()) {
                equipLevel.Add(reader.GetInt32(0));
            }
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    void GetItemDB(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            while (reader.Read()) {
                itemStock.Add(reader.GetInt32(0));
                //Debug.Log("itemStock: " + itemStock);
            }
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }
    public void SetItemDB(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    public void SetResultMoney(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    public void SetResultScore(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    public void CompareScore(string conn, string query) {
        try {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            while (reader.Read()) {
                compareScore = reader.GetInt32(0);
            }
            DBClose();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    void DBClose() {
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        //dbconn = null;
    }
}
