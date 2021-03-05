using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate_Heart : MonoBehaviour
{
    float speed = 60f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
    public void Rotation()
    {
        //transform.localRotation = Quaternion.Euler(0, 90, 0);
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
