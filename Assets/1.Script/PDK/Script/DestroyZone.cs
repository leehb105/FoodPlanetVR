using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objects")){
            Destroy(other.gameObject, 0);
            //사라지는 애니메이션 혹은 소리

            if(other.tag == "TOOL" && other.name.Contains("Hammer")) {
                ToolManager.Instance.ToolHammerRespawn();
            }
            if (other.tag == "TOOL" && other.name.Contains("Knife")) {
                ToolManager.Instance.ToolKnifeRespawn();
            }
        }
    }
}
