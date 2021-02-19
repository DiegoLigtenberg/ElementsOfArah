using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.Rendering;

public class DelayRenderer : MonoBehaviour
{
    ParticleSystemRenderer ps;
    private Vector3 oldpos;
    private Vector3 newpos;
    private float cooldown;
    private bool startdelay;
    float lastStep, timeBetweenSteps = 0.1f;
    float lastStep2, timeBetweenSteps2 = 0.1f;

    // Start is called before the first frame update
    void Start()
    { 
        GetComponent<ParticleSystemRenderer>().shadowCastingMode = ShadowCastingMode.Off;
        //StartCoroutine(turnshadowon());
        StartCoroutine(startDelay());
    }

    public IEnumerator startDelay()
    {
        yield return new WaitForSeconds(0.05f);
        startdelay = true;
    }

        public IEnumerator turnshadowon()
    {
        yield return new WaitForSeconds(0.55f);
        GetComponent<ParticleSystemRenderer>().shadowCastingMode = ShadowCastingMode.On;
    }

    // Update is called once per frame
    void Update()
    {

        oldpos = this.transform.position;

        if (Time.time - lastStep > timeBetweenSteps)
        {
            lastStep = Time.time;
            oldpos = this.transform.position;

        }
        if (Time.time - lastStep2 > timeBetweenSteps2 && startdelay)
        {
            lastStep2 = Time.time;
            newpos = this.transform.position;

        }


        if (oldpos == newpos)
        {
            StartCoroutine(turnshadowon());
        }
    }
}
