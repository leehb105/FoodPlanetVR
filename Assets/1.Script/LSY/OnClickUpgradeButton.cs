using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickUpgradeButton : MonoBehaviour
{
    public void ClickUpgradeHammerBtn()//망치 업그레이드 버튼
    {
        print("버튼 클릭");
        if (EquipManager.instance.equipList[0].level < 3)
        {
            DBManager.instance.UseMoney_Equip(EquipManager.instance.equipList[0].name, EquipManager.instance.equipList[0].price);
        }
        else
        {
            UIManager.instance.MaxLevelInfo("Hammer");
        }
    }
    public void ClickUpgradeKnifeBtn()//칼 업그레이드 버튼
    {
        print("버튼 클릭");
        if (EquipManager.instance.equipList[1].level < 3)
        {
            DBManager.instance.UseMoney_Equip(EquipManager.instance.equipList[1].name, EquipManager.instance.equipList[1].price);
        }
        else
        {
            UIManager.instance.MaxLevelInfo("Knife");
        }
    }
    public void ClickUpgradeGrillBtn()//그릴 업그레이드 버튼
    {
        print("버튼 클릭");
        if (EquipManager.instance.equipList[2].level < 3)
        {
            DBManager.instance.UseMoney_Equip(EquipManager.instance.equipList[2].name, EquipManager.instance.equipList[2].price);
        }
        else
        {
            UIManager.instance.MaxLevelInfo("Grill");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
