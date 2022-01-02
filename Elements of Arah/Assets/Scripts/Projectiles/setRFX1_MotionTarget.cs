using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRFX1_MotionTarget : MonoBehaviour
{
    public RFX1_TransformMotion tm;
    public RFX1_TransformMotionHitPlayer tmp;
    public string Target;
    // Start is called before the first frame update
    void Awake()
    {
        //remove thi if statement it was for testing arrow effect
        if (this.gameObject.name == "Collision NODAMAGE ARROW")
        {
            if (tm != null)
            {
                GameObject.Find("arrowpos").transform.position = Gun.clonePosition2;
                Target = GameObject.Find("arrowpos").gameObject.name;
            }            
        }

        if (tm != null)
        {
            tm.Target = GameObject.Find(Target);
        }
     

        if (tmp != null)
        {
            tmp.Target = GameObject.Find(Target);
        }
     
    
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Collision NODAMAGE")
        {
            if (tm != null)
            {
                tm.Target = GameObject.Find(Target);
            }


        }

        if (this.gameObject.name == "Collision NODAMAGE ARROW")
        {
            if (tm != null)
            {
                GameObject.Find("arrowpos").transform.position = Gun.clonePosition2;
                Target = GameObject.Find("arrowpos").gameObject.name;
                tm.Target = GameObject.Find(Target);
            }


        }
    }
}
