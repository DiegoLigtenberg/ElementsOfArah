﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CreatingCharacters.Abilities;

public class BasicAttackSphereSpin : MonoBehaviour
{
    public Vector3 currentRotation;
    public Vector3 currentRotationcum;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float amount;

    public Vector3 anglesToRotate;

    Vector3 Startpos;

    public float frequency = 4f;    //movement speed
    public float amplitude = 2f;    //movement amount
    public float maxspeed = 20f;
    float elapsedTime = 0f;

    public Transform pos;

    public Vector3 readableAngles;



    [HideInInspector] public float XrotationAbil;
    [HideInInspector] public float YrotationAbil;
    [HideInInspector] public float ZrotationAbil;


    public int x;
    public int y;
    public int z;

    public int castIteration;




    public Material m;

    // Start is called before the first frame update
    void Start()
    {
 

        // if ()
        {
            Startpos = transform.position;
            rb.maxAngularVelocity = 10f;
        }

    }

    private int a = 1;

    // Update is called once per frame
    void Update()
    {
        
        if (SunShine.SunShineActive)
        {
            m.EnableKeyword("_EMISSION");
     
        }
        if (!SunShine.SunShineActive)
        {
            m.DisableKeyword("_EMISSION");
        }



        // Debug.Log(tf.rotation);

        if (castIteration == 1 || castIteration == 3)
        {
            a = 1;
        }

        if (castIteration == 2)
        {
            a = -1;
        }

        //rotation around X axis
        Quaternion rotationX = Quaternion.AngleAxis(anglesToRotate.x * Time.deltaTime, new Vector3(a * 1f, 0f, 0f));
        //rotation around Y axis
        Quaternion rotationY = Quaternion.AngleAxis(anglesToRotate.y * Time.deltaTime, new Vector3(0f, a * 1f, 0f));
        //rotation around Z axis
        Quaternion rotationZ = Quaternion.AngleAxis(anglesToRotate.z * Time.deltaTime, new Vector3(0f, 0f, a * 1f));

        this.transform.rotation = this.transform.rotation * rotationX * rotationY * rotationZ;

        currentRotation = transform.rotation.eulerAngles;


        readableAngles += 9f * rotationX.ToEulerAngles();


        if (readableAngles.x >= 55.82247)
        {
            // readableAngles = Vector3.zero;
        }
        /*
        if (readableAngles.x > 15 && readableAngles.x < 45)
        {
            go.SetActive(true);
        }
        else
        {
            go.SetActive(false);
            rotationX = new Quaternion(0, 0, 0, 0);
        }
        */


        //anglesToRotate = new Vector3(50 + 90 * (float)Mathf.Sin(currentRotation.magnitude / 180), 50 + 60 * Mathf.Abs((float)Mathf.Cos(currentRotation.magnitude / 180)), 90);

        //was 0,300,0
        anglesToRotate = new Vector3(x, y, z);
        //anglesToRotate = new Vector3(x, y, z);

        elapsedTime += Time.deltaTime * Time.timeScale * frequency;
        //   transform.position = Startpos + Vector3.up * 1 / 2 * Mathf.Sin(elapsedTime) * amplitude;





    }






}
