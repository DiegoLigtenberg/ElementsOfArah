using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedActivation : MonoBehaviour
{

    public GameObject gameobject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1.5f);
        gameobject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
