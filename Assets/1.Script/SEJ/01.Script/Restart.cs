using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class Restart : MonoBehaviour
{
    string filepath;
    IDataReader reader;
    string temp_path;
    SqliteConnection con;
    IDbCommand dbcmd;
    string sqlQuery;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateSystemDB(int stageLevel, string StageRock, string StageStar) //디비 오픈하고 쿼리문으로 제어하는 곳
    {
        try
        {
            con.Open(); //DB접속
            dbcmd = con.CreateCommand(); //여기부터 sql입력을 위한 코드 
            sqlQuery = string.Empty; //내가 입력할 명령어를 위한 값 = 비운다

            sqlQuery = "SELECT StageLock From Stage";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            int tempMoney = 0;
            while (reader.Read())
            {
                tempMoney = reader.GetInt32(0);
            }
            reader.Close();

            stageLevel = stageLevel + tempMoney;
            sqlQuery = "UPDATE Stage SET StageLock = 0 WHERE StageStar = 0";//aa를 한다 ㅇㅇ에서 //내가 건들 곳
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            reader.Close();

            con.Open(); //DB접속
            dbcmd = con.CreateCommand(); //여기부터 sql입력을 위한 코드 
            sqlQuery = string.Empty; //내가 입력할 명령어를 위한 값 = 비운다

            con.Open(); //DB접속
            dbcmd = con.CreateCommand(); //여기부터 sql입력을 위한 코드 
            sqlQuery = string.Empty; //내가 입력할 명령어를 위한 값 = 비운다

            //
            con.Close();
        }
        catch (Exception e)
        {
            print(e);
        }

        finally
        {
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
            if (!File.Exists(filepath))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DB.db");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }
        }
        else
        {
            //윈도우 일 경우
            filepath = Application.dataPath + "/StreamingAssets/DB.db";
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/DB.db", filepath);
                //print(filepath);
            }
        }
        try
        {
            temp_path = "URI=file:" + filepath;
            con = new SqliteConnection(temp_path);


        }
        catch (Exception e)
        {
            print(e);
        }
    }
}
