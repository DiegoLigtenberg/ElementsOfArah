using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

public class setTransformMotionDmg : MonoBehaviour
{
    public RFX1_TransformMotion tm;
    private BasicAttack ba;

    public int dmg;
    // Start is called before the first frame update
    void Start()
    {
        ba = GetComponent<BasicAttack>();
        Debug.Log(ba);
    }

    

    // Update is called once per frame
    void Update()
    {

        dmg = ba.AbilityDamage;
        Debug.Log("ability damage set to" + " " + GetComponent<BasicAttack>().AbilityDamage);
    }
}
