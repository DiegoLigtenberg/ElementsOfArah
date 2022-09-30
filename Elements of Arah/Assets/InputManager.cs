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

        }


        {
            if (PlayerPrefs.GetInt("ClickedKeyBindFurioushit") == 0)
            {
            
                    keyBindings.furioushit = KeyCode.Alpha1;
                
            }
            else
            {
                if (SettingMenu.ChangedFurioushit)
                {

                    keyBindings.furioushit = SettingMenu.keyFurioushit;
                    SettingMenu.ChangedFurioushit = false;
                }
            }


            if (PlayerPrefs.GetInt("ClickedKeyBindBeamability") == 0)
            {                
             
                    keyBindings.beamability = KeyCode.Alpha2;

                
            }
            else
            {
                if (SettingMenu.ChangedBeamability)
                {
                    keyBindings.beamability = SettingMenu.keyBeamability;
                    SettingMenu.ChangedBeamability = false;
                }
            }

            if (PlayerPrefs.GetInt("ClickedKeyAvalancheability") == 0)
            {
             
                    keyBindings.avalanche= KeyCode.Alpha3;

                
            }
            else
            {
                if (SettingMenu.ChangedAvalancheability)
                {
                    keyBindings.avalanche = SettingMenu.keyAvalancheability;
                    SettingMenu.ChangedAvalancheability = false;
                }
            }

            if (PlayerPrefs.GetInt("ClickedKeySunshineability") == 0)
            {
                
                    keyBindings.sunshine = KeyCode.Alpha4;

                
            }
            else
            {
                if (SettingMenu.ChangedSunshineability)
                {
                    keyBindings.sunshine = SettingMenu.keySunshineability;
                    SettingMenu.ChangedSunshineability = false;
                }
            }


            if (PlayerPrefs.GetInt("ClickedKeyDashability") == 0)
            {
               
                    keyBindings.dash = KeyCode.LeftControl;

                
            }
            else
            {
                if (SettingMenu.ChangedDashability)
                {
                    keyBindings.dash = SettingMenu.keyDashAbility;
                    SettingMenu.ChangedDashability = false;
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

    



            default:        
                return KeyCode.None;
        }
    }
}


