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
    public TMP_Text textHpNumber;

    void Start()
    {
        textHpNumber = GetComponent<TMP_Text>();
        hp = GetComponentInParent<Health>();
    }

    void Update()
    {
        // Show health for whichever component exists
        textHpNumber.text = hp.currentHealth.ToString();
        
    }
}