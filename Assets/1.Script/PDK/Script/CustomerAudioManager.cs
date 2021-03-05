using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAudioManager : MonoBehaviour {

    public enum State {
        none,
        Enter,
        Order,
        Good,
        Bad,
    }
    public State state;

    private List<AudioSource> customerAS = new List<AudioSource>();

    private void Start() {
        checkChild();
    }

    void checkChild() {
        foreach (Transform child in transform) {
            customerAS.Add(child.GetComponent<AudioSource>());
        }
    }

    public void playSound(State st) {
        state = st;
        switch (state) {
            case State.Enter: customerAS[0].Play(); break;
            case State.Order: customerAS[1].Play(); break;
            case State.Good: customerAS[2].Play(); break;
            case State.Bad: customerAS[3].Play(); break;
            default: break;
        }
    }


}
