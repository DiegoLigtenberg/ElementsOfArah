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

        public float heightarrows;
        private void OnEnable()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            CooldownData();

            getdmg = AbilityDamage;
            arrowRainSpawnPosition =  this.transform.position + new Vector3(0, heightarrows, 0); //= Gun.clonePosition2 + new Vector3(0,heightarrows,0);
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

            yield return new WaitForSeconds(0.25f);

            if (Ability.animationCooldown <= 0.8f)
            {
                Ability.animationCooldown = 0.8f;
            }
            if (Ability.globalCooldown <= 0.8f)
            {
                Ability.globalCooldown = 0.8f;
            }
            yield return new WaitForSeconds(0.35f);
            yield return new WaitForSeconds(0.001f);

            Vector3 middle = new Vector3(0, 0, 0);
            Vector3 left = new Vector3(-1, 0, 0);
            Vector3 right = new Vector3(1, 0, 0);
            Vector3 down = new Vector3(0, 0, -1);
            Vector3 up = new Vector3(0, 0, 1);


            GameObject b = Instantiate(effect[0], spawnpos + middle + new Vector3(0,30,0), Quaternion.identity);
           // Transform target = GameObject.Find("Warrior Idle/CaveTroll_Pants_low_Mesh.002/Cube").transform;
           // b.transform.LookAt(target);


            //   Instantiate(effect[0], spawnpos+middle,effectTransform[0].rotation);  //middle     //quaternion.identiy
            /*
            yield return new WaitForSeconds(0.2f);
            Instantiate(effect[0], spawnpos+left, effectTransform[1].rotation); //left
            yield return new WaitForSeconds(0.2f);
            Instantiate(effect[0], spawnpos+right, effectTransform[2].rotation); //right
            yield return new WaitForSeconds(0.2f);
            Instantiate(effect[0], spawnpos+down, effectTransform[3].rotation); //down 
            yield return new WaitForSeconds(0.2f);
            Instantiate(effect[0], spawnpos+up, effectTransform[4].rotation); //up

    */

            yield return new WaitForSeconds(1);



        }
    }
}