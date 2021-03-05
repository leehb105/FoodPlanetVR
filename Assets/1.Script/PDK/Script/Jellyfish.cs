using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour {
    public GameObject greenSauce;
    public GameObject purpleSauce;

    public GameObject greenParticle;
    public GameObject purpleParticle;

    GameObject sauceFactory;

    int myType;

    // Start is called before the first frame update
    void Start() {
        if (transform.name == "GreenJellyFIsh") {
            myType = 0;

        }
        else if (transform.name == "PurpleJellyFIsh") {
            myType = 1;

        }

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log("other.name: " + other.transform.name);
        if (myType == 0) {
            greenParticle.SetActive(true);
            StartCoroutine(GreenWait());
            sauceFactory = Instantiate(greenSauce);
            sauceFactory.name = "greenSauce";
            sauceFactory.transform.position = transform.position + new Vector3(0, -0.15f, 0);
        }
        else if (myType == 1) {
            purpleParticle.SetActive(true);
            StartCoroutine(PurpleWait());
            sauceFactory = Instantiate(purpleSauce);
            sauceFactory.name = "purpleSauce";
            sauceFactory.transform.position = transform.position + new Vector3(0, -0.15f, 0);
        }
    }

    IEnumerator GreenWait() {
        yield return new WaitForSeconds(1.0f);
        greenParticle.SetActive(false);
    }
    IEnumerator PurpleWait() {
        yield return new WaitForSeconds(1.0f);
        purpleParticle.SetActive(false);
    }


}
