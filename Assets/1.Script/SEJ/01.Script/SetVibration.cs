using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVibration : MonoBehaviour
{
    public static SetVibration setVibration;

    void Start()
    {
       
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
        {
            OVRInput.SetControllerVibration(0.3f, 0.3f, OVRInput.Controller.RTouch);
            StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.RTouch));

        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch))
        {
            OVRInput.SetControllerVibration(0.3f, 0.3f, OVRInput.Controller.LTouch);
            StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.LTouch));
        }

    }
    IEnumerator VibrateController(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}
