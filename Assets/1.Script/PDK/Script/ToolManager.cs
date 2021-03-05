using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class ToolManager : MonoBehaviour {
    public static ToolManager Instance;
    public void Awake() {
        Instance = this;
    }
    public GameObject myHammer1, myHammer2, myHammer3;
    public GameObject myKnife1, myKnife2, myKnife3;
    public GameObject myGrill1, myGrill2, myGrill3;

    public GameObject hammerSpot, knifeSpot, grillSpot;

    GameObject hammer, knife, grill;

    List<int> equipLevel = new List<int>();

    int hammerLevel = 1;
    int knifeLevel = 1;
    int grillLevel = 1;

    public GameObject toolArea;
    public int toolRespawnTime;

    // Start is called before the first frame update
    void Start() {
        //GameManager equipLevel 에서 장비별 레벨정보 가져오기
        //순서는 해머 나이프 그릴
        if (GameManager.Instance.equipLevel != null) {
            equipLevel = GameManager.Instance.equipLevel;

            hammerLevel = equipLevel[0];
            knifeLevel = equipLevel[1];
            grillLevel = equipLevel[2];
        }
        ToolHammerRespawn();
        ToolKnifeRespawn();
        ToolGrillRespawn();
    }

    // Update is called once per frame
    void Update() {
        //있는지 없는지를 상시 검사하는거는 성능상의 문제가 생길수도있음
        //그렇다고 몇초 혹은 몇분마다 검사하는거는 게임진행에 문제가 생길수도있음 방안 고려중

        //해머가 없어지거나 못찾으면 리스폰
        //칼이 없어지거나 못찾으면 리스폰
        //그릴이 없어지거나 못찾으면 리스폰
        //근데 그릴이 없어질리가??
    }

    public void ToolHammerRespawn() {
        switch (hammerLevel) {
            case 3:
                hammer = Instantiate(myHammer3);
                hammer.name = myHammer3.name;
                hammer.transform.position = hammerSpot.transform.position;
                break;
            case 2:
                hammer = Instantiate(myHammer2);
                hammer.name = myHammer2.name;
                hammer.transform.position = hammerSpot.transform.position;
                break;
            default:
                hammer = Instantiate(myHammer1);
                hammer.name = myHammer1.name;
                hammer.transform.position = hammerSpot.transform.position;
                break;
        }
    }
    public void ToolKnifeRespawn() {
        switch (knifeLevel) {
            case 3:
                knife = Instantiate(myKnife3);
                knife.name = myKnife3.name;
                knife.transform.position = knifeSpot.transform.position;
                break;
            case 2:
                knife = Instantiate(myKnife2);
                knife.name = myKnife2.name;
                knife.transform.position = knifeSpot.transform.position;
                break;
            default:
                knife = Instantiate(myKnife1);
                knife.name = myKnife1.name;
                knife.transform.position = knifeSpot.transform.position;
                break;
        }
    }
    private void ToolGrillRespawn() {
        switch (grillLevel) {
            case 3:
                grill = Instantiate(myGrill3);
                grill.name = myGrill3.name;
                grill.transform.position = grillSpot.transform.position;
                break;
            case 2:
                grill = Instantiate(myGrill2);
                grill.name = myGrill2.name;
                grill.transform.position = grillSpot.transform.position;
                break;
            default:
                grill = Instantiate(myGrill1);
                grill.name = myGrill1.name;
                grill.transform.position = grillSpot.transform.position;
                break;
        }
    }
}
