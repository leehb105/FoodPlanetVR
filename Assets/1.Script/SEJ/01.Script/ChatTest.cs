using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatTest : MonoBehaviour
{
    public InputField myInput;

    private void Start()
    {
        SocketClient.instance.sendMessage(string.Format("플레이어 {0}번이 방에 입장하였습니다!", SocketClient.instance.playerNo));
    }

    public void PrintMyChat()
    {
        string message = myInput.text;
        SocketClient.instance.sendMessage(message);
        
    }
}
