using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testestst : MonoBehaviour
{
    [SerializeField] ParticleSystem[] part;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < part.Length; i++)
        {
            part[i].Stop();
        }
        for (int i = 0; i < part.Length; i++)
        {
            part[i].Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (part[0].isStopped)
        {
            for (int i = 1; i < part.Length; i++)
            {
                part[i].Play();
            }
        }
/*        for (int i = 0; i < part.Length; i++)
        {
            print(part[i].isStopped);
            print(part[i].isPaused);
            print(part[i].isPlaying);
        }*/
    }
}
