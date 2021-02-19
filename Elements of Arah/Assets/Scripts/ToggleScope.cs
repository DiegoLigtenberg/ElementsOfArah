using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ToggleScope : MonoBehaviour
{

    public MeshRenderer[] mr;
    public CinemachineFreeLook cv;
    [SerializeField] Transform target;
    [SerializeField] Transform target2;


    private bool switcher = true;
    // Start is called before the first frame update
    void Start()
    {
        switcher = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switcher = !switcher;


            if (switcher)
            {
                for (int i = 0; i < mr.Length; i++)
                {

                    mr[0].enabled = true;
                    mr[1].enabled = true;
                    mr[2].enabled = false;
                    mr[3].enabled = false;

                    cv.m_LookAt = target.transform;

                }
            }

            else
            {
                switcher = false;
                Debug.Log(switcher);

                for (int i = 0; i < mr.Length; i++)
                {
                    mr[0].enabled = false;
                    mr[1].enabled = false;
                    mr[2].enabled = true;
                    mr[3].enabled = true;

             
                    cv.m_LookAt = target2.transform;
                }

            }
           

        }
         */

    }
}
