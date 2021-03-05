using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JsonLoad();
    }
    public void JsonLoad()
    {
        //Debug.Log("제이슨테스트");
        string JsonString = File.ReadAllText(Application.dataPath + "/8.json/ItemSet.json");
        Debug.Log(JsonString);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
