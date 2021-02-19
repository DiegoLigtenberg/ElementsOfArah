using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpin : MonoBehaviour
{
    public Vector3 currentRotation;
    public Vector3 currentRotationcum;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float amount;

    public Vector3 anglesToRotate;

    Vector3 Startpos;

    public float frequency = 4f;    //movement speed
    public float amplitude = 2f;    //movement amount

    float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {

        // if ()
        {
            Startpos = transform.localPosition;
            rb.maxAngularVelocity = 7f;
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        //rotation around X axis
        Quaternion rotationX = Quaternion.AngleAxis(anglesToRotate.x * Time.deltaTime, new Vector3(1f, 0f, 0f));
        //rotation around Y axis
        Quaternion rotationY = Quaternion.AngleAxis(anglesToRotate.y * Time.deltaTime, new Vector3(0f, 1f, 0f));
        //rotation around Z axis
        Quaternion rotationZ = Quaternion.AngleAxis(anglesToRotate.z * Time.deltaTime, new Vector3(0f, 0f, 1f));

        this.transform.rotation = this.transform.rotation * rotationX * rotationY * rotationZ;

        currentRotation = transform.rotation.eulerAngles;

        // if ()
        {
          //  rb.maxAngularVelocity = 15f;
        }

        if (rb.angularVelocity.y < 5)
        {
            rb.AddTorque(transform.up * amount, ForceMode.Acceleration);
        }


       // anglesToRotate = new Vector3(50 + 90 * (float)Mathf.Sin(currentRotation.magnitude / 180), 50 + 60 * Mathf.Abs((float)Mathf.Cos(currentRotation.magnitude / 180)), 90);

        elapsedTime += Time.deltaTime * Time.timeScale * frequency;
        transform.localPosition = Startpos + Vector3.up * 1 / 2 * Mathf.Sin(elapsedTime) * amplitude;


      
    }





    



}
