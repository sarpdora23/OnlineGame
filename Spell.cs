﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          
       // Destroy(gameObject,1f);
    }
    private void Awake()
    {
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("denek");
       // Destroy(gameObject);

    }
}
