using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicAttackSpin2 : MonoBehaviour
{
    public Vector3 currentRotation;
    public Vector3 currentRotationcum;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float amount;

    public Vector3 anglesToRotate;

    Vector3 Startpos;

    public float frequency = 4f;    //movement speed
    public float amplitude = 2f;    //movement amount
    public float maxspeed = 20f;
    float elapsedTime = 0f;

    public Transform pos;

    public Vector3 readableAngles;



    [HideInInspector] public float XrotationAbil;
    [HideInInspector] public float YrotationAbil;
    [HideInInspector] public float ZrotationAbil;


    public int x;
    public int y;
    public int z;
    public GameObject player;
    public int castIteration;

    // Start is called before the first frame update
    void Start()
    {



        // if ()
        {
           // Startpos = transform.position;
            rb.maxAngularVelocity = 10f;
        }

    }

    private int a = 1;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(tf.rotation);

        if (castIteration == 1 || castIteration == 3)
        {
            a = 1;
        }

        if (castIteration == 2)
        {
            a = -1;
        }

        float rand = Random.Range(.7f, 1.3f);
        //rotation around X axis
        Quaternion rotationX = Quaternion.AngleAxis(anglesToRotate.x * Time.deltaTime, new Vector3(-a * 1.5f * rand, 0f, 0f));
        //rotation around Y axis
        Quaternion rotationY = Quaternion.AngleAxis(anglesToRotate.y * Time.deltaTime, new Vector3(0f, -a * 1.5f * rand, 0f));
        //rotation around Z axis
        Quaternion rotationZ = Quaternion.AngleAxis(anglesToRotate.z * Time.deltaTime, new Vector3(0f, 0f, -a * 1.5f));

        this.transform.rotation = this.transform.rotation * rotationX * rotationY * rotationZ;

        currentRotation = transform.rotation.eulerAngles;


        readableAngles += 9f * rotationX.ToEulerAngles();


        if (readableAngles.x >= 55.82247)
        {
            // readableAngles = Vector3.zero;
        }
        /*
        if (readableAngles.x > 15 && readableAngles.x < 45)
        {
            go.SetActive(true);
        }
        else
        {
            go.SetActive(false);
            rotationX = new Quaternion(0, 0, 0, 0);
        }
        */
      //  rb.AddTorque(Vector3.down * 1,ForceMode.Force);

        //if (currentRotation.magnitude > 200)
        {
        //    anglesToRotate = new Vector3(x + 50 + 90 * (float)Mathf.Sin(currentRotation.magnitude / 180 * rand), y + 50 + 60 * Mathf.Abs((float)Mathf.Cos(currentRotation.magnitude / 180)), z + 50 + 16 * Mathf.Abs((float)Mathf.Cos(currentRotation.magnitude / 180) + 20));

        }
        
        {
           // anglesToRotate = new Vector3(x + 50 + 90 * (float)Mathf.Sin(360/ 1800 * rand), y + 50 + 60 * Mathf.Abs((float)Mathf.Cos(360 / 1800)), z + 50 + 16 * Mathf.Abs((float)Mathf.Cos(currentRotation.magnitude / 180) + 20));

        }


        //was 0,300,0
        //  anglesToRotate = new Vector3(x,y,z);
        anglesToRotate = new Vector3(x + xconst, y + yconst, z);

        elapsedTime += Time.deltaTime * Time.timeScale * frequency;
        //   transform.position = Startpos + Vector3.up * 1 / 2 * Mathf.Sin(elapsedTime) * amplitude;

        if (Time.time - lastStep > timeBetweenSteps)
        {
            lastStep = Time.time;
            float rand2 = Random.Range(0.5f, 3.0001f);
            
            if (xminplus)
            {
                xconst = xconst - 6.2f * rand2;
            }
            if (!xminplus)
            {
                xconst = xconst + 3.2f * rand2;
            }

            /*
            if (yminplus)
            {
                yconst = yconst - 1.8f * rand2;
            }
            if (!yminplus)
            {
                yconst = yconst + 1.8f * rand2;
            }
            */
            if (xconst <= -30)
            {
                xminplus = false;

                // xconst = 70;
            }
            if (xconst >= 30)
            {
                xminplus = true;
            }
            
            if (yconst <= -20)
            {
                yminplus = false;
                //  yconst = 70;
            }
            if (yconst >= 20)
            {
                yminplus = true;
            }
            

        }
        this.transform.position = player.transform.position + new Vector3(0, 1, 0);

    }


    private void LateUpdate()
    {

     //   this.transform.position = player.transform.position + new Vector3(0, 1, 0);
    }
    float lastStep, timeBetweenSteps = 0.1f;
    private float xconst = 5;
    private float yconst = 5;

    private bool xminplus;
    private bool yminplus;



}
