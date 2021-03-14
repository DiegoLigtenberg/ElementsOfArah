using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyYRotation : MonoBehaviour
{

    public Transform copyYrot;
    private Quaternion copy;
    // Start is called before the first frame update
    void Start()
    {
        copyYrot = GameObject.Find(ActivePlayerManager.ActivePlayerName).transform;
    }

    private int a = -1;


    // Update is called once per frame
    void Update()
    {
        //  copy =  Quaternion.Euler(0, copyYrot.eulerAngles.y,0);

        //  copy = this.transform.rotation;

        //  this.transform.position = copyYrot.transform.position;

        //GIMBAL LOCK HERE YOU GO
        //this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x,  a * copyYrot.eulerAngles.y, this.transform.rotation.eulerAngles.z);
        //  this.transform.rotation = new Quaternion( this.transform.rotation.w ,this.transform.rotation.x,  copyYrot.rotation.y, this.transform.rotation.z);
    }

    private void LateUpdate()
    {
        this.transform.position = copyYrot.transform.position;
    }
}
