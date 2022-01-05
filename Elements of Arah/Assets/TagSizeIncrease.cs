using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSizeIncrease : MonoBehaviour
{
    private ParticleSystem ps;
    private float timer;
    //access the particle module, in this case MainModule
    private ParticleSystem.MainModule pMain;

    private bool shrink;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        pMain = ps.main;

        timer = 0.01f - delay;
    }

    // Update is called once per frame
    void Update()
    {
        pMain.maxParticles = 1000;   
     
        if(timer < .75f && !shrink) //1.5f
        {
            timer += Time.deltaTime;
            if (timer < 0.1f )
            {
                pMain.startSize = new ParticleSystem.MinMaxCurve(0f, 0.5f * Mathf.Max(0, timer * 1.5f));
            }
            else
            {
                pMain.startSize = new ParticleSystem.MinMaxCurve(0f, Mathf.Max(0, timer * 1.5f));
            }
          
        }
        else
        {
            shrink = true;
        }
        if (shrink)
        {

            if (timer > 0.3f) //0.5f
            {

                timer -= Time.deltaTime;
                pMain.startSize = new ParticleSystem.MinMaxCurve(0f, timer*1.5f);
            }

        }
     

        Debug.Log(timer);
        
    }
}
