using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour
{
    Image btnImg;

    Sprite idleImg;
    Sprite onImg;
    Sprite offImg;

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
        btnImg = GetComponent<Image>();
        state = State.Idle;
        idleImg = Resources.Load<Sprite>("BtnImages/Btn_Idle");
        onImg = Resources.Load<Sprite>("BtnImages/Btn_On");
        offImg = Resources.Load<Sprite>("BtnImages/Btn_Off");
        SetButton(State.Idle);
    }

    public void SetButton(State state)
    {
        switch (state)
        {
            case State.Idle:
                /*if (this.state == state)
                    return;*/
                btnImg.sprite = idleImg;
                //print("Idle state");
                break;
            case State.On:
                /*if (this.state == state)
                    return;*/
                //print("On state");
                btnImg.sprite = onImg;
                break;
            case State.Off:
                //print("off state");
                /*if (this.state == state)
                    return;*/
                btnImg.sprite = offImg;
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
