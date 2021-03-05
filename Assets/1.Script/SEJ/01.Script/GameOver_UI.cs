using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickHome()
    {
        Destroy(GameObject.Find("StageNum"), 0);
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());
    }

    public void OnClickRestart()
    {
        FadeInOut.fadeInOut.loadStageRoom = true;
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());
    }
}
