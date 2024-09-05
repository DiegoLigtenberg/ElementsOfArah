using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings" )]
public class KeyBindings : ScriptableObject 
{
    public KeyCode furioushit, beamability, avalanche, sunshine, dash,
                    chargeshot, rapidfire, arrowrain, friction, marcodash;

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

            case "chargeshot":
                return chargeshot;

            case "rapidfire":
                return rapidfire;

            case "arrowrain":
                return arrowrain;

            case "marcodash":
                return marcodash;   

            default:
                return KeyCode.None;
        }
    }

}
