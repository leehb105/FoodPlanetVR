﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipRotate_Knife : MonoBehaviour
{
    float speed = 90f;

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
        //장비 y축 기준으로 시계방향 회전
        transform.Rotate(new Vector3(90, 0, 0), speed * Time.deltaTime);
    }
}
