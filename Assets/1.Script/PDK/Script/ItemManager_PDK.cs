using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager_PDK : MonoBehaviour {

    List<int> itemStock = new List<int>(); //[0]타이머, [1]하트
    int itemTimmer = 0;
    int itmeHeart = 0;

    // Start is called before the first frame update
    void Start() {
        if (GameManager.Instance.itemStock != null) {
            itemStock = GameManager.Instance.itemStock;
            //Debug.Log("itemStockCount: "+itemStock.Count);
            itemTimmer = itemStock[0];
            itmeHeart = itemStock[1];
        }
        //Debug.Log("itemTimmer: " + itemTimmer);
        //Debug.Log("itmeHeart: " + itmeHeart);
        GameManager.Instance.startComplainCount = 5;
        if (itemTimmer == 1) {
            TimerPlus();
        }

        if (itmeHeart == 1) {
            HeartPlus();
        }
        

    }

    // Update is called once per frame
    void Update() {

    }
    void TimerPlus() {
        FoodManager.Instance.FoodTimePlus();
        GameManager.Instance.SetItemDB(GameManager.Instance.conn, "UPDATE Item SET ItemStock = ItemStock - 1 WHERE ItemName is 'timer'");
    }
    void HeartPlus() {
        GameManager.Instance.Complain++;
        GameManager.Instance.startComplainCount = 6;
        GameManager.Instance.SetItemDB(GameManager.Instance.conn, "UPDATE Item SET ItemStock = ItemStock - 1 WHERE ItemName is 'heart'");
    }


}
