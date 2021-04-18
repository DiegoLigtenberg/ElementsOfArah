using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreatingCharacters.Abilities
{
    public class ArrowRainMarco : Ability
    {
        public Animator anim;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private Vector3 oldCamTransform;
        private Quaternion oldCamRotation;
        public Image abilityImage;   //the hidden image in canvas
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private Vector3 arrowRainSpawnPosition;

        private void OnEnable()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            CooldownData();

            getdmg = AbilityDamage;
            arrowRainSpawnPosition = Gun.clonePosition2;
        }

        private void CooldownData()
        {
            /*
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && latecast || latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            if (abilityCooldownLeft != 0)
            {
                abilityImage.fillAmount -= 1 / AbilityCooldown * Time.deltaTime;
                if (abilityImage.fillAmount <= 0.05f)
                {
                    abilityImage.fillAmount = 0;
                }
            }
            */
        }

        public void stopArrowRain()
        {
            anim.SetBool("arrowRainActive", false);
            anim.ResetTrigger("basicAttack");
            anim.ResetTrigger("basicAttackx2");
        }

        public override void Cast()
        {
            latecast = true;
    

            StartCoroutine(arrowRain());

           

            // Instantiate(effect[1], effectTransform[0].position, effectTransform[1].transform.rotation);
        }


        public IEnumerator arrowRain()
        {
            Vector3 spawnpos = arrowRainSpawnPosition;
            anim.SetTrigger("arrowRain");
            anim.SetBool("arrowRainActive", true);

            yield return new WaitForSeconds(1.2f);

            if (Ability.animationCooldown <= 1.2f)
            {
                Ability.animationCooldown = 1.2f;
            }
            if (Ability.globalCooldown <= 0.7f)
            {
                Ability.globalCooldown = 0.7f;
            }
            yield return new WaitForSeconds(0.001f);
       
          
            Instantiate(effect[0], spawnpos, Quaternion.identity);

     
            yield return new WaitForSeconds(1);



        }
    }
}