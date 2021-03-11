using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AttachCamToActivePlayer : MonoBehaviour
{

    public Transform[] players;
    public Transform[] cameraoffset;
    public GameObject cam;
    public Canvas canvas;
    public GameObject maincamera;
    // Start is called before the first frame update
    void Start()
    {
     

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gameObject.activeSelf)
            {
                maincamera.transform.SetParent(players[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            players[0].gameObject.SetActive(true);
            players[1].gameObject.SetActive(false);

            canvas.worldCamera = players[0].GetComponent<Camera>();
            maincamera.transform.SetParent(players[0]);
     

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            players[1].gameObject.SetActive(true);
            players[0].gameObject.SetActive(false);
            maincamera.transform.SetParent(players[1]);
            canvas.worldCamera = players[1].GetComponent<Camera>();

        }
    }
}
