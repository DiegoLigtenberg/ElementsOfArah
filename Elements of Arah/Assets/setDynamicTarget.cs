using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setDynamicTarget : MonoBehaviour
{
    public RFX1_TransformMotion tm;
    public RFX1_TransformMotionHitPlayer tmp;
    public string Target;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Target = Gun.targetname;


        if (tm != null)
        {
            tm.Target = GameObject.Find(Target);
        }


        if (tmp != null)
        {
            tmp.Target = GameObject.Find(Target);
        }
        
    }
}
