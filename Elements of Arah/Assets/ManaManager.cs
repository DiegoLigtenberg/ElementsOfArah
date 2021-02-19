using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

namespace CreatingCharacters.Abilities
{
    public class ManaManager : MonoBehaviour
    {

        [SerializeField] ManaBar manabar;
        public float mana;

        [SerializeField] private float startingMana = 100;

        [HideInInspector] public float currentMana;
        [HideInInspector] public float currentManaPCT = 1;



        // Start is called before the first frame update
        private void OnEnable()
        {
            currentManaPCT = 1;
            manabar.SetMaxMana(currentManaPCT);
            startingMana = 100;

        }
     

        // Update is called once per frame
        void Update()
        {
            currentMana = Ability.energy;
         //   Debug.Log(currentMana);
            currentManaPCT = (float)currentMana / (float)startingMana;
          

            manabar.SetMana(currentManaPCT);
        }
    }
}