using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_Skip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {



    }
    public void OnClickStart() {
        SceneManager.LoadScene("WaitingRoom");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
