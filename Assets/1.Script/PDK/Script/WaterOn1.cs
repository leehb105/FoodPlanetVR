using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOn1 : MonoBehaviour {
    public GameObject water;
    public GameObject blackDrink;
    public GameObject greenDrink;
    GameObject greenDrinkFactory;
    GameObject blackDrinkFactory;
    private AudioSource audioSource;

    string myname;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        myname = transform.name;
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "CUP") {
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.transform.position = transform.position + new Vector3(0, 0.03f, 0);
            water.SetActive(true);
            StartCoroutine(Wait());
            Destroy(other.gameObject, 0);
            if (myname == "GreenDrinkDispenser") {
                greenDrinkFactory = Instantiate(greenDrink);
                greenDrinkFactory.name = greenDrink.name;
                greenDrinkFactory.transform.position = transform.position + new Vector3(0, 0.03f, 0);
            }
            else if (myname == "BlackDrinkDispenser") {
                blackDrinkFactory = Instantiate(blackDrink);
                blackDrinkFactory.name = blackDrink.name;
                blackDrinkFactory.transform.position = transform.position + new Vector3(0, 0.03f, 0);
            }
        }
    }
    IEnumerator Wait() {
        audioSource.Play();
        yield return new WaitForSeconds(1.0f);
        water.SetActive(false);
    }
}
