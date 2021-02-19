using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowWhenSpawned : MonoBehaviour
{

    Renderer rend;
    public Color begincolor;
    public Color endcolor;
    private float timeElapsed;
    public float lerpDuration;
    // Start is called before the first frame update
    void Start()
    {
        rend = this.transform.parent.gameObject.GetComponent<Renderer>();
       // Debug.Log(rend.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.localScale.x < 2)
        {
            //grow fire
            this.transform.localScale = new Vector3(this.transform.localScale.x + 0.004f, this.transform.localScale.y + 0.004f, this.transform.localScale.z + 0.004f);

        }

        float t = timeElapsed / lerpDuration;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        rend.material.color  = Color.Lerp(begincolor, endcolor, t);
        timeElapsed += Time.deltaTime;
    }
}
