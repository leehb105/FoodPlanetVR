using Mono.Data.Sqlite;
using System.Collections.Generic;
using System;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    //시작하면 한번 스테이지의 정보를 가져온다
    //스테이지 번호, 스테이지 별점-> LoadDB()
    //화살표를 통해 스테이지를 조절하면 그에 맞게 스테이지 정보를 변경한다

    public static StageManager instance;
    private void Awake()
    {
        instance = this;
    }
    private int stageStar;
    //private int stageLock;
    private int myStage = 1;
    //public List<int> stageLock = new List<int>();
    public List<int> stageScore = new List<int>();

    public int MyStage
    {
        get{ return myStage; }
        set{ myStage = value; }
    }
    GameObject textStage;
    [SerializeField] TextMeshProUGUI scoreText;
    //GameObject[] stars = new GameObject[3];
    //GameObject tempStars;
    //GameObject[] planets = new GameObject[5];
    GameObject tempPlanets;
    GameObject lockImg;
    StageNum stageNum;
    GameObject pos;
    public List<GameObject> planetList;

    // Start is called before the first frame update
    void Start()
    {
        //tempStars = GameObject.Find("Stars_Img");
        //tempPlanets = GameObject.Find("Planets");
        textStage = GameObject.Find("Planet_Text");
        lockImg = GameObject.Find("Lock_Img");
        UIManager.instance.SetStageBtn(myStage);
        /*if (UIManager.instance != null) {
           
        }*/
        

    }    
    // Update is called once per frame
    void Update()
    {
        SetStageInfo();

    }
    public void PreStage()//스테이지 이전
    {
        if (myStage > 1)
        {
            myStage--;
            UIManager.instance.BtnStart(myStage);
            UIManager.instance.SetStageBtn(myStage);
        }
    }
    public void NextStage()//스테이지 다음
    {
        if (myStage < 3)
        {
            myStage++;
            UIManager.instance.BtnStart(myStage); 
            UIManager.instance.SetStageBtn(myStage);
        }
    }
    /*public void SetPlanets()
    {
        *//*for(int i= 0; i< planets.Length; i++)//행성 정보를 넣어준다
        {
            planets[i] = tempPlanets.transform.GetChild(i).gameObject;
            planets[i].SetActive(false);
        }
        for (int j = 0; j < myStage; j++)//현재 스테이지의 행성을 활성화해준다
        {
            planets[j].SetActive(true);
        }
        for(int j = myStage; j<planets.Length; j++)//현재 스테이지를 제외하고 앞뒤로 검색해 행성을 비활성화 해준다
        {
            planets[j].SetActive(false);
        }
        for (int j = myStage - 2; j >= 0; j--)//현재 스테이지를 제외하고 앞뒤로 검색해 행성을 비활성화 해준다
        {
            planets[j].SetActive(false);
        }*//*
    }*/
    /*public void SetLockState()
    {
        if(stageLock[myStage-1] == 0)
        {
            lockImg.SetActive(false);
        }
        else
        {
            lockImg.SetActive(true);
        }
    }*/
    public void SetStageInfo()//스테이지 정보 갱신
    {
        switch (myStage)
        {
            case 1:
                textStage.GetComponent<TextMeshProUGUI>().text = "Tutorial";
                break;
            case 2:
                textStage.GetComponent<TextMeshProUGUI>().text = "Easy";
                break;
            case 3:
                textStage.GetComponent<TextMeshProUGUI>().text = "Hard";
                break;
        }
        //textStage.GetComponent<TextMeshProUGUI>().text = myStage.ToString();//스테이지 넘버 입력
        SetScore();
        //SetPlanets();
        //SetLockState();
    }
    
    public void SetScore()
    {
        switch (myStage)
        {
            case 2:
                scoreText.text = stageScore[myStage-1].ToString();
                break;
            case 3:
                scoreText.text = stageScore[myStage-1].ToString();
                break;
            default:
                break;
        }
        /*for(int i = 0; i < stageStars[myStage-1]; i++)
        {
            stars[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            stars[i].GetComponentInChildren<ParticleSystem>().Play();
        }
        for(int i = stageStars[myStage-1]; i < stars.Length ; i++)
        {
            stars[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.05f);
            stars[i].GetComponentInChildren<ParticleSystem>().Stop();
        }*/

    }

}
