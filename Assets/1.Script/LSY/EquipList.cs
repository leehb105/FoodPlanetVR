using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipList : MonoBehaviour
{
    public enum Equip
    {
        Hammer,
        Grill,
        Knife,
    }
    private Equip equip;
    private int level;
    public void SetEquipList(Equip equip, int level)
    {
        this.equip = equip;
        this.level = level;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
