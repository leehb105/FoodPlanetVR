using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void Start()
    {
        exitUI = GameObject.Find("ExitCheck_UI");
        exitUI.SetActive(false);
    }
    GameObject exitUI;
    public void ClickExitBtn()//게임종료
    {
        exitUI.SetActive(true);
        exitUI.transform.position = Camera.current.transform.position + new Vector3(0, 0.3f, 1);
    }
    public void ClickExitYesBtn()
    {
        Application.Quit();
    }
    public void ClickExitNoBtn()
    {
        exitUI.SetActive(false);

    }

    void Update()
    {
        transform.position = Camera.main.transform.position + Vector3.forward;
    }


}
