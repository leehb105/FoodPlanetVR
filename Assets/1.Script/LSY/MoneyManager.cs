using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public GameObject textMoney;
    int myMoney;
    public static MoneyManager instance;

    private void Awake()
    {
        instance = this;
        //DBManager.instance.LoadMoneyDB();
    }
    public int MyMoney { 
        get { return myMoney; } 
        set { myMoney = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        //textMoney = GameObject.Find("Money_Text");
        //print("myMoney: " + myMoney);
    }
    public void CheckMoney(int money)
    {
        myMoney = money;
        textMoney.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,0}", myMoney);
    }
    /*public void SetMoneyText()
    {
        char[] moneyText = new char[6];
        if (myMoney > 999)
        {
            for(int i = 0; i<myMoney.ToString().Length; i++)
            {
                moneyText[i] = myMoney.ToString()[i];
            }
            
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        textMoney.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,0}", myMoney);
        //SetMoneyText();
    }
}
