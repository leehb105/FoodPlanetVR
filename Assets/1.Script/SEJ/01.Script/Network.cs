using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour
{
    public static Network instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickRedy()
    {
        SocketClient.instance.sendMessage("Ready");
    }
}
