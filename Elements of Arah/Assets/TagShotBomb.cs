﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagShotBomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.magnitude < 3)
        {
            transform.localScale += new Vector3(0.007f, 0.007f, 0.007f);
        }
    }
}
