using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testitem : MonoBehaviour
{
    string itemName;
    // Start is called before the first frame update
    void Start()
    {
        itemName = gameObject.name;
        print(itemName);//item1,2 으로 출력
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
