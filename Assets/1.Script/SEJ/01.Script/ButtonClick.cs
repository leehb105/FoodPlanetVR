using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void Restart()
    {
        FadeInOut.fadeInOut.loadStageRoom = true;
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());

    }
    public void Home()
    {
        Destroy(GameObject.Find("StageNum"), 0);
        StartCoroutine(FadeInOut.fadeInOut.FadeOut());
    }
}
