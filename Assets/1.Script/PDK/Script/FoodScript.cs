using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;


public class FoodScript : MonoBehaviour {
    float curHP;
    string myname;
    bool knifeEnter = false;
    bool HammerEnter = false;
    bool GrillEnter = false;
    Rigidbody myRigidbody;
    BoxCollider myCollider;
    float bakeTime;
    bool grillDetect;
    bool grillChange;
    public AudioSource audioSource;

    public IngredientsSpawnPoint linkPoint;


    //빵은 FoodType0, 고기는 FoodType1, 상추는 FoodType2
    public float HP {          //싱글톤 안쓰는이유 : enemy는 하나가 아니기때문
        get { return curHP; } // enemy.HP = enemy.HP -1 -> enemy.HP(set), enemy.HP(get)
        set {
            curHP = Mathf.Max(0, value);
        }
    }
    public bool GrillDetect {
        get { return grillDetect; }
        set {
            grillDetect = value;
        }

    }
    // Start is called before the first frame update
    void Start() {
        audioSource.GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<BoxCollider>();
        myname = transform.name;
        curHP = 1f;
    }

    public GameObject smokeFx;
    public void onSmoke(GameObject go) {
        smokeFx = go;
        audioSource.Play();
    }
    public void disableSmoke() {
        if (smokeFx == null)
            return;
        audioSource.Stop();
        Destroy(smokeFx);
    }

    // Update is called once per frame
    void Update() {
        if (grillDetect) {
            bakeTime += Time.deltaTime;
            if (bakeTime >= 7) {
                disableSmoke();
                grillChange = true;
                HP = 0;
            }
        }

        switch (myname) {
            case "0Square_Rock":
                //돌 -> 둥근돌
                if (HammerEnter) {
                    if (curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position, "1Round_Rock", 0);
                    }
                }
                //돌 -> 네모돌 제외
                //else if (knifeEnter) {
                //    if (curHP <= 0) {
                //        Destroy(gameObject, 0);
                //        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(-0.1f, 0, 0), "4SquareBread_Left", 0);
                //        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0.1f, 0, 0), "4SquareBread_Right", 0);
                //    }
                //}
                else if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;

            case "1Round_Rock":
                //둥근돌 -> 버거 위아래
                if (HammerEnter) {
                    if (curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(-0.1f, 0, 0), "2BurgerBun_Top", 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0.1f, 0, 0), "3BurgerBun_Bottom", 0);
                    }
                    else if (GrillEnter) {
                        if (grillChange && curHP <= 0) {
                            Destroy(gameObject, 0);
                            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                        }
                    }
                }
                break;

            case "2BurgerBun_Top":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;

            case "3BurgerBun_Bottom":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;
            //토스트 관련 제외
            //case "4SquareBread_Left":
            //    if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.3f, 0), "5Toast", 0);
            //        }
            //    }
            //    break;
            //case "4SquareBread_Right":
            //    if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.3f, 0), "5Toast", 0);
            //        }
            //    }
            //    break;
            //case "5Toast":
            //    if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.3f, 0), "Burned", 4);
            //        }
            //    }
            //    break;

            /////////////////////////////////////////////////////////////////////////////////////////////////

            case "0BIGMeat":
                //고기 -> 다진고기
                if (HammerEnter) {
                    if (curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position, "1MeatBone", 1);
                    }
                }
                //고기 -> 스테이크용고기
                else if (knifeEnter) {
                    if (curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position, "3SteakMeat", 1);
                    }
                }
                else if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;
            case "1MeatBone":
                //다진고기 -> 패티
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "2Patty", 1);
                    }
                }
                break;
            case "2Patty":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;
            case "3SteakMeat":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "6Steak_Welldone", 1);
                    }
                }
                break;
            //case "4Steak_Rare":
            //    if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.3f, 0), "5Steak_medium", 1);
            //        }
            //    }
            //    break;
            //case "5Steak_medium":
            //    if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.1f, 0), "6Steak_Welldone", 1);
            //        }
            //    }
            //    break;
            case "6Steak_Welldone":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;
            //상추는 손질하는게 아니라 처음부터 햄버거용으로 수정
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //case "0Cabbage":
            //    if (HammerEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position, "1Hamburger_lettuce", 2);
            //        }
            //    }
            //    else if (knifeEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position, "2mass_lettuce", 2);
            //        }
            //    }
            //    else if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.3f, 0), "Burned", 4);
            //        }
            //    }
            //    break;
            case "HamburgerLet":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;
            //샐러드 
            //case "SaladLet":
            //    if (GrillEnter) {
            //        if (curHP <= 0) {
            //            Destroy(gameObject, 0);
            //            IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.3f, 0), "Burned", 4);
            //        }
            //    }
            //    break;
            case "Cheese":
                if (GrillEnter) {
                    if (grillChange && curHP <= 0) {
                        Destroy(gameObject, 0);
                        IngredientsSpawnManager.Instance.FoodPrepping(transform.position + new Vector3(0, 0.03f, 0), "Burned", 4);
                    }
                }
                break;

            default:
                break;
        }
    }
    private void OnCollisionEnter(Collision other) {

        if (other.transform.tag == "TOOL" && !other.transform.name.Contains("Grill")) {
            //myRigidbody.isKinematic = true;
            //myRigidbody.useGravity = false;
            //myCollider.isTrigger = true;
            //Debug.Log(gameObject.name+"이 "+other.gameObject.name+"과 충돌, 생명력:" + curHP);
            if (other.transform.name.Contains("Hammer")) {
                HammerEnter = true;
                knifeEnter = false;
                GrillEnter = false;
                myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            if (other.transform.name.Contains("Knife")) {
                HammerEnter = false;
                knifeEnter = true;
                GrillEnter = false;
                myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        else if (other.transform.name.Contains("Grill")) {
            //Debug.Log(gameObject.name + "이 " + other.gameObject.name + "과 충돌, 생명력:" + curHP);
            HammerEnter = false;
            knifeEnter = false;
            GrillEnter = true;
        }
        if (other.transform.tag == "DISH") {
            //transform.SetParent(other.transform);
            //접시에 올라간뒤로는 플레이어가 오브젝트를 따로 잡을수 없도록,
            //DistanceGrabbable 끄는걸로 했는데도 잡혀서 layer를 0로 변경하는방식으로 진행
            myCollider.enabled = false;
            gameObject.layer = 0;

            myRigidbody.useGravity = false;
            myRigidbody.isKinematic = true;

        }
    }

    private void OnCollisionExit(Collision other) {
        HammerEnter = false;
        knifeEnter = false;
        GrillEnter = false;
        myRigidbody.constraints = RigidbodyConstraints.None;
    }

    //private void OnTriggerExit(Collider other) {
    //    if (other.transform.tag == "TOOL") {
    //        myRigidbody.isKinematic = false;
    //        myRigidbody.useGravity = true;
    //        myCollider.isTrigger = false;

    //        HammerEnter = false;
    //        knifeEnter = false;
    //        GrillEnter = false;
    //    }
    //}

    //private void OnTriggerEnter(Collision other) {
    //    //이름으로 호출하는거는 느려서? 태그로 호출하는게 빠르다함 나중에 바꿔야징
    //    if (other.transform.name.Contains("Hammer")) {
    //        HammerEnter = true;
    //        knifeEnter = false;
    //    }

    //    if (other.transform.name.Contains("Knife")) {
    //        HammerEnter = false;
    //        knifeEnter = true;
    //    }
    //    if (other.transform.name.Contains("Grill")) {

    //    }
    //}
}
