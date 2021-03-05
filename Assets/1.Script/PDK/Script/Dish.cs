using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class Dish : MonoBehaviour {

    List<string> WelldoneSteak = new List<string> { "6Steak_Welldone" };
    List<string> MiniBurger = new List<string> { "3BurgerBun_Bottom", "2Patty", "2BurgerBun_Top" };
    List<string> CheeseBurger = new List<string> { "3BurgerBun_Bottom", "2Patty", "Cheese", "2BurgerBun_Top" };



    List<string> myfood = new List<string> { };
    public enum enumFood {
        none,       //0
        WelldoneSteak,
        MiniBurger,
        CheeseBurger          //3
    }
    enumFood ef;
    BoxCollider myCollider;
    BoxCollider otherCollider;
    Vector3 allColliderSize;

    // Start is called before the first frame update
    void Start() {

        ef = enumFood.none;
        myCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {
    }

    bool CheckWelldoneSteak() {
        if (myfood.Count != WelldoneSteak.Count) {
            return false;
        }
        for (int i = 0; i < myfood.Count; i++) {
            if (myfood[i] != WelldoneSteak[i]) {
                return false;
            }
        }
        return true;
    }
    bool CheckMiniBurger() {
        if (myfood.Count != MiniBurger.Count) {
            return false;
        }
        for (int i = 0; i < myfood.Count; i++) {
            if (myfood[i] != MiniBurger[i]) {
                return false;
            }
        }
        return true;
    }
    bool CheckCheeseBurger() {
        if (myfood.Count != CheeseBurger.Count) {
            return false;
        }
        for (int i = 0; i < myfood.Count; i++) {
            if (myfood[i] != CheeseBurger[i]) {
                return false;
            }
        }
        return true;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "FOOD") {
            other.transform.SetParent(transform);
            otherCollider = other.transform.GetComponent<BoxCollider>();

            //하나 닿을때마다 나의 컬라이더박스의 y값을 늘림(위로)
            myCollider.size += new Vector3(0, otherCollider.size.y, 0);
            myCollider.center += new Vector3(0, otherCollider.size.y / 2, 0);
            allColliderSize += new Vector3(0, otherCollider.size.y, 0);

            other.transform.localScale = new Vector3(1, 1, 1);
            other.transform.localRotation = Quaternion.Euler(0, 0, 0);
            other.transform.localPosition = allColliderSize;

        }

        if (other.transform.tag == "CHECK") {
            int children = transform.childCount;
            for (int i = 0; i < children; ++i) {
                myfood.Add(transform.GetChild(i).name);
            }
            if (CheckWelldoneSteak()) {
                ef = enumFood.WelldoneSteak;
            }
            //////////////////////////////////
            else if (CheckMiniBurger()) {
                ef = enumFood.MiniBurger;
            }
            else if (CheckCheeseBurger()) {
                ef = enumFood.CheeseBurger;
            }
            CheckFood checkFood = other.transform.gameObject.GetComponent<CheckFood>();
            checkFood.dishFoodName = ef.ToString();
            checkFood.foodCheck();
            Destroy(gameObject, 0);
            //Debug.Log("내이름: " + ef.ToString());
        }
    }
}
