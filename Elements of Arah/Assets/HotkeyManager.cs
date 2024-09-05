using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyManager : MonoBehaviour
{

    public GameObject[] arahHotkeys;
    public GameObject[] marcoHotkeys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //arah active
        if (ActivePlayerManager.ActivePlayerNum == 0) { 
            //hotkeys
            foreach (GameObject hotkey in marcoHotkeys) { hotkey.SetActive(false); }
            foreach (GameObject hotkey in arahHotkeys) { hotkey.SetActive(true); }
        }

        //marco active
        if (ActivePlayerManager.ActivePlayerNum == 1)
        {
            //hotkeys
            foreach (GameObject hotkey in marcoHotkeys) { hotkey.SetActive(true); }
            foreach (GameObject hotkey in arahHotkeys) { hotkey.SetActive(false); }
        }
    }
}
