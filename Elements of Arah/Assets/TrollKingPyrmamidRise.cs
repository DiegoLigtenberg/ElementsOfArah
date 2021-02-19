using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollKingPyrmamidRise : MonoBehaviour
{

    public GameObject smoke;
    public float increase = 0.03f;

    private float timeleft;
    private float timeleftdecrease;
    // Start is called before the first frame update
    void Start()
    {
        smoke.SetActive(true);
        increase = 0.002f;
    }

  
    private bool ready = false;
    public IEnumerator startdelay()
    {
        yield return new WaitForSeconds(2f);
        increase = 0.002f;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        timeleft -= Time.deltaTime;


        if (timeleft *Time.deltaTime < 0 && !TrollPhasingLaser.isbeaming && !ready)
        {
            for ( int i = 0; i < 100; i++)
            {
                if (transform.position.y < 64.67809)
                {
                    this.transform.position = new Vector3(transform.position.x, ( transform.position.y + Mathf.Max(Mathf.Min(0.7f * increase, 0.00042f), 0.00025f)), transform.position.z);
                    increase *= 10.15f;
                    timeleft = 0.001f;
                }
                timeleft = 0.0001f;
            }
   
        }
        if (TrollPhasingLaser.isbeaming)
        {
            StartCoroutine(startdelay());
       
        }

        if (ready)
        {
            for (int i = 0; i < 100; i++)
            {
                if (transform.position.y >-30)
                {
                    this.transform.position = new Vector3( transform.position.x, transform.position.y - Mathf.Max(Mathf.Min(0.7f * increase, 0.00042f), 0.00025f), transform.position.z);
                    increase *= 10.15f;
                    timeleft = 0.001f;
                }
                timeleft = 0.0001f;
            }
        }
 
    }
}
