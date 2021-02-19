using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;


public class TextMeshProStopWatchMinute : MonoBehaviour
{

    public TMP_Text textFirebreath;

    public static int minute = 0;
    // Start is called before the first frame update
    void Start()
    {

        float minute = 0;
        textFirebreath = GetComponent<TMP_Text>();


    }

    // Update is called once per frame
    void Update()
    {

       
        textFirebreath.text = minute.ToString();

    }
}
