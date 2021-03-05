using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ClickSystem : MonoBehaviour {
    bool isPlay;
    bool isOn;
    public GameObject vibrationManager;
    public GameObject menu;
    public TextMeshProUGUI volText;
    public TextMeshProUGUI vibText;
    public GameObject exit;
    //public GameObject uiHelpers;


    string filepath;
    IDataReader reader;
    string temp_path;
    SqliteConnection con;
    IDbCommand dbcmd;
    string sqlQuery;
    void Start() {
        SetDBPath();

        exit.SetActive(false);

        Debug.Log("Player : " + PlayerPrefs.GetInt("BGM"));
        menu.SetActive(true);
        if (PlayerPrefs.GetInt("BGM") == 1) {
            Debug.Log("BGM !!! ON");
            //SoundManager.soundMN.PlaySound();
            UpdateSystemDB("Volume", 1);
            //Debug.Log("set volume == 1");
            volText.text = "BGM On";
            Debug.Log("BGM" + volText.text);
            //Debug.Log("vol On");
            DB.instance.Volume = 1;
        }
        else {
            SoundManager.soundMN.StopSound();
            UpdateSystemDB("Volume", 0);
            //Debug.Log("set volume == 0");
            volText.text = "BGM Off";
            //Debug.Log("vol Off");
            DB.instance.Volume = 0;
        }
        if (PlayerPrefs.GetInt("Vib") == 1) {
            vibrationManager.SetActive(true);
            UpdateSystemDB("Vibration", 1);
            vibText.text = "Viration On";
            DB.instance.Vibration = 1;
        }
        else {
            vibrationManager.SetActive(false);
            UpdateSystemDB("Vibration", 0);
            vibText.text = "Viration Off";
            DB.instance.Vibration = 0;
        }
        menu.SetActive(false);

    }

    void Update() {

    }


    //볼륨 버튼 
    public void OnClickVolume() {
        if (DB.instance.Volume == 0) {
            Debug.Log("vo 1");
            //만약 db에서 가져온 volume값이 1이면, sound를 재생하고싶다.
            //SoundManager.soundMN.GetComponent<AudioSource>().Play();
            SoundManager.soundMN.PlaySound();
            UpdateSystemDB("Volume", 1);
            //Debug.Log("set volume == 1");
            volText.text = "BGM On";
            //Debug.Log("vol On");
            DB.instance.Volume = 1;
            PlayerPrefs.SetInt("BGM", 1);
        }
        else //if (DB.instance.Volume == 1)
        {
            Debug.Log("vo 0");
            //만약 db에서 가져온 volume 값이 0이면 sound를 정지하고싶다.
            //SoundManager.soundMN.GetComponent<AudioSource>().Stop();
            SoundManager.soundMN.StopSound();
            UpdateSystemDB("Volume", 0);
            //Debug.Log("set volume == 0");
            volText.text = "BGM Off";
            //Debug.Log("vol Off");
            DB.instance.Volume = 0;
            PlayerPrefs.SetInt("BGM", 0);
        }
    }

    public void OnClickExit() {
        exit.SetActive(true);
    }

    //진동 버튼 
    public void OnClickVib() {
        if (DB.instance.Vibration == 0) {
            vibrationManager.SetActive(true);
            UpdateSystemDB("Vibration", 1);
            vibText.text = "Viration On";
            DB.instance.Vibration = 1;
            isOn = true;
            PlayerPrefs.SetInt("Vib", 1);
        }
        else if (DB.instance.Vibration == 1) {
            vibrationManager.SetActive(false);
            UpdateSystemDB("Vibration", 0);
            vibText.text = "Viration Off";
            DB.instance.Vibration = 0;
            isOn = false;
            PlayerPrefs.SetInt("Vib", 0);
        }
    }
    //종료버튼 
    public void OnClickClose() {
        menu.SetActive(false);
        //UIHelpers의 컴포넌트들을 disable하고싶다. 
        /*uiHelpers.GetComponent<LaserPointer>().enabled = false;
        uiHelpers.GetComponent<LineRenderer>().enabled = false;*/
    }
    //Waiting Room으로 버튼
    public void OnClickWaitingRoom() {
        Destroy(GameObject.Find("StageNum"), 0);
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());
    }
    public void UpdateSystemDB(string volvib, int a) //디비 오픈하고 쿼리문으로 제어하는 곳
    {
        try {
            con.Open(); //DB접속
            dbcmd = con.CreateCommand(); //여기부터 sql입력을 위한 코드 
            sqlQuery = string.Empty; //내가 입력할 명령어를 위한 값 = 비운다

            //sqlQuery = "SELECT Volume, Vibration FROM SystemOption";//aa를 조회한다 ㅇㅇ에서 //내가 건들 곳
            sqlQuery = "UPDATE SystemOption SET " + volvib + " = " + a;//aa를 한다 ㅇㅇ에서 //내가 건들 곳
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            reader.Close();

            //
            con.Close();
        }
        catch (Exception e) {
            print(e);
        }

    }

    public void SetDBPath()//디비 경로 잡아주는 곳
    {
        filepath = string.Empty;
        if (Application.platform == RuntimePlatform.Android)//실행플랫폼이 안드로이드일 경우
        {
            //안드로이드 일 경우
            filepath = Application.persistentDataPath + "/DB.db";
            if (!File.Exists(filepath)) {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DB.db");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }
        }
        else {
            //윈도우 일 경우
            filepath = Application.dataPath + "/StreamingAssets/DB.db";
            if (!File.Exists(filepath)) {
                File.Copy(Application.streamingAssetsPath + "/DB.db", filepath);
                //print(filepath);
            }
        }
        try {
            temp_path = "URI=file:" + filepath;
            con = new SqliteConnection(temp_path);


        }
        catch (Exception e) {
            print(e);
        }
    }
}
