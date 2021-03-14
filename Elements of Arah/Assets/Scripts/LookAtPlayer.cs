using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find(ActivePlayerManager.ActivePlayerName).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
