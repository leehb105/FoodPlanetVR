using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager instance;
    private void Awake()
    {
        instance = this;
        //DBManager.instance.LoadEquipDB();
    }

    GameObject hammerPos;
    GameObject knifePos;
    GameObject grillPos;
    Vector3 originHScale;
    Vector3 originKScale;
    Vector3 originGScale;

    Transform[] h;
    Transform[] k;
    Transform[] g;

    public struct Equip
    {
        public string name;
        public int level;
        public int price;
    }
    public List<Equip> equipList = new List<Equip>();

    // Start is called before the first frame update
    void Start()
    {
        hammerPos = GameObject.Find("HammerPos");
        knifePos = GameObject.Find("KnifePos");
        grillPos = GameObject.Find("GrillPos");
        originHScale = hammerPos.transform.localScale;
        originKScale = knifePos.transform.localScale;
        originGScale = grillPos.transform.localScale;
        h = new Transform[3];
        k = new Transform[3];
        g = new Transform[3];
        for (int i = 0; i < h.Length; i++)
        {
            h[i] = GameObject.Find("HammerPos").transform.GetChild(i);
            k[i] = GameObject.Find("KnifePos").transform.GetChild(i);
            g[i] = GameObject.Find("GrillPos").transform.GetChild(i);
        }
        PrintList();
        //SetEquip();
        DBManager.instance.LoadEquipDB();
    }
    public void OffEquip()
    {
        hammerPos.transform.localScale = originHScale;
        knifePos.transform.localScale = originKScale;
        grillPos.transform.localScale = originGScale;
    }
    
    public void OnEquip(string name)
    {
        switch (name)
        {
            case "HammerPos":
                hammerPos.transform.localScale = originHScale * 1.2f;
                break;
            case "KnifePos":
                knifePos.transform.localScale = originKScale * 1.2f;
                break;
            case "GrillPos":
                grillPos.transform.localScale = originGScale * 1.2f;
                break;
        }
        
    }
    public void SetEquip()
    {//순서 해머-나이프-그릴
        switch (equipList[0].level)
        {
            case 1:
                h[0].gameObject.SetActive(true);
                h[1].gameObject.SetActive(false);
                h[2].gameObject.SetActive(false);
                break;
            case 2:
                h[0].gameObject.SetActive(false);
                h[1].gameObject.SetActive(true);
                h[2].gameObject.SetActive(false);
                break;
            case 3:
                h[0].gameObject.SetActive(false);
                h[1].gameObject.SetActive(false);
                h[2].gameObject.SetActive(true);
                break;
        }
        switch (equipList[1].level)
        {
            case 1:
                k[0].gameObject.SetActive(true);
                k[1].gameObject.SetActive(false);
                k[2].gameObject.SetActive(false);
                break;
            case 2:
                k[0].gameObject.SetActive(false);
                k[1].gameObject.SetActive(true);
                k[2].gameObject.SetActive(false);
                break;
            case 3:
                k[0].gameObject.SetActive(false);
                k[1].gameObject.SetActive(false);
                k[2].gameObject.SetActive(true);
                break;
        }
        switch (equipList[2].level)
        {
            case 1:
                g[0].gameObject.SetActive(true);
                g[1].gameObject.SetActive(false);
                g[2].gameObject.SetActive(false);
                break;
            case 2:
                g[0].gameObject.SetActive(false);
                g[1].gameObject.SetActive(true);
                g[2].gameObject.SetActive(false);
                break;
            case 3:
                g[0].gameObject.SetActive(false);
                g[1].gameObject.SetActive(false);
                g[2].gameObject.SetActive(true);
                break;
        }
    }
    
    public void PrintList()
    {
        for(int i = 0; i<equipList.Count; i++)
        {
            print(equipList[i].name + " : " + equipList[i].level + " : " + equipList[i].price);
        }
    }

    public void ClearList()
    {
        equipList.Clear(); 
    }
    public void ModifyList(string name, int level, int price)
    {
        Equip data = new Equip();//변수 선언
        data.name = name;
        data.level = level;
        data.price = price;
        equipList.Add(data);
    }
    public void AddEquipData(string name, int level, int price)//리스트에 데이터(장비이름, 장비레벨)을 넣어주기 위한 메소드
    {
        Equip data = new Equip();//변수 선언
        data.name = name;
        data.level = level; 
        data.price = price;
        equipList.Add(data);
    }
}
