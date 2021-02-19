using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : MonoBehaviour
{

    public Transform cameratransform;
    private float temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
      //  Debug.Log(Gun.aimhelp);
        if (Gun.aimhelp)
        {

            // this.transform.rotation = cameratransform.rotation;


            //THIS PIECE BELOW SHOULD BE ON FOR AIMHELP!!!
            this.transform.eulerAngles = new Vector3((0.960f + temp) * cameratransform.eulerAngles.x, cameratransform.eulerAngles.y, cameratransform.eulerAngles.z);
            temp = Mathf.Clamp((Gun.offsetcamera / 200), 0, 0.040f);
        }
    }

    void Update()
    {

  

        //changes rotation of firepoint of abilities -> makes it so that it rotates correctly with cam
        if (!Gun.aimhelp)
        {
            this.transform.rotation = cameratransform.rotation;
         //   Debug.Log(Gun.offsetcamera);
               
        }

         if (Gun.aimhelp)
        {

           // this.transform.rotation = cameratransform.rotation;

       
          //THIS PIECE BELOW SHOULD BE ON FOR AIMHELP!!!
            this.transform.eulerAngles = new Vector3((0.960f + temp) * cameratransform.eulerAngles.x, cameratransform.eulerAngles.y, cameratransform.eulerAngles.z);

        }
       // Debug.Log(Gun.aimhelp);
       // Debug.Log(temp);
        temp =  Mathf.Clamp(   (Gun.offsetcamera / 200) ,0,0.040f);
        

 
       
    }
}
