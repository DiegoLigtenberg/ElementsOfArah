using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceYScale : MonoBehaviour
{
    public float timeleft;
    // Start is called before the first frame update
    void Start()
    {
        timeleft = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        if(timeleft < 0)
        {
            if (this.transform.localScale.x >= 0.15)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * .99f, this.transform.localScale.z);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
         
        }
    }
}
