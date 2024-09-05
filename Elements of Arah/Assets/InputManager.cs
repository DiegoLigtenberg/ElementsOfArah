using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    public KeyBindings keyBindings;
    public SettingMenu settingmenu;
    public static int amountoftimes;

  
    private void Awake()
    {
        //  Debug.Log(amountoftimes);
        //dus niet 1e keer
        //if (amountoftimes > 0)

        //  /////////////////////////////////////////////////////////////////////////////////
        //  /////////////////////////////////////////////////////////////////////////////////
        //PUT THIS ON BEFORE BUILDING
        //  /////////////////////////////////////////////////////////////////////////////////
        //  /////////////////////////////////////////////////////////////////////////////////
        Debug.Log(PlayerPrefs.GetInt("ClickedKeyBindFurioushit"));
        if (PlayerPrefs.GetInt("OpenedKeybind") == 0)
        {
          
            PlayerPrefs.SetInt("ClickedKeyBindFurioushit",0);
            PlayerPrefs.SetInt("ClickedKeyBindBeamability", 0);
            PlayerPrefs.SetInt("ClickedKeyAvalancheability", 0);
            PlayerPrefs.SetInt("ClickedKeySunshineability", 0);
            PlayerPrefs.SetInt("ClickedKeyDashability", 0);

            PlayerPrefs.SetInt("ClickedKeyBindChargeshotability", 0);
            PlayerPrefs.SetInt("ClickedKeyBindRapidfireability", 0);
            PlayerPrefs.SetInt("ClickedKeyBindArrowrainability", 0);
            PlayerPrefs.SetInt("ClickedKeyBindFrictionability1", 0);
            PlayerPrefs.SetInt("ClickedKeyBindDashmarcoability", 0);

        }


        {
            if (PlayerPrefs.GetInt("ClickedKeyBindFurioushit") == 0)
            {
            
                    keyBindings.furioushit = KeyCode.Alpha1;
                    keyBindings.chargeshot = KeyCode.Alpha1;
                
            }
            else
            {
                if (SettingMenu.ChangedFurioushit)
                {

                    keyBindings.furioushit = SettingMenu.keyFurioushit;
                    keyBindings.chargeshot = SettingMenu.keyChargeshot;
                    SettingMenu.ChangedFurioushit = false;
                    SettingMenu.ChangedChargeshot = false;
                }
            }


            if (PlayerPrefs.GetInt("ClickedKeyBindBeamability") == 0)
            {                
             
                    keyBindings.beamability = KeyCode.Alpha2;
                    keyBindings.rapidfire = KeyCode.Alpha2;

                
            }
            else
            {
                if (SettingMenu.ChangedBeamability)
                {
                    keyBindings.beamability = SettingMenu.keyBeamability;
                    keyBindings.rapidfire = SettingMenu.keyRapidfireability;
                    SettingMenu.ChangedBeamability = false;
                    SettingMenu.ChangedRapidfireability = false;
                }
            }

            if (PlayerPrefs.GetInt("ClickedKeyAvalancheability") == 0)
            {
             
                    keyBindings.avalanche= KeyCode.Alpha3;
                    keyBindings.arrowrain = KeyCode.Alpha3; 

                
            }
            else
            {
                if (SettingMenu.ChangedAvalancheability)
                {
                    keyBindings.avalanche = SettingMenu.keyAvalancheability;
                    keyBindings.arrowrain = SettingMenu.keyArrowrainability;
                    SettingMenu.ChangedAvalancheability = false;
                    SettingMenu.ChangedArrowrainability = false;
                }
            }

            if (PlayerPrefs.GetInt("ClickedKeySunshineability") == 0)
            {
                
                    keyBindings.sunshine = KeyCode.Alpha4;
                    keyBindings.friction = KeyCode.Alpha4;

                
            }
            else
            {
                if (SettingMenu.ChangedSunshineability)
                {
                    keyBindings.sunshine = SettingMenu.keySunshineability;
                    keyBindings.friction = SettingMenu.keyFrictionability;
                    SettingMenu.ChangedSunshineability = false;
                    SettingMenu.ChangedFrictionability = false;
                }
            }


            if (PlayerPrefs.GetInt("ClickedKeyDashability") == 0)
            {
               
                    keyBindings.dash = KeyCode.LeftControl;
                    keyBindings.marcodash = KeyCode.LeftControl;

                
            }
            else
            {
                if (SettingMenu.ChangedDashability)
                {
                    keyBindings.dash = SettingMenu.keyDashAbility;
                    keyBindings.marcodash = SettingMenu.keyMarcoDashAbility;
                    SettingMenu.ChangedDashability = false;
                    SettingMenu.ChangedMarcoDashability = false;
                }
            }
            // Debug.Log(keyBindings.furioushit);
        }
        amountoftimes++;


        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus)) { Time.timeScale += 0.1f; }
        if (Input.GetKeyDown(KeyCode.Minus)) { Time.timeScale -= 0.1f; }
        if (Input.GetKeyDown(KeyCode.P)) { Time.timeScale = 1; }
    }

    public bool KeyDown(string key)
    {
        if (Input.GetKeyDown(keyBindings.CheckKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public KeyCode getKeyCode(string key)
    {
        switch (key)
        {
            // arah
            case "dash":
                return keyBindings.dash;
               // return keyBindings.CheckKey("dash");

            case "furioushit":
                return keyBindings.furioushit;

            case "beam":
                return keyBindings.beamability;

            case "avalanche":
                return keyBindings.avalanche;

            case "sunshine":
                return keyBindings.sunshine;

            // marco
            case "chargeshot":
                return keyBindings.chargeshot;

            case "rapidfire":
                return keyBindings.rapidfire;

            case "arrowrain":
                return keyBindings.arrowrain;

            case "friction":
                return keyBindings.friction;

            case "marcodash":
                return keyBindings.marcodash;



            default:        
                return KeyCode.None;
        }
    }
}


