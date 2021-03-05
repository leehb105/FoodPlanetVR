using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    Clock,
}
public class Item : MonoBehaviour
{
    public ItemType Type;

    public GameObject itemOff;
    public GameObject itemOn;
    public GameObject itemHit;

    public ParticleSystem fxLight;


    private void Start()
    {
        //자식 오브젝트 들을 가져와서 on, off에 넣어준다
        itemOff = transform.GetChild(0).gameObject;//꺼진 아이템 저장
        itemOn = transform.GetChild(1).gameObject;//켜진 아이템 저장

        //오브젝들을 처음에 비활성화 시킨다
        itemOn.SetActive(false);
        itemOff.SetActive(false);
        //setItem(ItemType.Hide);

        fxLight.gameObject.SetActive(false);

    }

    public void rayEnter()
    {
        
        
    }
    public void rayExit()
    {

    }


    public void rayEnter(GameObject go)
    {
        if(go == gameObject)
        {
            if (ItemManager.instance.TimerStock == 0 ||
                ItemManager.instance.HeartStock == 0)
            {
                fxLight.gameObject.SetActive(false);
                itemOff.SetActive(false);
                itemOn.SetActive(true);
                itemHit.SetActive(false);
            }
            else
            {
                fxLight.gameObject.SetActive(true);
                itemOff.SetActive(false);
                itemOn.SetActive(false);
                itemHit.SetActive(true);
            }
        }
        else //레이를 통해 받은 오브젝트가 아니면
        
        {
            if (ItemManager.instance.TimerStock == 1 ||
                ItemManager.instance.HeartStock == 1)
            {
                fxLight.gameObject.SetActive(true);
                itemOff.SetActive(false);
                itemOn.SetActive(true);
                itemHit.SetActive(false);
            }
            else
            {
                fxLight.gameObject.SetActive(false);
                itemOff.SetActive(true);
                itemOn.SetActive(false);
                itemHit.SetActive(false);
            }
        }
    }



    public void SetItem()//씬 start하면 디비에 있는 아이템정보를 가져와서 해당 조건에 맞게 세팅해준다
    {
        switch (gameObject.name)
        {
            case "Heart"://Heart오브젝트라면
                switch (ItemManager.instance.HeartStock)
                {
                    case 0://아이템의 재고가 없으면
                        itemOff.SetActive(true);
                        itemOn.SetActive(false);
                        itemHit.SetActive(false);

                        break;
                    case 1://아이템의 재고가 있으면
                        itemOff.SetActive(false);
                        itemOn.SetActive(true);
                        itemHit.SetActive(false);

                        fxLight.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case "Timer"://Timer오브젝트 라면
                switch (ItemManager.instance.TimerStock)
                {
                    case 0://아이템의 재고가 없으면
                        itemOff.SetActive(true);
                        itemOn.SetActive(false);
                        itemHit.SetActive(false);

                        break;
                    case 1://아이템의 재고가 있으면
                        itemOff.SetActive(false);
                        itemOn.SetActive(true);
                        itemHit.SetActive(false);

                        fxLight.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
        }

    }
    // Update is called once per frame
    void Update()
    {
        SetItem();
    }
}
