using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;
    private void Awake()
    {
        instance = this;
        SetDBPath();

    }

    string filepath;
    IDbConnection dbconn;
    IDbCommand dbcommand;
    IDataReader reader;

    string temp_path;
    string sqlQuery;

    // Start is called before the first frame update
    void Start()
    {
        LoadItemDB();
        LoadMoneyDB();
        LoadStageDB();
    }
    
    public void UseMoney_Equip(string name, int price)
    {
        //업그레이드 한다
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;

            int myMoney = MoneyManager.instance.MyMoney;
            if (myMoney < price || myMoney <= 0)//잔액이 부족한 경우: 내 돈이 0원보다 적거나 아이템보다 적은 돈이 있거나 
            {
                UIManager.instance.NoMoney(name);//장비 3개 추가할 것
                dbconn.Close();
            }
            else
            {
                int spentMoney = myMoney - price;//내돈-값을 받은 돈 을 소비  한 돈에 넣어준다
                sqlQuery = "UPDATE Money SET MyMoney = @SpentMoney";
                dbcommand.CommandText = sqlQuery;

                SqliteParameter param = new SqliteParameter();
                param.ParameterName = "@SpentMoney";
                param.Value = spentMoney;
                dbcommand.Parameters.Add(param);

                reader = dbcommand.ExecuteReader();
                reader.Close();

                MoneyManager.instance.CheckMoney(spentMoney);
                dbconn.Close();
                BuyEquip(name, price);
            }
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void BuyEquip(string name, int money)//장비를 구매하는 메소드
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            sqlQuery = "UPDATE Equip SET EquipLock = 0 WHERE EquipName = @Name";//현재의 장비는 사용 중지 시킴
            dbcommand.CommandText = sqlQuery;
            
            SqliteParameter param = new SqliteParameter();
            param.ParameterName = "@Name";
            param.Value = name;
            dbcommand.Parameters.Add(param);

            reader = dbcommand.ExecuteReader();
            reader.Close();

            sqlQuery = "SELECT EquipID, EquipLevel FROM Equip WHERE EquipName = @Name";//현재 장비의 레벨 가져오기 위한 곳
            dbcommand.CommandText = sqlQuery;

            dbcommand.Parameters.Add(param);

            reader = dbcommand.ExecuteReader();
            int tempLevel = 0;
            string tempID = string.Empty;
            while (reader.Read())
            {
                tempID = reader.GetString(0);
                tempLevel = reader.GetInt32(1);
            }
            tempLevel++;
            reader.Close();

            sqlQuery = "UPDATE Equip SET EquipLock = 1 WHERE EquipLevel = " + tempLevel + " AND EquipID = '" + tempID + "'";//가져온 레벨+1의 것을 사용
            dbcommand.CommandText = sqlQuery;

            reader = dbcommand.ExecuteReader();
            reader.Close();
            dbconn.Close();

            ReloadEquipDB();
            StartCoroutine(UIManager.instance.BuyEquipParticle(tempID));
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void UseMoney_Item(string name, int money)//구매 버튼을 누르면 호출
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            
            int myMoney = MoneyManager.instance.MyMoney;
            if (myMoney < money || myMoney <= 0)//잔액이 부족한 경우: 내 돈이 0원보다 적거나 아이템보다 적은 돈이 있거나 
            {
                UIManager.instance.NoMoney(name);
                dbconn.Close();
            }
            else
            {
                int spentMoney = myMoney - money;//내돈-값을 받은 돈 을 소비  한 돈에 넣어준다
                sqlQuery = "UPDATE Money SET MyMoney = @SpentMoney";
                dbcommand.CommandText = sqlQuery;

                SqliteParameter param = new SqliteParameter();
                param.ParameterName = "@SpentMoney";
                param.Value = spentMoney;
                dbcommand.Parameters.Add(param);

                reader = dbcommand.ExecuteReader();
                reader.Close();
                MoneyManager.instance.CheckMoney(spentMoney);
                dbconn.Close();
                BuyItem(name, 1);
            }
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void BuyItem(string name, int stock)//아이템 이름과 재고를 받아와서 재고를 변경해준다
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            sqlQuery = "UPDATE Item SET ItemStock = " + stock + " WHERE ItemName = '" + name + "'";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();

            StartCoroutine(UIManager.instance.BuyItemParticle(name));
            reader.Close();
            dbconn.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void ReloadEquipDB()
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            sqlQuery = "SELECT EquipName, EquipLevel, EquipPrice FROM Equip WHERE EquipLock = 1";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();

            EquipManager.instance.ClearList();//리스트에 다시 넣기 위해 전의 리스트를 비운다
            while (reader.Read())//완료한 스테이지 번호를 가져온다
            {
                EquipManager.instance.ModifyList(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2));//이름과 레벨을 다시 순차적으로 넣어준다
            }
            reader.Close();
            dbconn.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void LoadEquipDB()//장비 레벨과 정보를 가져온다
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            sqlQuery = "SELECT EquipName, EquipLevel, EquipPrice FROM Equip WHERE EquipLock = 1";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();

            while (reader.Read())//완료한 스테이지 번호를 가져온다
            {
                EquipManager.instance.AddEquipData(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2));//이름과 레벨을 순차적으로 넣어준다
            }
            reader.Close();
            dbconn.Close();
            EquipManager.instance.SetEquip();
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }

    public void LoadMoneyDB()
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();

            sqlQuery = string.Empty;
            sqlQuery = "SELECT MyMoney FROM Money";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();

            while (reader.Read())//셀렉트
            {
                MoneyManager.instance.MyMoney = reader.GetInt32(0);
            }
            reader.Close();
            dbconn.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void LoadStageDB()
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;

            sqlQuery = string.Empty;
            sqlQuery = "SELECT StageScore FROM Stage";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();

            while (reader.Read())//위의 스테이지의 별점을 가져온다
            {
                StageManager.instance.stageScore.Add(reader.GetInt32(0));
            }
            reader.Close();
            dbconn.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }

    }
    public void LoadItemDB()
    {
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            sqlQuery = "SELECT ItemStock, ItemPrice FROM Item WHERE ItemName = 'heart'";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();
            while (reader.Read())
            {
                ItemManager.instance.HeartStock = reader.GetInt32(0);
                ItemManager.instance.HeartPrice = reader.GetInt32(1);
                
            }
            reader.Close();

            sqlQuery = string.Empty;
            sqlQuery = "SELECT ItemStock, ItemPrice FROM Item WHERE ItemName = 'timer'";
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();
            while (reader.Read())
            {
                ItemManager.instance.TimerStock = reader.GetInt32(0);
                ItemManager.instance.TimerPrice = reader.GetInt32(1);
            }
            reader.Close();
            dbconn.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }

    }
   
    public void  SetDBPath()
    {
        filepath = string.Empty;
        if(Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/DB.db";
            if (!File.Exists(filepath))
            {
                //UnityWebRequest unityWebRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/DB.db");
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DB.db");
                loadDB.bytesDownloaded.ToString();
                /*unityWebRequest.downloadedBytes.ToString();
                while (!unityWebRequest.isDone) {
                    conn.text = "무한 while중...";
                }
                File.WriteAllBytes(filepath, unityWebRequest.downloadHandler.data);*/
                while (!loadDB.isDone)
                {
                    //conn.text = "무한 while중...";
                }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }
        }
        else
        {
            filepath = Application.dataPath + "/StreamingAssets/DB.db";
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/DB.db", filepath);
            }
        }
        try
        {
            temp_path = "URI=file:" + filepath;
            dbconn = new SqliteConnection(temp_path); 
        }
        catch(Exception e)
        {
            print(e);
        }
    }
  
}