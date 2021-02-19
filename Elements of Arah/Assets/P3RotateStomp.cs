using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3RotateStomp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        var randrot = Random.Range(-35, 35);

        transform.rotation = Quaternion.Euler(0, GameObject.Find("Warrior Idle").transform.rotation.eulerAngles.y +randrot, 0);
    }
}
