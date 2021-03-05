using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public static ButtonClickSound instance;
    public AudioClip buttonClickSound;
    public AudioSource audioSource;
    //public AudioClip bgm;

    private void Awake()
    {
        if(ButtonClickSound.instance == null)
        {
            ButtonClickSound.instance = this;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void PlayBtnSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
