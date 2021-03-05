//This script uses the system time to position the hands of a clock. You may assign as many or few hands as you wish, but I recommend you use all 3!
//And of course you can use your own watch model, should work with any as long as the hands are
//all at 12:00 when their rotation is zeroed and their local Z axis is perpendicular to the clock face

using UnityEngine;
using System.Collections;
using System;
//using System.Diagnostics.Eventing.Reader;

public class RealClock : MonoBehaviour
{
    MeshRenderer meshRenderer;

    public Transform Seconds;
    [SerializeField] [Range(0f, 1f)] float lerptime;

    [SerializeField] Color[] mycolors;

    int colorindex = 0;
    float t = 0f;
    int len;
    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        len = mycolors.Length;

    }

    // Update is called once per frame
    void Update()
    {
        float second = System.DateTime.Now.Second;
        float millisecond = System.DateTime.Now.Millisecond;

        if (Seconds)
            Seconds.localRotation = Quaternion.Euler(second / 4 * 360, 0, 0);


        meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, mycolors[colorindex], lerptime);
        t = Mathf.Lerp(t, 1f, lerptime * Time.deltaTime);
        if (t > 0.9f)
        {
            t = 0f;
            colorindex++;
            colorindex = (colorindex >= len) ? 0 : colorindex;
        }
        Destroy(gameObject, 20);
    }

}

