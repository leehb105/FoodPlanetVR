using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(0, 0, -50);
        transform.position = transform.forward + 10f * dir * Time.deltaTime;
        
    }
}
