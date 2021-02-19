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
    public TMP_Text textDashes;






    // Start is called before the first frame update
    void Start()
    {
        textDashes = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textDashes.text = dash.remainingDashes.ToString();

    }
}
