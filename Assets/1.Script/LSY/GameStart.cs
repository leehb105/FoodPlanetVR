using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    GameObject g_stageNum;
    StageNum sNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetStageNum()
    {
        g_stageNum = GameObject.Find("StageNum");
        sNum = g_stageNum.GetComponent<StageNum>();
        sNum.StageNumber = StageManager.instance.MyStage;
    }
    public void ClickStart()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("스테이지 시작");

            SetStageNum();
            SceneManager.LoadScene(1);
            DontDestroyOnLoad(g_stageNum);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ClickStart();
    }
}
