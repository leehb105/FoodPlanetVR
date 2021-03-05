using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
public class DBAccess : MonoBehaviour
{
    public Text text0;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public string tempQuery;
    void AccessDB()
    {
        string s;
        string ItemID = "";
        string ItemName = "";
        int ItemPrice = 0;

        string filepath = string.Empty;

        if (Application.platform == RuntimePlatform.Android)//실행플랫폼이 안드로이드일 경우
        {
            //안드로이드 일 경우
            filepath = Application.persistentDataPath + "/DB.db";
            text0.text = "RuntimePlatform.Android 실행";
            if (!File.Exists(filepath))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DB.db");
                text1.text = "경로가!/assets/DB.db이게 맞냐 ";//여기까지는 출력이 된다
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
                //

            }
        }
        else
        {
            //윈도우 일 경우
            filepath = Application.dataPath + "/StreamingAssets/DB.db";
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/DB.db", filepath);
                text0.text = "윈도우 환경 DB.db 실행";
            }
        }
        //print(filepath);
        string temp_filepath = "URI=file:" + filepath;
        try
        {
            text2.text = "try문 실행";
            SqliteConnection con = new SqliteConnection(temp_filepath);
            con.Open();
            text3.text = "오픈성공";
            //
            IDbCommand dbcmd = con.CreateCommand();
            string sqlQuery = "SELECT Volume FROM SystemOption";
            dbcmd.CommandText = sqlQuery;

            IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                ItemID = reader.GetString(0);

                //Debug.Log("볼륨값: " + ItemID);
                text4.text = ItemID +" "+ ItemName+ " " + ItemPrice;
            }
//
            con.Close();
        }
        catch (Exception e)
        {
            s = e.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AccessDB();

    }
    // Update is called once per frame
    void Update()
    {

    }
}
