using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager soundMN;
    private void Awake() {
        soundMN = this;

    }
    AudioSource sound;
    [SerializeField] AudioClip btnClick;
    [SerializeField] AudioClip enhance;
    [SerializeField] AudioClip charging;

    // Start is called before the first frame update
    void Start() {
        sound = GetComponent<AudioSource>();
        //sound.Play();
        sound.playOnAwake = false;
        //sound.mute = true;

        if (DB.instance.Volume == 0) {
            Debug.Log("DB.instance.Volume == " + DB.instance.Volume);
            StopSound();
        }
        else {
            Debug.Log("DB.instance.Volume:" + DB.instance.Volume);
            PlaySound();
        }

    }
    public void Charging() {
        sound.PlayOneShot(charging);
        print("사운드 재생 완료 ");
    }
    public void EnhanceSound() {
        sound.PlayOneShot(enhance);
    }
    public void BtnSound() {
        sound.PlayOneShot(btnClick);
    }
    //밑에 PlaySound, StopSound 이송이, 심은진 메소드 겹침 맞는걸로 수정할 것
    //public void PlaySound()
    //{
    //    sound.volume = 0.6f;
    //    sound.PlayOneShot(bgm);
    //}
    //public void StopSound()
    //{
    //    sound.Stop();
    //}


    public void PlaySound() {
        Debug.Log("사운드 뮤트 false하고 켰음");
        sound.mute = false;
        sound.Play();
    }
    public void StopSound() {
        Debug.Log(sound.mute);
        if (sound.mute) {
            //Debug.Log("이미 꺼져있음");
            return;
        }
        else {
            //Debug.Log("켜져있으니 끌거임");
            sound.mute = true;
        }
    }
    // Update is called once per frame
    void Update() {

    }
}
