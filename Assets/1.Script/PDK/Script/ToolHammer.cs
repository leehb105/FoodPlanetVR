using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHammer : MonoBehaviour {
    float powerLevel;
    public Rigidbody rb;
    public float speed = 0;
    FoodScript food;
    float gravityPower = 5;
    bool groundCheck;

    public GameObject particle;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        if (gameObject.transform.name.Contains("level3")) {
            powerLevel = 1f;
        }
        else if (gameObject.transform.name.Contains("level2")) {
            powerLevel = 0.5f;
        }
        else {
            powerLevel = 0.4f;
        }

    }

    // Update is called once per frame
    void Update() {
        speed = rb.velocity.magnitude;
        //if (!groundCheck) {
        //    transform.position = Vector3.down * gravityPower;
        //}
    }

    private void OnCollisionEnter(Collision other) {

        if (speed >= 3.0f) {
            GameObject part = Instantiate(particle);
            part.transform.position = other.transform.position;
            part.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            audioSource.Play();
            if (other.transform.tag == "FOOD") {
                food = other.transform.gameObject.GetComponent<FoodScript>();
                food.HP -= powerLevel;
                speed = 0;
            }
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.transform.tag == "FOOD") {
            food = null;
        }
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
    //        groundCheck = true;

    //    }

    //    if (speed >= 3.0f) {
    //        GameObject part = Instantiate(particle);
    //        part.transform.position = other.transform.position;

    //        audioSource.Play();
    //        if (other.transform.tag == "FOOD") {
    //            food = other.transform.gameObject.GetComponent<FoodScript>();
    //            food.HP -= powerLevel;
    //            speed = 0;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other) {
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
    //        groundCheck = false;
    //    }
    //    if (other.transform.tag == "FOOD") {
    //        food = null;
    //    }
    //}
}
