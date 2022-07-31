using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] CharacterUI;


    // Update is called once per frame
    void Update()
    {
        // sets the ui of active player as active, and other inactive player ui as inactive
        for (int i = 0; i<CharacterUI.Length;i++)
        {
            if (i == ActivePlayerManager.ActivePlayerNum) { CharacterUI[i].SetActive(true);}
            else  {CharacterUI[i].SetActive(false); }
        }
    }
}
