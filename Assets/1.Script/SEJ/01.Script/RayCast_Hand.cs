using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RayCast_Hand : MonoBehaviour
{
    RaycastHit hitinfo;
    public GameObject soundManager;
    public GameObject vibrationManager;
    public GameObject menu;
    public TextMeshProUGUI volText;
    public TextMeshProUGUI vibText;
    
    Ray ray;
    void Start()
    {

    }
    void Update()
    {
        MenuControll();

    }
    public void MenuControll()
    {
        ray = new Ray(transform.position, transform.forward);
        //Debug.Log("aa");

        if (Physics.Raycast(ray, out hitinfo, 100000f))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch)) {
                if (hitinfo.collider.gameObject.CompareTag("Button")) {
                    hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
            switch (hitinfo.transform.gameObject.name)
            {
                case "Vol":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        Debug.Log("vo 999"+ hitinfo.transform.gameObject.name);
                        hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                     break;
                case "Vib":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    break;
                case "Close":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    break;
                case "Home":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    break;
                case "Restart":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    break;
                case "Exit":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch))
                    {
                        hitinfo.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                    break;
            }
        }
    }
}

