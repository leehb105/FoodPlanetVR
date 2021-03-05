using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;
using UnityEngine.Android;
using System.Threading;
using UnityEngine.UI;

public class SocketClient : MonoBehaviour
{
    public static SocketClient instance;
    TcpClient clientSocket = new TcpClient();
    NetworkStream stream = default(NetworkStream);
    string message = string.Empty;
    bool judgeFlag = false;
    public Text text;

    [SerializeField]
    //public string ConnectIP = "172.30.58.79";
    public string ConnectIP = "172.30.58.79";

    [SerializeField]
    public int PortID = 11000;
    public int playerNo = 0;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);


        ConnectToServer();
    }
    private void Start()
    {
    }

    private IEnumerator ServerLoop()
    {
        sendMessage("myScore_1000");
        yield return new WaitForSeconds(0.5F);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            sendMessage("SketchPad$TestMessage");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sendMessage("EventSuccess");
        }
    }

    private void ConnectToServer()
    {
        clientSocket.Connect(ConnectIP, PortID);
        stream = clientSocket.GetStream();

        message = "Connected to Server";

        byte[] buffer = Encoding.Unicode.GetBytes("player" + playerNo + "$" + message);
        stream.Write(buffer, 0, buffer.Length);
        stream.Flush();

        Thread t_handler = new Thread(getMessage);
        t_handler.IsBackground = true;
        t_handler.Start();
    }
    int yourscore;
    private void getMessage()
    {

        while (true)
        {
            stream = clientSocket.GetStream();
            int BUFFERSIZE = clientSocket.ReceiveBufferSize;
            byte[] buffer = new byte[BUFFERSIZE];
            int bytes = stream.Read(buffer, 0, buffer.Length);

            string message = Encoding.Unicode.GetString(buffer, 0, bytes);
            Debug.Log(message);

            // P-1 / P-2 점수 비교
            // 점수 높은 플레이어한테 승리를 보내야함 

            if (message.IndexOf("Ready") >= 0)
            {
                //SketchCharacterBulider.pushSaveCharacter(message);
                //레디 버튼 처리
                //-> 플레이어 게임룸 이동 
            }
            else if (message.IndexOf("FoodCompleted") >= 0)
            {
                //DanceEventKey.pushKey(message);
                //-> 강도 출현 메소드 호출
            }

            else if (message.IndexOf("myScore") >= 0)
            {
                //게임 끝났을때
                sendMessage("myScore_1000");
                judgeFlag = true;


                string[] _spllit = message.Split('_');
                try
                {
                    yourscore = int.Parse(_spllit[1]);
                }
                catch (Exception e)
                {
                    print(e);
                }


                //Game Finish 
                if (ResultUI.instance.score > yourscore)
                {
                    //내가 이긴거
                    sendMessage("result_Lose");
                    //팝업 띄우기
                    text.text = "Lose";
                }
                else if (ResultUI.instance.score < yourscore)
                {
                    //내가 진거
                    sendMessage("result_Win");
                    //팝업 띄우기
                    text.text = "Win";
                }
                else if (ResultUI.instance.score == yourscore)
                {
                    //동점
                    sendMessage("result_Draw");
                    //팝업 띄우기
                    text.text = "Draw";
                }

            }
            else if (message.IndexOf("result_") >= 0)
            {
                //팝업 띄우기
                string[] _spllit = message.Split('_');
                text.text = _spllit[1];
            }
            //Thread.Sleep(100);
            //Console log
        }
    }

    public void callGameFinish(string finishScore)
    {
        sendMessage("myScore :" + finishScore);
    }


    public void sendMessage(string message)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(message + "$");
        stream.Write(buffer, 0, buffer.Length);
        stream.Flush();
    }
}