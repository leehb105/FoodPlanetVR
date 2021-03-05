using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickBuyButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        exitUI = GameObject.Find("ExitCheck_UI");
        exitUI.SetActive(false);
    }
    GameObject exitUI;
    public void ClickExitBtn()//게임종료
    {
        exitUI.SetActive(true);
    }
    public void ClickExitYesBtn()
    {
        Application.Quit();
    }
    public void ClickExitNoBtn()
    {
        exitUI.SetActive(false);
    }
    public void ClickBuyHeartBtn()//하트 아이템 구매
    {
        if(ItemManager.instance.HeartStock == 0)
        {
            DBManager.instance.UseMoney_Item("heart", ItemManager.instance.HeartPrice);
        }
        else
        {
            UIManager.instance.ItemStockMsg("heart");
        }
    }
    public void ClickBuyTimerBtn()//타이머 아이템 구매
    {
        if (ItemManager.instance.TimerStock == 0)
        {
            DBManager.instance.UseMoney_Item("timer", ItemManager.instance.TimerPrice);
        }
        else
        {
            UIManager.instance.ItemStockMsg("timer");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
