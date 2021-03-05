using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut fadeInOut;
    CanvasGroup canvas;
    float alpha;
    float start = 0;
    float end = 1;
    float fadeTime = 1;
    float timer;
    bool isPlaying = false;
    public bool loadStageRoom = false;
    private void Awake()
    {
        fadeInOut = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
    }
    
    public IEnumerator FadeOut()
    {
        isPlaying = true;
        if (isPlaying)
        {
            timer = 0;
            canvas.alpha = Mathf.Lerp(start, end, timer);
            while (canvas.alpha < 1)
            {
                timer += Time.deltaTime / fadeTime;
                canvas.alpha = Mathf.Lerp(start, end, timer);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1);
        if (SceneManager.GetActiveScene().name.Equals("Intro")) {
            SceneManager.LoadScene("WaitingRoom");
        }
        if (SceneManager.GetActiveScene().name.Equals("WaitingRoom")) {
            SceneManager.LoadScene("StageRoom");
        }
        if (SceneManager.GetActiveScene().name.Equals("StageRoom")) {
            if (loadStageRoom) {
                SceneManager.LoadScene("StageRoom");
                loadStageRoom = false;
            }
            else {
                SceneManager.LoadScene("WaitingRoom");
            }
            
        }



    }

    public void ScreenBlack()
    {
        //canvas.alpha = 1;
        print(alpha);
        while (true)
        {
            timer += Time.deltaTime/100; 
            print(alpha);
            canvas.alpha = Mathf.Lerp(start, end, timer);
            if (timer > 1)
            {
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
