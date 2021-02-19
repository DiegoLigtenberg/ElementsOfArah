using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollAbilityFire : MonoBehaviour
{

    public GameObject[] effect;
    public Transform[] effectTransform;

    public Rigidbody rb;
    public Vector3 PlayerPositionSpawnPosition;

    public Animator anim;

    float lastStep, timeBetweenSteps = 3;
    float lastStep2, timeBetweenSteps2 = 5f;

    public BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPositionSpawnPosition = rb.position;


        if (Time.time - lastStep2 > timeBetweenSteps2)
        {
            lastStep2 = Time.time;

            Debug.Log("firing");

            StartCoroutine(basicAttack());
    
        }
    
        
        if (Time.time - lastStep > timeBetweenSteps)
        {
            lastStep = Time.time;

            Debug.Log("firing");
            Instantiate(effect[2], effectTransform[1].position, effectTransform[1].rotation);
            bc.enabled = true;

           // Instantiate(effect[0], PlayerPositionSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
        }
    }



    public IEnumerator basicAttack()
    {
        /*
        Debug.Log("hit");
        yield return new WaitForSeconds(2.1f);
        Instantiate(effect[1], effectTransform[0].position, effectTransform[0].rotation);
        yield return new WaitForSeconds(0.4f);
       // Instantiate(effect[1], effectTransform[0].position, effectTransform[0].rotation);
        yield return new WaitForSeconds(0.4f);
        Instantiate(effect[1], effectTransform[0].position, effectTransform[0].rotation);
        */


        yield return null;
    }

}
