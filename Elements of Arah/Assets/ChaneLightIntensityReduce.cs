using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaneLightIntensityReduce : MonoBehaviour
{
    public Light light;
    public GameObject[] totem;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(closeself());
    }


    public IEnumerator closeself()
    {
        yield return new WaitForSeconds(2);
        totem[0].gameObject.SetActive(false);
        totem[1].gameObject.SetActive(false);
        totem[2].gameObject.SetActive(false);
        totem[3].gameObject.SetActive(false);
        totem[4].gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (light.innerSpotAngle > 0 )
        {
            light.innerSpotAngle -= 0.2f;
        }

        if (light.spotAngle > 0)
        {
            light.spotAngle += 0.35f;
        }



    }
}
