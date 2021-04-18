using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public Transform referencepoint;
    public Vector3 directionvector;
    public GameObject crosshairPrefab;
    public Rigidbody prefabBody;
    RaycastHit hitinfo;
    public static Vector3 CrossWork;
    public RectTransform crossHairImage;
    private GameObject cloneObject;
    private Transform cloneTransform;
    private Vector3 cloneUpPosition;
    private GameObject cloneObject2;
    public static Transform cloneTransform2;
    public static Vector3 clonePosition2;
    [SerializeField] private int spawnHeight = 6;
    private float distanceOfCrosshair;
    public static float TrueDistanceOfCrosshair;
    public Transform cam;
    public Image[] image;
    public LayerMask CollidesWith = ~0;
    public static float offsetcamera;
    public Transform fromCenterPlayer;
    public static float fromCenterPLayerDistance; //only used to fix P1 bug with avalanche
    public static String targetname;
    public Color darkGreen;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CrossWork = hitinfo.point;

        FireGun();
    }

    private void FireGun()
    {

        directionvector = (referencepoint.position - firePoint.position).normalized;
        Ray ray = new Ray(firePoint.position, directionvector);
        Ray ray2 = new Ray(fromCenterPlayer.position, directionvector); //zodat je niet door enemies heen kan

        if (Physics.Raycast(ray2, out hitinfo, 2700, CollidesWith))
        {
            //this only works for P1 bug of double hit so we use it for avalanche and ability!
            fromCenterPLayerDistance = hitinfo.distance;  //this one doesnt actually work./ should use truedistance with ray instead of ray2          
        }

        // IDLE
        crossHairImage.localScale = new Vector3(0.65f, 0.65f);

        if (Physics.Raycast(ray, out hitinfo, 2700, CollidesWith))
        {
          

            targetname = hitinfo.collider.gameObject.name;
            TrueDistanceOfCrosshair = hitinfo.distance;
            offsetcamera = hitinfo.distance;

            distanceOfCrosshair = Mathf.Sqrt((.03f * hitinfo.distance));
            float hold = distanceOfCrosshair;

            distanceOfCrosshair = Mathf.Clamp(distanceOfCrosshair, 0.07f, 1.7f);
            var distanceOfCrosshairInvert = (1.9f - distanceOfCrosshair);

            distanceOfCrosshair = Mathf.Clamp(distanceOfCrosshair, 0.1f, 1f);

            Rigidbody clone = (Rigidbody)Instantiate(prefabBody, hitinfo.point, transform.rotation);
            cloneObject = clone.gameObject;
            cloneTransform = (cloneObject.transform);

            //6 determines how high spawn is.
            cloneUpPosition = spawnHeight * cloneTransform.up.normalized;

            //this is the position for upwards action
            Rigidbody clone2 = (Rigidbody)Instantiate(prefabBody, hitinfo.point + cloneUpPosition, new Quaternion(0, 0, 0, 0));
            cloneObject2 = clone2.gameObject;
            cloneTransform2 = (cloneObject2.transform);
            clonePosition2 = cloneTransform2.position;

            //EVERYTHINg IS BASED AROUND THIS
            //the raycast ball (Dynamic 2 - in main screen !!!
            cloneTransform.localScale = new Vector3(.5f * distanceOfCrosshair, .5f * distanceOfCrosshair, .5f * distanceOfCrosshair);
            cloneTransform2.localScale = new Vector3(.5f * distanceOfCrosshair, .5f * distanceOfCrosshair, .5f * distanceOfCrosshair); //5 meters above

            //delete clones       
            Destroy(clone2.gameObject);
            Destroy(clone.gameObject);

            //the image ( The crosshair image that goes bigger and smaller)
            //hold value between 0 and 1.5 -> 0 is low range, 1.5 is max range of distance crosshair;
            if (hold < 1.5f)
            {
                // Debug.Log(Gun.fromCenterPLayerDistance);
                //in range -> this needs finetunning! based on max range
                if (hitinfo.collider.tag == "Enemy")
                {
                    if (Gun.TrueDistanceOfCrosshair < 16f)
                    {
                        //  Debug.Log(hold);                  
                        image[0].color = Color.green;

                        // image[1].color = Color.green; //stip crosshair

                    }
                    else if (Gun.TrueDistanceOfCrosshair >= 16 && Gun.TrueDistanceOfCrosshair < 80)  //60)
                    {
                        image[0].color = darkGreen;
                        //image[1].color = darkGreen;
                    }
                    else
                    {
                        image[0].color = Color.white;
                    }
                    //  crossHairImage.localScale = new Vector3(1.8f * distanceOfCrosshairInvert, 2.4f * distanceOfCrosshairInvert, 2.3f * distanceOfCrosshairInvert);
                }
                else
                {
                    image[0].color = Color.white;
                }

                if (Gun.TrueDistanceOfCrosshair >= 16f)
                {
                    crossHairImage.localScale = new Vector3(Mathf.Max(1.04f * distanceOfCrosshairInvert, 0.65f), Mathf.Max(1.12f * distanceOfCrosshairInvert, 0.65f), 1.04f * distanceOfCrosshairInvert);

                    //crossHairImage.localPosition = crossHairImage.localPosition + new Vector3(0, 5, 0);
                }
                else if (Gun.TrueDistanceOfCrosshair < 16f)
                {
                    crossHairImage.localScale = new Vector3(Mathf.Max(1.7f * distanceOfCrosshairInvert, 0.65f), Mathf.Max(2.2f * distanceOfCrosshairInvert, 0.65f), 1.44f * distanceOfCrosshairInvert);
                }
                //the actual dynamic crosshair everything is based around that goes smaller and bigger
                else
                {
                    crossHairImage.localScale = new Vector3(1.3f * distanceOfCrosshairInvert, 1.4f * distanceOfCrosshairInvert, 1.3f * distanceOfCrosshairInvert);
                }
            }
            else
            {
                image[0].color = Color.white;
            }
        }

        else
        {
            image[0].color = Color.white;
        }
    }
}
