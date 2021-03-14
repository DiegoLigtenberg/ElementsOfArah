using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;
public class TextMeshProPlayerStats : MonoBehaviour
{

    public TMP_Text text;
    private HealthPlayer healthplayer;


    void Start()
    {
        healthplayer = GameObject.Find("hp check").GetComponent<HealthPlayer>();
        text = GetComponent<TMP_Text>();
    }


    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Currenthpnumber")
        {
            text.text = healthplayer.currentHealth.ToString();
        }
        if (this.gameObject.name == "Currentmananumber")
        {
            text.text = ((int)Ability.energy).ToString();
        }
    }
}
