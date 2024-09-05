using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackgroundController : MonoBehaviour
{
    public GameObject[] arahUIbackground;
    public GameObject[] marcoUIbackground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //arah active
        if (ActivePlayerManager.ActivePlayerNum == 0)
        {
            //UI
            foreach (GameObject img in marcoUIbackground) { img.SetActive(false); }
            foreach (GameObject img in arahUIbackground) { img.SetActive(true); }
        }

        //marco active
        if (ActivePlayerManager.ActivePlayerNum == 1)
        {
            //UI
            foreach (GameObject img in marcoUIbackground) { img.SetActive(true); }
            foreach (GameObject img in arahUIbackground) { img.SetActive(false); }
        }
    }
}
