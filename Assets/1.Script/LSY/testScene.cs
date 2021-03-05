using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScene : MonoBehaviour
{
    //스테이지 씬에서 내가테스트 하려고 만든거, 통합하면 알려주고 지워야 한다
    GameObject temp;
    StageNum sNum;
    // Start is called before the first frame update
    void Start()
    {
        sNum = GameObject.Find("StageNum").GetComponent<StageNum>();
        int a = sNum.StageNumber;
        print("넘어온 스테이지 정보"+a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
