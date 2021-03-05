using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceScript : MonoBehaviour {
    Rigidbody myRigidbody;
    BoxCollider myCollider;

    void Start() {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "DISH") {
            //transform.SetParent(other.transform);
            //접시에 올라간뒤로는 플레이어가 오브젝트를 따로 잡을수 없도록,
            //DistanceGrabbable 끄는걸로 했는데도 잡혀서 layer를 0로 변경하는방식으로 진행
            //transform.GetComponent<DistanceGrabbable>().enabled = false;
            gameObject.layer = 0;

            myCollider.isTrigger = true;
            myRigidbody.useGravity = false;
            myRigidbody.isKinematic = true;

        }
    }
}
