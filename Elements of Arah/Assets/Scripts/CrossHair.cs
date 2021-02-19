using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public LayerMask mask;
    public GameObject crosshairPrefab;
    public CrossHair projectile;

    public bool ownerAiming { get; set; }

    float lastStep, timeBetweenSteps = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (crosshairPrefab != null)
        {
            crosshairPrefab = Instantiate(crosshairPrefab);
            ToggleCrosshair(false);
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hitInfo;

            Ray[] rays = new Ray[1];

            rays[0] = new Ray(transform.position, transform.forward);


       

            if (Physics.Raycast(rays[0], out hitInfo, 50, mask))
            {
                Debug.Log("hit");
                Instantiate(crosshairPrefab, hitInfo.point,transform.rotation);
            }

            

                if (Input.GetKey("c"))
            {

                

                if (Time.time - lastStep > timeBetweenSteps)
                {
                    lastStep = Time.time;
                    crosshairPrefab = Instantiate(crosshairPrefab);
                }
            }
        }
    }

    void PositionCrosshair (Ray ray)
    {
      //  RaycastHit hit;
        
    }



    //toggle on and off croshair
    void ToggleCrosshair(bool enabled)
    {
        if (crosshairPrefab != null)
        {
            crosshairPrefab.SetActive(enabled);
        }
    }

}
