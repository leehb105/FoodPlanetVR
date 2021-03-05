using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SocketClient.instance.sendMessage(string.Format("플레이어 {0}번이 방에 입장하였습니다!", SocketClient.instance.playerNo));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
