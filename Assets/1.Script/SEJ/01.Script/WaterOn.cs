using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOn : MonoBehaviour
{
    public GameObject water;
    public GameObject waterCube;
    // Start is called before the first frame update
    void Start()
    {
        water.SetActive(false);
        waterCube.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //오른손 트리거를 눌렀을떄 
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            water.SetActive(true);
            StartCoroutine(Wait());
            waterCube.SetActive(true);
        }
        //왼손 트리거를 눌렀을 때
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            water.SetActive(true);
            StartCoroutine(Wait());
            waterCube.SetActive(true);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        water.SetActive(false);
    }
}
