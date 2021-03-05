using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerExit : MonoBehaviour {
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("손님 퇴장");
        audioSource.Play();
    }
}
