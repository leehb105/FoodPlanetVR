using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEquip : MonoBehaviour
{
    Transform[] childs;
    // Start is called before the first frame update
    void Start()
    {
        childs = new Transform[3];
        SetEquip();
    }
    public void SetEquip()
    {
        for (int i = 0; i < childs.Length; i++)//장비 3개를 넣어준다
        {
            childs[i] = gameObject.transform.GetChild(i);
            print(childs[i].name);
        }
        for (int i = 0; i < EquipManager.instance.equipList.Count; i++)
        {
            switch (EquipManager.instance.equipList[i].name)
            {
                case "Hammer":
                    childs[i - 1].gameObject.SetActive(true);
                    break;
                case "Grill":
                    childs[i - 1].gameObject.SetActive(true);
                    break;
                case "Knife":
                    childs[i - 1].gameObject.SetActive(true);
                    break;
            }
        }
        //EquipManager.instance.equipList;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
