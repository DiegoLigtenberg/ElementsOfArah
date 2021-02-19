using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBasicAttackDmg : MonoBehaviour
{
    public RFX1_TransformMotion setdmg;
    // Start is called before the first frame update
    void Start()
    {
        setdmg.damage = DamageManager.basicAttackDMG;
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
