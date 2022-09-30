using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;


public class TextMeshProText : MonoBehaviour
{

    public DashAbility dash;
    public DashAbilityMarco dashmarco;
    public TMP_Text textDashes;






    // Start is called before the first frame update
    void Start()
    {
        textDashes = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivePlayerManager.ActivePlayerNum == 0)
        {
            textDashes.text = dash.remainingDashes.ToString();
        }
        if (ActivePlayerManager.ActivePlayerNum == 1)
        {
            textDashes.text = dashmarco.remainingDashes.ToString();
        }


    }
}
