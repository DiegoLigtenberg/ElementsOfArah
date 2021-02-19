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
    



    // Start is called before the first frame update
    void Start()
    {
        textHpNumber = GetComponent<TMP_Text>();

        hp =  GetComponentInParent<Health>();

   
    }

    // Update is called once per frame
    void Update()
    {

        textHpNumber.text = hp.currentHealth.ToString();

    }
}
