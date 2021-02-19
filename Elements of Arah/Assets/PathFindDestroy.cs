using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void destroyme()
    {

        //StartCoroutine(latedestroy());
        Debug.Log("I GeT DESTROYED");
        //    this.gameObject.SetActive(false);
        Phase01AA.qbdspawned = false;
        Destroy(this.gameObject);
    }

    /*
    private IEnumerator latedestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
    */

    // Update is called once per frame
    void Update()
    {

        if (ReadPathFindingPosition.distancetoPlayer < 0.52 && ReadPathFindingPosition.distancetoPlayer != 0)
        {
            //this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
