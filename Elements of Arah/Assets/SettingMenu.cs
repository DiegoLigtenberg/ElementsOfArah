using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    List<KeyCode> DropDownKeys = new List<KeyCode>();
    List<KeyCode> DropDownKeys1 = new List<KeyCode>();
    List<KeyCode> DropDownKeys2 = new List<KeyCode>();
    List<KeyCode> DropDownKeys3 = new List<KeyCode>();
    List<KeyCode> DropDownKeys4 = new List<KeyCode>();
    //  KeyCode[] DropDownKeys;
    public TMP_Dropdown keybindingsDropdown;
    public TMP_Dropdown keybindingsDropdown1;
    public TMP_Dropdown keybindingsDropdown2;
    public TMP_Dropdown keybindingsDropdown3;
    public TMP_Dropdown keybindingsDropdown4;

    public KeyBindings keyBindings;

    public KeyCode dropDownKeyCode;
    public KeyCode dropDownKeyCode1;
    public KeyCode dropDownKeyCode2;
    public KeyCode dropDownKeyCode3;
    public KeyCode dropDownKeyCode4;
    // public KeyBindings keybind;
    private int i = 0;
    private int j = 0;

    public PauseMenu pauseMenu;

    public static bool first;
    public KeyCode[] test;
    private int currentKeyCodeIndex = 0;
   private string option;
    void Start()
    {
        j = 0;
        if (!first)
        {
            SceneManager.LoadScene(0);
            first = true;
        }


        List<string> allkeys = new List<string>();

        for (int k = 0; k < Enum.GetValues(typeof(KeyCode)).Length; k++)
        // foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {

            j++;
            //Debug.Log(kcode + " \t " + Enum.GetValues(typeof(KeyCode))[]);
            test = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            KeyCode kcode = test[k];
         //  Debug.Log(kcode);

            //getallen                     //alfabet               //fkeys                       /shift     //mouse keys
            if ((k > 22 && k < 33) || (k > 45 && k < 72) || (k > 102 && k < 115) || (k > 120 && k < 127) && k%2== 0 || (k > 139 && k < 146))
            {
                // Debug.Log(kcode + " \t " + j);
                DropDownKeys.Add(kcode);
                DropDownKeys1.Add(kcode);
                DropDownKeys2.Add(kcode);
                DropDownKeys3.Add(kcode);
                DropDownKeys4.Add(kcode);

                // Debug.Log(i);

                if ( kcode.ToString().Contains("Alpha"))
                {
                    option = kcode.ToString();
                    option = option.Substring(5);
                }
              else  if (kcode.ToString().Contains("Left"))
                {
                    option = kcode.ToString();
                    option = option.Substring(4);
                }
               else if (kcode.ToString().Contains("Mouse"))
                {
                    option = kcode.ToString();
                    option = option.Substring(0, 1) + option.Substring(5);
                }
                else
                {
                    option = kcode.ToString();
                }
               
        


                allkeys.Add(option);
                currentKeyCodeIndex = i;
                i++;
            }

        }



        keybindingsDropdown.AddOptions(allkeys);
        keybindingsDropdown.value = PlayerPrefs.GetInt("HotkeyFurioushit", 0);
        keybindingsDropdown.RefreshShownValue();
        PlayerPrefs.SetInt("HotkeyFurioushit", keybindingsDropdown.value);
        keyFurioushit = DropDownKeys[PlayerPrefs.GetInt("HotkeyFurioushit", 0)];


        keybindingsDropdown1.AddOptions(allkeys);
        keybindingsDropdown1.value = PlayerPrefs.GetInt("HotkeyBeamability", 0);
        keybindingsDropdown1.RefreshShownValue();
        PlayerPrefs.SetInt("HotkeyBeamability", keybindingsDropdown1.value);
        keyBeamability = DropDownKeys1[PlayerPrefs.GetInt("HotkeyBeamability", 0)];


        keybindingsDropdown2.AddOptions(allkeys);
        keybindingsDropdown2.value = PlayerPrefs.GetInt("HotkeyAvalancheability", 0);
        keybindingsDropdown2.RefreshShownValue();
        PlayerPrefs.SetInt("Hotkey", keybindingsDropdown2.value);
        keyAvalancheability = DropDownKeys2[PlayerPrefs.GetInt("HotkeyAvalancheability", 0)];


        keybindingsDropdown3.AddOptions(allkeys);
        keybindingsDropdown3.value = PlayerPrefs.GetInt("HotkeySunshineability", 0);
        keybindingsDropdown3.RefreshShownValue();
        PlayerPrefs.SetInt("Hotkey", keybindingsDropdown3.value);
        keySunshineability = DropDownKeys3[PlayerPrefs.GetInt("HotkeySunshineability", 0)];

        keybindingsDropdown4.AddOptions(allkeys);
        keybindingsDropdown4.value = PlayerPrefs.GetInt("HotkeyDashability", 0);
        keybindingsDropdown4.RefreshShownValue();
        PlayerPrefs.SetInt("Hotkey", keybindingsDropdown4.value);
        keyDashAbility = DropDownKeys4[PlayerPrefs.GetInt("HotkeyDashability", 0)];



    }




    public static KeyCode keyFurioushit;
    public static bool ChangedFurioushit;
    public void setKeybindFurioushit()
    {
      
            if (PlayerPrefs.GetInt("OpenedKeybind") == 1)
        {
            Debug.Log(PlayerPrefs.GetInt("OpenedKeybind"));
            PlayerPrefs.SetInt("ClickedKeyBindFurioushit", 1);
        }
     
        ChangedFurioushit = true;
        PlayerPrefs.SetInt("HotkeyFurioushit", keybindingsDropdown.value);
        keyFurioushit = DropDownKeys[PlayerPrefs.GetInt("HotkeyFurioushit", 0)];
       // Debug.Log(keybindingsDropdown.value);
        //Debug.Log(keyFurioushit);

    }

    public static KeyCode keyBeamability;
    public static bool ChangedBeamability;
    public void setKeybindBeamability()
    {
        if (PlayerPrefs.GetInt("OpenedKeybind") == 1)
        {
            PlayerPrefs.SetInt("ClickedKeyBindBeamability", 1);
        }
        ChangedBeamability = true;
        PlayerPrefs.SetInt("HotkeyBeamability", keybindingsDropdown1.value);
        keyBeamability = DropDownKeys1[PlayerPrefs.GetInt("HotkeyBeamability", 0)];
    }

    public static KeyCode keyAvalancheability;
    public static bool ChangedAvalancheability;
    public void setKeybindAvalancheability()
    {
        if (PlayerPrefs.GetInt("OpenedKeybind") == 1)
        {
            PlayerPrefs.SetInt("ClickedKeyAvalancheability", 1);
        }
        ChangedAvalancheability = true;
        PlayerPrefs.SetInt("HotkeyAvalancheability", keybindingsDropdown2.value);
        keyAvalancheability = DropDownKeys[PlayerPrefs.GetInt("HotkeyAvalancheability", 0)];
    }

    public static KeyCode keySunshineability;
    public static bool ChangedSunshineability;
    public void setKeybindSunshineability()
    {
        if (PlayerPrefs.GetInt("OpenedKeybind") == 1)
        {
            PlayerPrefs.SetInt("ClickedKeySunshineability", 1);
        }
        ChangedSunshineability = true;
        PlayerPrefs.SetInt("HotkeySunshineability", keybindingsDropdown3.value);
        keySunshineability = DropDownKeys[PlayerPrefs.GetInt("HotkeySunshineability", 0)];
    }

    public static KeyCode keyDashAbility;
    public static bool ChangedDashability;
    public void setKeybindDashability()
    {
        if (PlayerPrefs.GetInt("OpenedKeybind") == 1)
        {
            PlayerPrefs.SetInt("ClickedKeyDashability", 1);
        }
        ChangedDashability = true;
        PlayerPrefs.SetInt("HotkeyDashability", keybindingsDropdown4.value);
        keyDashAbility = DropDownKeys[PlayerPrefs.GetInt("HotkeyDashability", 0)];
    }

    public void ResetScene()
    {
        pauseMenu.Resume();
        SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    void Update()
    {

    }


    /*
    public KeyCode OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            return e.keyCode;
            // Debug.Log("Detected key code: " + e.keyCode);
        }
        else
        {
            return KeyCode.None;
        }
    }
    */
}
