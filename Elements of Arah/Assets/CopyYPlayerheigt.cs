using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyYPlayerheigt : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = ActivePlayerManager.ActivePlayerGameObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        this.transform.position = new Vector3(this.transform.position.x,  Mathf.Clamp(  player.transform.position.y , 0, 0.4f* player.transform.position.y +40)  , this.transform.position.z);
    }
}
