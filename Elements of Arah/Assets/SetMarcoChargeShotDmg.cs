using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMarcoChargeShotDmg : MonoBehaviour
{
    public AE_PhysicsMotion setdmg;


    // Start is called before the first frame update
    void Awake()
    {

        setdmg.damage = DamageManager.chargeshotMarcoDMG;



    }

    // Update is called once per frame
    void Update()
    {

    }
}
