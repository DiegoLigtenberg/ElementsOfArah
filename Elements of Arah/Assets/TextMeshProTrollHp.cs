using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;


public class TextMeshProTrollHp : MonoBehaviour
{
    public Health hp;
    public HealthWendigo hpw;
    public TMP_Text textHpNumber;

    void Start()
    {
        textHpNumber = GetComponent<TMP_Text>();
        hp = GetComponentInParent<Health>();
        hpw = GetComponentInParent<HealthWendigo>();
    }

    void Update()
    {
        // Show health for whichever component exists
        if (hp != null)
        {
            textHpNumber.text = hp.currentHealth.ToString();
        }
        else if (hpw != null)
        {
            textHpNumber.text = hpw.currentHealth.ToString();
        }
        else
        {
            textHpNumber.text = "0";
        }
    }
}