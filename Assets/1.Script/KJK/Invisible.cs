using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Invisible : MonoBehaviour
{
    /*public SkinnedMeshRenderer smr;*/
    private DistanceGrabber dg;

    //public GameObject leftHands;
   // public GameObject rightHands;

    //SkinnedMeshRenderer leftSR;
    //SkinnedMeshRenderer rightSR;
    SkinnedMeshRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        /*  smr = GetComponent<SkinnedMeshRenderer>();*/
        dg = GetComponent<DistanceGrabber>();
        //leftSR = leftHands.GetComponent<SkinnedMeshRenderer>();
        //rightSR = rightHands.GetComponent<SkinnedMeshRenderer>();
        sr = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }
    public void ControllSkinMesh()
    {
        
        if (dg.grabbedObject)
        {
            //smr.enabled = false;
            sr.enabled = false;
        }
        if (!dg.grabbedObject)
        {
            //smr.enabled = true;
            sr.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ControllSkinMesh();
    }
}
