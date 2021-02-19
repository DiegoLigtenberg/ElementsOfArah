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
    }
}
