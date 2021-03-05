using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowState : MonoBehaviour
{
    GameObject btnImg;
    Vector3 originPosition;
    public enum State
    {
        Idle,
        On,
        Off,
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        btnImg = gameObject;
        originPosition = btnImg.transform.localScale;
        state = State.Idle;
        SetButton(State.Idle);
    }

    public void SetButton(State state)
    {
        switch (state)
        {
            case State.Idle:
                /*if (this.state == state)
                    return;*/
                //print("Idle state");
                btnImg.transform.localScale = originPosition;
                break;
            case State.On:
                /*if (this.state == state)
                    return;*/
                //print("On state");
                btnImg.transform.localScale = originPosition * 1.2f;
                break;
        }
    }
    public void SetStateIdle()
    {
        state = State.Idle;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
