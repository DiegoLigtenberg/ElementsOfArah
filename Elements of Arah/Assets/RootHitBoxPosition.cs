using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHitBoxPosition : MonoBehaviour
{
    public BoxCollider bc;
    public MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponentInChildren<BoxCollider>();
        mr = GetComponentInChildren<MeshRenderer>();
        StartCoroutine(enabledelay());
    }

    public IEnumerator enabledelay()
    {
        yield return new WaitForSeconds(0.2f);
        bc.enabled = true;
       // mr.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = ReadPathFindingPosition.pathFindPos; // + new Vector3(0,0,10);
      


    }



    private void LateUpdate()
    {
       
    }
}
