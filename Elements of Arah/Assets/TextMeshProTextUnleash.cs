using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;

public class TextMeshProTextUnleash : MonoBehaviour
{

    public TMP_Text textUnleash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //firebreath statenumber
        if (this.gameObject.name == "AbilityStateNumber")
        {
            textUnleash.text = FrictionMarco.friction_stacks.ToString();
        }
    }
}
