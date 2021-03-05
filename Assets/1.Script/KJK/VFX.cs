using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public GameObject particle;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = new Vector3(0.2f, 0, 0.1f);
        GameObject part = Instantiate(particle);
        part.transform.position = transform.position + dir;

        audioSource.Play();
        //Debug.Log("myname: "+ transform.name+ ", Other Name: " + collision.transform.name);
    
    }
       
}
