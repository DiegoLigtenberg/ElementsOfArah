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
    public GameObject off;
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
            off.SetActive(false);
            players[0].gameObject.SetActive(true);
            players[0].transform.localScale = new Vector3(1, 1, 1);
            players[0].transform.rotation = players[1].transform.rotation;
            players[0].transform.position = players[1].transform.position;






            players[1].gameObject.SetActive(false);

            canvas.worldCamera = players[0].GetComponent<Camera>();
            maincamera.transform.SetParent(players[0]);

            off.SetActive(true);


        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            off.SetActive(false);
            players[1].gameObject.SetActive(true);

            players[1].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            players[1].transform.rotation = players[0].transform.rotation;
            players[1].transform.position = players[0].transform.position;






            players[0].gameObject.SetActive(false);
            maincamera.transform.SetParent(players[1]);
            canvas.worldCamera = players[1].GetComponent<Camera>();
            off.SetActive(true);
        }
    }
}
