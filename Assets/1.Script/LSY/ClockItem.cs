using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockItem : MonoBehaviour
{   
    public enum ItemState
    {
        Off,
        On,
        Hit,
        OnHit,
    }
    public GameObject[] list;
    public GameObject FX_FloorLazer;

    public void setFloorFX(bool active, float alpha)
    {
        if (FX_FloorLazer.gameObject.activeSelf == active)
            return;

        var _tar = FX_FloorLazer.GetComponent<ParticleSystem>();
        var _main = _tar.main;
        _main.startColor = new Color(1, 1, 1, alpha);
        FX_FloorLazer.gameObject.SetActive(active);
    }

    Vector3 initScale;

    private void Start()
    {
        initScale = transform.localScale;
    }
    public void setActive(ItemState c)
    {
        switch (c)
        {
            case ItemState.Off:
                transform.localScale = initScale;
                setFloorFX(false, 0);
                break;
            case ItemState.On:
                transform.localScale = initScale;
                setFloorFX(true, 0.1f);
                break;
            case ItemState.Hit:
                transform.localScale = initScale * 1.1f;
                setFloorFX(true, 0.3f);
                break;
            case ItemState.OnHit:
                transform.localScale = initScale * 1.1f;
                setFloorFX(true, 0.3f);
                break;
            default:
                break;
        }
        for (int i = 0; i < list.Length; ++i)
        {
            if((int)c == i)
            {
                list[i].SetActive(true);
            }
            else
            {
                list[i].SetActive(false);
            }
        }
    }
    public void SetItem()//씬 start하면 디비에 있는 아이템정보를 가져와서 해당 조건에 맞게 세팅해준다
    {
        switch (ItemManager.instance.TimerStock)
        {
            case 0://아이템의 재고가 없으면
                setActive(ItemState.Off);
                break;
            case 1://아이템의 재고가 있으면
                setActive(ItemState.On);
                break;
            default:
                break;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        SetItem();
    }

}
