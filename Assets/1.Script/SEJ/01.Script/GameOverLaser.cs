using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLaser : MonoBehaviour
{
    public GameObject laserPointer;
    public GameObject gameOver;

    void Start()
    {
        
    }

    void Update()
    {
        if(gameOver.activeSelf == true)
        {
            laserPointer.SetActive(true);

        }
        else
        {
            laserPointer.SetActive(false);
            
        }
    }
}
