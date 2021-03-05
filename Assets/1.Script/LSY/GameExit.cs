using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickExit();
    }
    public void ClickExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("종료");
            Application.Quit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        /*//트리거 클릭하면 어플을 종료한다
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            print("트리거 다운");
            //Application.Quit();
        }*/
    }
}
