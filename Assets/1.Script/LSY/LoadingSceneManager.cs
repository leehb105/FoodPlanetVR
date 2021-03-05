using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager instance;
    public static string nextScene;

    [SerializeField] Image progressBar;
    [SerializeField] Image backGround;
    [SerializeField] GameObject position;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //progressBar.fillAmount = 0;
        //StartCoroutine(LoadScene());
    }
    public void LoadScene(string sceneName)
    {

        //SceneManager.LoadScene(sceneName);
        StartCoroutine(CoLoadScene(sceneName));
    }

    IEnumerator CoLoadScene(string sceneName)
    {
        //yield return null;

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);

        //asyncScene.allowSceneActivation = false;

        float timer = 0.0f;

        while (!asyncScene.isDone)
        {
            yield return null;

            print("상태 : " +  asyncScene.progress);
            //timer += Time.deltaTime;

            //if (asyncScene.progress >= 0.9f)
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1, timer);

            //    if (progressBar.fillAmount == 1.0f)
            //    {
            //        asyncScene.allowSceneActivation = true;
            //    }
            //}
            //else
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, asyncScene.progress, timer);

            //    if (progressBar.fillAmount >= asyncScene.progress)
            //    {
            //        timer = 0f;
            //    }
            //}
            
        }

//asyncScene.allowSceneActivation = true;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha8))
        {
          LoadScene("StageRoom_KJK");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
           LoadScene("WaitingRoom");
        }*/
    }
}
