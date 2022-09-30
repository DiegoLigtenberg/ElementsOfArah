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
    public float size_arrow_particle = 1.5f;
    public float max_size;
    public bool collision_shrink;
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
        pMain.maxParticles = 5000;   
     
        if(timer < 0.65 && !shrink && timer * size_arrow_particle < max_size) //1.5f
        {
            timer += Time.deltaTime;
            if (timer < 0.1f )
            {

               // Debug.Log(timer * size_arrow_particle);
                pMain.startSize = new ParticleSystem.MinMaxCurve(0f, 0.7f * Mathf.Max(0, timer *size_arrow_particle));
            }
            else
            {
                pMain.startSize = new ParticleSystem.MinMaxCurve(0f, Mathf.Max(0, timer * size_arrow_particle));
            }
          
        }
        else
        {
            shrink = true;
            //save current size, and then slowly reduce instead of instant
        }
        if (shrink)
        {

            if (timer > 0.3f) //0.5f
            {

                timer -= 0.05f * Time.deltaTime;
               // pMain.startSize = new ParticleSystem.MinMaxCurve(0f, timer*1.5f);
            }

        }

        if (collision_shrink && size_arrow_particle > 0)
        {
            //timer -= 0.05f * Time.deltaTime;
            size_arrow_particle -= Time.deltaTime;
            pMain.startSize = new ParticleSystem.MinMaxCurve(0f, timer*size_arrow_particle);
        }


        // Debug.Log(timer);

    }
}
