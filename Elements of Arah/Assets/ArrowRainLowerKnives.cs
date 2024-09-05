using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainLowerKnives : MonoBehaviour
{

    public float speed;
    private float rng_range;

    // Start is called before the first frame update
    void Start()
    {
        rng_range = 6;
        rng_range = Random.Range(-rng_range / 2, rng_range / 2);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(0, 0, (speed+rng_range) * Time.deltaTime);
    }
}
