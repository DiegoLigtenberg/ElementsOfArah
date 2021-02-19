using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SphereController : MonoBehaviour
{
 
    public SphereCollider sc;
    public Rigidbody rb;


    public GameObject[] effect;
    public Transform[] effectTransform;

    private bool onCooldown;
    public int CooldownDuration;


    private Vector3 test;

    public Image im;
    public AudioSource aus;

    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        test = new Vector3(2, 3, 4);
        Debug.Log(test.magnitude);
        rb.AddForce(new Vector3(0, 0, 10),ForceMode.Impulse);
    }



    // Update is called once per frame
    void Update()
    {
        if (!onCooldown)
        {
            im.color = Color.green;
        }
        if (onCooldown)
        {
            im.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onCooldown == false)
        {
            Instantiate(effect[0], effectTransform[0].position, Quaternion.identity);
            onCooldown = true;

            Invoke("RemoveCooldown", CooldownDuration);
            aus.Play();

        }


            if (rb.velocity.z < 35)
        {
            rb.AddForce(new Vector3(0, 0, 3));
        }
     



        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-15, 0, 0) );
            if (rb.velocity.x >= 0.05f)
            {
                rb.AddForce(new Vector3(-5 -(Mathf.Pow(rb.velocity.x, 1.2f)), 0, 0));
            }


        }


        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(15, 0, 0));
           // Debug.Log(rb.velocity.x);
            if (rb.velocity.x <= -0.05f)
            {
                Debug.Log("true");
                rb.AddForce(5 + Mathf.Abs( rb.velocity.x  )   , 0, 0);
            }
        }

        if (transform.position.y < -4)
        {
            SceneManager.LoadScene("DenizExample");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
     }

    public void RemoveCooldown()
    {
        onCooldown = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Map")
        {
            SceneManager.LoadScene("DenizExample");
        }
    
    }


}