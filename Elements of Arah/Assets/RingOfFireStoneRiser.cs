using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFireStoneRiser : MonoBehaviour
{


    // Start is called before the first frame update
    public float start_delay;
    public float timer;
    public float max_height;
    public float height;
    [Range(1f, 3f)]
    public float increase_speed;
    public BoxCollider bc;
    void Start()
    {
        increase_speed /= 100f;
    }

    // Update is called once per frame
    void Update()
    {
         timer += Time.deltaTime;
      

        if (timer  > start_delay && timer < 11)
        {
            bc.enabled = true;
            if (height <= max_height)
            {
                height += Time.deltaTime;
            }
            for (int i = 0; i < 100; i++)
            {
                if (height <= max_height)
                {
                    float temp_height = Time.deltaTime * increase_speed;
                    transform.position += new Vector3(0, temp_height, 0);

                }
            }
        }

        if (timer > 12.5f)
        {
            for (int i = 0; i < 100; i++)
            {
                float temp_height = Time.deltaTime * increase_speed;
                transform.position -= new Vector3(0, temp_height, 0);

                
            }
        }
    }
}
