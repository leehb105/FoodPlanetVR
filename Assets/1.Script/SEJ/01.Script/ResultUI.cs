using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

//컴플레인, 플레이 타임, 강도 처치 수 , 총 수익

public class ResultUI : MonoBehaviour {
    public static ResultUI instance;
    private void Awake() {
        instance = this;
    }
    public GameObject resultUI;
    public TextMeshProUGUI txtProfit;
    public TextMeshProUGUI txtRobber;
    public TextMeshProUGUI txtComplain;
    public TextMeshProUGUI txtPlayTime;
    public TextMeshProUGUI txtTips;
    public TextMeshProUGUI txtScore;
    float r; //robber
    float p; //playtime
    float complainScore;
    float playtimeScore;
    public float score;
    float tip;
    int finalProfit;
    int dbScore;

    public GameObject laserPointer;


    void Start() {
        ResultData();
        //DB.instance.DailyMoney = GameManager.Instance.Profit + tip;
    }

    void Update() {
        if (resultUI.activeSelf == true) {
            laserPointer.SetActive(true);
        }
        else {
            laserPointer.SetActive(false);
        }
    }

    //Home으로 돌아가는 버튼
    public void OnClickHome() {
        Destroy(GameObject.Find("StageNum"), 0);
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());

    }

    //재시작하는 버튼  
    public void OnClickRestart() {
        FadeInOut.fadeInOut.loadStageRoom = true;
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());
    }

    public void ResultData() {
        dbScore = GameManager.Instance.compareScore;
        //총 수익
        txtProfit.text = GameManager.Instance.Profit.ToString();
        //컴플레인 수 : 컴플레인개수(50%)컴플레인1개->50% 컴플레인2개->40%, 컴플레인3개부터->30%
        switch (GameManager.Instance.ComplainCheck) {
            case 0:
                complainScore = 50;
                break;
            case 1:
                complainScore = 50;
                break;
            case 2:
                complainScore = 40;
                break;
            case 3:
                complainScore = 30;
                break;
            case 4:
                complainScore = 20;
                break;
            default:
                complainScore = 10;
                break;
        }
        txtComplain.text = GameManager.Instance.ComplainCheck.ToString() + "개";

        //강도 출현 수 : 강도처치여부(30%): 잡은강도수/전체강도수*30%
        if (GameManager.Instance.StageLevel == 2) {
            r = 30;
            txtRobber.text = "EASY";
        }
        else {
            r = (GameManager.Instance.KillRobberCount / GameManager.Instance.RobberCount) * 30;

            txtRobber.text = GameManager.Instance.KillRobberCount.ToString() + "/" + GameManager.Instance.RobberCount + " 명 처치";
        }


        // 플레이 시간 : 플레이타임비율계산(20%) = A : 게임중 발생한 주문음식의 총 시간계산 / B : 전체 플레이타임
        // B/A=0.7 ~ : 20% // B/A=0.8 ~ : 15% // B/A=0.9 ~ : 10%
        p = GameManager.Instance.RemainOrderTime / GameManager.Instance.playTime;

        if (p >= 0.9f) {
            playtimeScore = 10f;
        }
        else if (p >= 0.8f) {
            playtimeScore = 15f;
        }
        else if (p >= 0.7f) {
            playtimeScore = 20;
        }

        txtPlayTime.text = string.Format("{0:F0}", (GameManager.Instance.playTime / 60)) + "분" + string.Format("{0:F0}", (GameManager.Instance.playTime % 60)) + "초";

        score = (complainScore + playtimeScore + r) * 10;
        if (score >= 900) {
            tip = Mathf.Round(GameManager.Instance.Profit * 0.2f);
            txtTips.text = string.Format("{0:F0}", tip);
        }
        else if (score >= 650) {
            tip = Mathf.Round(GameManager.Instance.Profit * 0.1f);
            txtTips.text = string.Format("{0:F0}", tip);
        }
        else {
            tip = Mathf.Round(GameManager.Instance.Profit * 0.05f);
            txtTips.text = string.Format("{0:F0}", tip);
        }
        finalProfit = GameManager.Instance.Profit + (int)tip;
        txtScore.text = score.ToString();

        GameManager.Instance.SetResultMoney(GameManager.Instance.conn, "UPDATE Money SET MyMoney = MyMoney + " + finalProfit);
        if (score > dbScore) {
            GameManager.Instance.SetResultScore(GameManager.Instance.conn, "UPDATE Stage SET StageScore = " + score + " WHERE StageLevel is " + GameManager.Instance.StageLevel);
        }
    }
}
