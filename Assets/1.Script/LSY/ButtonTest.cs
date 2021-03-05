using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    //클릭하면 딸깍
    Transform on;
    Transform off;
    // Start is called before the first frame update
    void Start()
    {
        //자식 오브젝트 들을 가져와서 on, off에 넣어준다
        on = gameObject.transform.GetChild(0);
        off = gameObject.transform.GetChild(1);
        //오브젝들을 처음에 비활성화 시킨다
        on.gameObject.SetActive(true);
        off.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ray"))
        {
            //print("레이에 맞았다");
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ray"))
        {
            //print("레이에 맞았다");
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
