using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatingCharacters.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        public Ability[] Abilities;
        public string[] Abilitynames;
        // Start is called before the first frame update
        void Start()
        {
            Abilities = new Ability[GetComponents<Ability>().Length];
            Abilitynames= new string[GetComponents<Ability>().Length];
            for (int i = 0; i < ActivePlayerManager.ActivePlayerGameObj.GetComponents<Ability>().Length; i++)
            {
                Abilities[i] = ActivePlayerManager.ActivePlayerGameObj.GetComponents<Ability>()[i];
                Abilitynames[i] = ActivePlayerManager.ActivePlayerGameObj.GetComponents<Ability>()[i].AbilityName;
            }
        }

        // Update is called once per frame
        void Update()
        {
          
          
        }
    }

}