using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerOnce : MonoBehaviour
{
    [SerializeField] private Transform target;
    // Start is called before the first frame update

    private float lastStep_1, timeBetweenSteps_1 = 1f;
    void Start()
    {
        target = GameObject.Find(ActivePlayerManager.ActivePlayerName).transform;
    }

    private bool stop;
    private int count;
    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep_1 > timeBetweenSteps_1)
        {
            lastStep_1 = Time.time;
            count++;
            Debug.Log("happened to early?");
            Debug.Log(count);
            if (count >= 2)
            {

                stop = true;
            }

        }


        if (target != null && !stop)
        {
            //  transform.LookAt(target);
        }
    }
}