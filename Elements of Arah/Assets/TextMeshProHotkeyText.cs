using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;


public class TextMeshProHotkeyText : MonoBehaviour
{

    public TMP_Text text;

    public FuriousHit furioushit;
    public BeamAbility beamability;
    public Avalanche avalancheability;
    public DashAbility dashability;
    public SunShine sunshineability;

    // Start is called before the first frame update
    void Start()
    {
 
          text = GetComponent<TMP_Text>();


    }

    public void changeText()
    {

    }


    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.name == "Hotkey Furioushit") {

            if (furioushit.abilityKey.ToString().Contains("Alpha"))
            {
                text.text = furioushit.abilityKey.ToString().ToLower().Substring(5);            
            }
            
            else if (furioushit.abilityKey.ToString().Contains("Left"))
            {
                if (furioushit.abilityKey.ToString().Contains("Control"))
                {
                    text.text = "ctrl";
                }
                if (furioushit.abilityKey.ToString().Contains("Shift"))
                {
                    text.text = "shift";
                }
                if (furioushit.abilityKey.ToString().Contains("Alt"))
                {
                    text.text = "alt";
                }              
            }
            else if (furioushit.abilityKey.ToString().Contains("Mouse"))
            {
                text.text = furioushit.abilityKey.ToString().Substring(0, 1) + furioushit.abilityKey.ToString().Substring(5);                
        
            }
            else
            {
                text.text = furioushit.abilityKey.ToString().ToLower();
            }            
           
        }


        if (this.gameObject.name == "Hotkey Beam") {
            if (beamability.abilityKey.ToString().Contains("Alpha"))
            {
                text.text = beamability.abilityKey.ToString().ToLower().Substring(5);
            }

            else if (beamability.abilityKey.ToString().Contains("Left"))
            {
                if (beamability.abilityKey.ToString().Contains("Control"))
                {
                    text.text = "ctrl";
                }
                if (beamability.abilityKey.ToString().Contains("Shift"))
                {
                    text.text = "shift";
                }
                if (beamability.abilityKey.ToString().Contains("Alt"))
                {
                    text.text = "alt";
                }
            }
            else if (beamability.abilityKey.ToString().Contains("Mouse"))
            {
                text.text = beamability.abilityKey.ToString().Substring(0, 1) + beamability.abilityKey.ToString().Substring(5);

            }
            else
            {
                text.text = beamability.abilityKey.ToString().ToLower();
            }
        }

        if (this.gameObject.name == "Hotkey Avalanche")
        {
            if (avalancheability.abilityKey.ToString().Contains("Alpha"))
            {
                text.text = avalancheability.abilityKey.ToString().ToLower().Substring(5);
            }

            else if (avalancheability.abilityKey.ToString().Contains("Left"))
            {
                if (avalancheability.abilityKey.ToString().Contains("Control"))
                {
                    text.text = "ctrl";
                }
                if (avalancheability.abilityKey.ToString().Contains("Shift"))
                {
                    text.text = "shift";
                }
                if (avalancheability.abilityKey.ToString().Contains("Alt"))
                {
                    text.text = "alt";
                }
            }
            else if (avalancheability.abilityKey.ToString().Contains("Mouse"))
            {
                text.text = avalancheability.abilityKey.ToString().Substring(0, 1) + avalancheability.abilityKey.ToString().Substring(5);

            }
            else
            {
                text.text = avalancheability.abilityKey.ToString().ToLower();
            }
        }
        if (this.gameObject.name == "Hotkey Sunshine") {
            if (avalancheability.abilityKey.ToString().Contains("Alpha"))
            {
                text.text = sunshineability.abilityKey.ToString().ToLower().Substring(5);
            }

            else if (sunshineability.abilityKey.ToString().Contains("Left"))
            {
                if (sunshineability.abilityKey.ToString().Contains("Control"))
                {
                    text.text = "ctrl";
                }
                if (sunshineability.abilityKey.ToString().Contains("Shift"))
                {
                    text.text = "shift";
                }
                if (sunshineability.abilityKey.ToString().Contains("Alt"))
                {
                    text.text = "alt";
                }
            }
            else if (sunshineability.abilityKey.ToString().Contains("Mouse"))
            {
                text.text = sunshineability.abilityKey.ToString().Substring(0, 1) + sunshineability.abilityKey.ToString().Substring(5);

            }
            else
            {
                text.text = sunshineability.abilityKey.ToString().ToLower();
            }
        }

        if (this.gameObject.name == "Hotkey Dash") {
            if (dashability.abilityKey.ToString().Contains("Alpha"))
            {
                text.text = dashability.abilityKey.ToString().ToLower().Substring(5);
            }

            else if (dashability.abilityKey.ToString().Contains("Left"))
            {
                if (dashability.abilityKey.ToString().Contains("Control"))
                {
                    text.text = "ctrl";
                }
                if (dashability.abilityKey.ToString().Contains("Shift"))
                {
                    text.text = "shift";
                }
                if (dashability.abilityKey.ToString().Contains("Alt"))
                {
                    text.text = "alt";
                }
            }
            else if (dashability.abilityKey.ToString().Contains("Mouse"))
            {
                text.text = dashability.abilityKey.ToString().Substring(0, 1) + dashability.abilityKey.ToString().Substring(5);

            }
            else
            {
                text.text = dashability.abilityKey.ToString().ToLower();
            }

        }
       // if (this.gameObject.name == "Hotkey 1 Shield") { text.text = furioushit.getAbilitykey.ToString().ToLower(); }
        //if (this.gameObject.name == "Hotkey 1 Healing") { text.text = furioushit.getAbilitykey.ToString().ToLower(); }
        

    }
}
