using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class StartButton : MonoBehaviour {

    [Header("Start 알림")]
    public GameObject startpopupObj;

    public AudioSource audioSource;

    public PlayableDirector pd;
    public TimelineAsset tl;



    // Start is called before the first frame update
    private void Awake() {
        startpopupObj.SetActive(false);
    }

    void Start() {
        //pd.Pause();
        audioSource.GetComponent<AudioSource>();
        pd.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision other) {
        PlayStartAnim(other.transform.tag);
        //Debug.Log("뭐임?뭐랑부딪힘?: " + other.transform.name);
        //startpopupObj.SetActive(true);
    }

    public void PlayStartAnim(string tag) {
        
        //if(tag == )
        pd.playableGraph.GetRootPlayable(0).SetSpeed(1);
        audioSource.enabled=true; 
        GameManager.Instance.CheckGameStart();
        Destroy(this.gameObject, 0);
    }

}
