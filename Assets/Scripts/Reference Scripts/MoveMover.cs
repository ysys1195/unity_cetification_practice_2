﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 150000; i++)
        {
            GameObject.Find("Player");
        }
    }
}
