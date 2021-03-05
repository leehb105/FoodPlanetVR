using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Material mat; //변경할 Material
    public GameObject water; //변경할 물체
    Vector3 openPos;

    void Start()
    {
        openPos = transform.position + Vector3.up * 20;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Name " + other.gameObject.name);
        if(other.gameObject.name == "Eye")
        {
            water.GetComponent<MeshRenderer>().material = mat;
        }
    }
    public void Up()
    {
        transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime * 0.3f);
    }
}
