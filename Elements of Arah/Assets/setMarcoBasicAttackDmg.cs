using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMarcoBasicAttackDmg : MonoBehaviour
{
    public AE_PhysicsMotion setdmg;

    // Start is called before the first frame update
    void Start()
    {
        setdmg.damage = DamageManager.basicAttackMarcoDMG;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
