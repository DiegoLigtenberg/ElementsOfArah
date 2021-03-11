using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerOnCollisionBossAA : MonoBehaviour
{

    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {

        ParticleSystem ps = GetComponent<ParticleSystem>();
        var sub = ps.subEmitters;
        sub.collision0.Play();

    }



    void OnParticleCollision(GameObject other)
    {
        var sub = ps.subEmitters;
        sub.collision0.Play();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
