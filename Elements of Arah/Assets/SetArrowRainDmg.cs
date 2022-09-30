using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArrowRainDmg : MonoBehaviour
{
    public CollisionDamageSmallRocket setdmg;
    // Start is called before the first frame update
    void Start()
    {
        setdmg.damage = DamageManager.avalancheDMG;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
