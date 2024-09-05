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

    public ChargeShotMarco chargeshotability;
    public RapidFireMarco rapidfireability;
    public ArrowRainMarco arrowrainability;
    public FrictionMarco frictionability;
    public DashAbilityMarco marcodashability;

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
        // arah
        if (ActivePlayerManager.ActivePlayerNum == 0)
        {
            if (this.gameObject.name == "Hotkey Furioushit")
            {

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


            if (this.gameObject.name == "Hotkey Beam")
            {
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
            if (this.gameObject.name == "Hotkey Sunshine")
            {
                if (sunshineability.abilityKey.ToString().Contains("Alpha"))
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

            if (this.gameObject.name == "Hotkey Dash")
            {
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

        // marco
        if (ActivePlayerManager.ActivePlayerNum == 1)
        {
            if (this.gameObject.name == "Hotkey Chargeshot")
            {

                if (chargeshotability.abilityKey.ToString().Contains("Alpha"))
                {
                    text.text = chargeshotability.abilityKey.ToString().ToLower().Substring(5);
                }

                else if (chargeshotability.abilityKey.ToString().Contains("Left"))
                {
                    if (chargeshotability.abilityKey.ToString().Contains("Control"))
                    {
                        text.text = "ctrl";
                    }
                    if (chargeshotability.abilityKey.ToString().Contains("Shift"))
                    {
                        text.text = "shift";
                    }
                    if (chargeshotability.abilityKey.ToString().Contains("Alt"))
                    {
                        text.text = "alt";
                    }
                }
                else if (chargeshotability.abilityKey.ToString().Contains("Mouse"))
                {
                    text.text = chargeshotability.abilityKey.ToString().Substring(0, 1) + chargeshotability.abilityKey.ToString().Substring(5);

                }
                else
                {
                    text.text = chargeshotability.abilityKey.ToString().ToLower();
                }

            }


            if (this.gameObject.name == "Hotkey Rapidfire")
            {
                if (rapidfireability.abilityKey.ToString().Contains("Alpha"))
                {
                    text.text = rapidfireability.abilityKey.ToString().ToLower().Substring(5);
                }

                else if (rapidfireability.abilityKey.ToString().Contains("Left"))
                {
                    if (rapidfireability.abilityKey.ToString().Contains("Control"))
                    {
                        text.text = "ctrl";
                    }
                    if (rapidfireability.abilityKey.ToString().Contains("Shift"))
                    {
                        text.text = "shift";
                    }
                    if (rapidfireability.abilityKey.ToString().Contains("Alt"))
                    {
                        text.text = "alt";
                    }
                }
                else if (rapidfireability.abilityKey.ToString().Contains("Mouse"))
                {
                    text.text = rapidfireability.abilityKey.ToString().Substring(0, 1) + rapidfireability.abilityKey.ToString().Substring(5);

                }
                else
                {
                    text.text = rapidfireability.abilityKey.ToString().ToLower();
                }
            }

            if (this.gameObject.name == "Hotkey Arrowrain")
            {
                if (arrowrainability.abilityKey.ToString().Contains("Alpha"))
                {
                    text.text = arrowrainability.abilityKey.ToString().ToLower().Substring(5);
                }

                else if (arrowrainability.abilityKey.ToString().Contains("Left"))
                {
                    if (arrowrainability.abilityKey.ToString().Contains("Control"))
                    {
                        text.text = "ctrl";
                    }
                    if (arrowrainability.abilityKey.ToString().Contains("Shift"))
                    {
                        text.text = "shift";
                    }
                    if (arrowrainability.abilityKey.ToString().Contains("Alt"))
                    {
                        text.text = "alt";
                    }
                }
                else if (arrowrainability.abilityKey.ToString().Contains("Mouse"))
                {
                    text.text = arrowrainability.abilityKey.ToString().Substring(0, 1) + arrowrainability.abilityKey.ToString().Substring(5);

                }
                else
                {
                    text.text = arrowrainability.abilityKey.ToString().ToLower();
                }
            }
            if (this.gameObject.name == "Hotkey Friction")
            {
                if (frictionability.abilityKey.ToString().Contains("Alpha"))
                {
                    text.text = frictionability.abilityKey.ToString().ToLower().Substring(5);
                }

                else if (frictionability.abilityKey.ToString().Contains("Left"))
                {
                    if (frictionability.abilityKey.ToString().Contains("Control"))
                    {
                        text.text = "ctrl";
                    }
                    if (frictionability.abilityKey.ToString().Contains("Shift"))
                    {
                        text.text = "shift";
                    }
                    if (frictionability.abilityKey.ToString().Contains("Alt"))
                    {
                        text.text = "alt";
                    }
                }
                else if (frictionability.abilityKey.ToString().Contains("Mouse"))
                {
                    text.text = frictionability.abilityKey.ToString().Substring(0, 1) + frictionability.abilityKey.ToString().Substring(5);

                }
                else
                {
                    text.text = frictionability.abilityKey.ToString().ToLower();
                }
            }

            if (this.gameObject.name == "Hotkey Marcodash")
            {
                if (marcodashability.abilityKey.ToString().Contains("Alpha"))
                {
                    text.text = marcodashability.abilityKey.ToString().ToLower().Substring(5);
                }

                else if (marcodashability.abilityKey.ToString().Contains("Left"))
                {
                    if (marcodashability.abilityKey.ToString().Contains("Control"))
                    {
                        text.text = "ctrl";
                    }
                    if (marcodashability.abilityKey.ToString().Contains("Shift"))
                    {
                        text.text = "shift";
                    }
                    if (marcodashability.abilityKey.ToString().Contains("Alt"))
                    {
                        text.text = "alt";
                    }
                }
                else if (marcodashability.abilityKey.ToString().Contains("Mouse"))
                {
                    text.text = marcodashability.abilityKey.ToString().Substring(0, 1) + marcodashability.abilityKey.ToString().Substring(5);

                }
                else
                {
                    text.text = marcodashability.abilityKey.ToString().ToLower();
                }

            }
            // if (this.gameObject.name == "Hotkey 1 Shield") { text.text = furioushit.getAbilitykey.ToString().ToLower(); }
            //if (this.gameObject.name == "Hotkey 1 Healing") { text.text = furioushit.getAbilitykey.ToString().ToLower(); }
        }

    }

}

