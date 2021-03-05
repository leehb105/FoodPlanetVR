using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class DB : MonoBehaviour {
    public static DB instance;
    private void Awake() {
        instance = this;
        SetDBPath();
        LoadSystemDB();
    }

    string filepath;
    IDataReader reader;
    string temp_path;
    SqliteConnection con;
    IDbCommand dbcmd;
    string sqlQuery;
    int volume;
    int vibration;
    //int myMoney;
    //int dailyMoney;
    public int Vibration {
        get { return vibration; }
        set { vibration = value; }
    }
    int stageStar;
    public int Volume {
        get { return volume; }
        set { volume = value; }
    }
    /*public int MyMoney {
        get { return myMoney; }
        set { myMoney = value; }
    }*/
    /*public int DailyMoney {
        get { return dailyMoney; }
        set { dailyMoney = value; }
    }*/
    void Start() {

    }
    public void LoadSystemDB()//디비 오픈하고 쿼리문으로 제어하는 곳
    {
        try {
            con.Open();
            dbcmd = con.CreateCommand();//여기부터 sql입력을 위한 코드 
            sqlQuery = string.Empty;

            //sqlQuery = "SELECT VolumVolume, Vibratione, Vibration FROM SystemOption";//aa를 조회한다 ㅇㅇ에서 //내가 건들 곳
            sqlQuery = "SELECT Volume, Vibration FROM SystemOption";//aa를 조회한다 ㅇㅇ에서 //내가 건들 곳
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())//완료한 스테이지 번호를 가져온다
            {
                volume = reader.GetInt32(0);//내가 건들 곳
                vibration = reader.GetInt32(1);//내가 건들 곳
                Debug.Log("volume: " + volume);
                Debug.Log("vibration: " + vibration);
            }
            reader.Close();

            //
            con.Close();
        }
        catch (Exception e) {
            print(e);
        }
        finally {
            reader.Close();
            con.Close();
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