using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class CooldownReducer : MonoBehaviour
    {

        float lastStep, timeBetweenSteps = 0.1f;
        private bool onlyonce;
        private float outofcombatmultiplier;

        private void Start()
        {
            Ability.energy = 100;
        }

        // Update is called once per frame
        void Update()
        {
            if (CooldownHandler.outOfCombat) { outofcombatmultiplier = 6.0f; }
            else { outofcombatmultiplier = 1f; }
            if (Ability.animationCooldown >= 0) { Ability.animationCooldown -= Time.deltaTime; }
            if (Ability.globalCooldown >= 0) { Ability.globalCooldown -= Time.deltaTime; }
            if (Ability.tickCooldown >= 0) { Ability.tickCooldown -= Time.deltaTime; }

            // Debug.Log(Ability.globalCooldown);
            if (Time.time - lastStep > timeBetweenSteps)
            {
                lastStep = Time.time;
                if (Ability.energy < 100)
                {
                    if (!ThirdPersonMovement.isLevitating)
                    {
                        Ability.energy += (.33f * outofcombatmultiplier);
                    }
                }
            }
        }
    }
}
