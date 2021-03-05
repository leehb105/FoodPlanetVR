using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnClickStageButton : MonoBehaviour
{
    
    GameObject g_stageNum;
    StageNum sNum;

    public void ClickLeftBtn()
    {
        print("왼쪽 버튼이 클릭 되었음");
        StageManager.instance.PreStage();
    }
    public void ClickRightBtn()
    {
        print("오른쪽 버튼이 클릭 되었음");
        StageManager.instance.NextStage();
    }

    public void SetStageNum()//현재의 스테이지 넘버를 DontDestroyOnLoad 하기 위해 저장한다
    {
        g_stageNum = GameObject.Find("StageNum");
        sNum = g_stageNum.GetComponent<StageNum>();
        sNum.StageNumber = StageManager.instance.MyStage;
    }
    public void ClickStartBtn()//스테이지를 시작한다
    {
        SetStageNum();
        DontDestroyOnLoad(g_stageNum);
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());
            
        //LoadingSceneManager.instance.LoadScene("StageRoom_KJK");
        //LoadingSceneManager.LoadScene("LoadingScene");
        //SceneManager.LoadScene(1);
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
