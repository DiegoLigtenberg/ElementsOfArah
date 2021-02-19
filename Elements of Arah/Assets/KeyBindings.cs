using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings" )]
public class KeyBindings : ScriptableObject 
{
    public KeyCode furioushit, beamability, avalanche, sunshine, dash;

    public KeyCode CheckKey(string key)
    {
        switch (key)
        {         
            case "furioushit":
                return furioushit;

            case "beam":
                return beamability;

            case "avalanche":
                return avalanche;

            case "sunshine":
                return sunshine;

            case "dash":
                return dash;

            default:
                return KeyCode.None;
        }
    }

}
