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
        public Image abilityImage;   //the hidden image in canvas
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private Vector3 arrowRainSpawnPosition;

        public int ability_range;

        public float heightarrows;
        public static bool stop_direction;

        public GameObject textobjectcd;
        public GameObject nomana;
        private bool manaactive;
        public GameObject outrange;
        public AudioSource[] aus;
        //[HideInInspector] public float textcdleft;
        public GameObject faraway_cross_aim;

        private void OnEnable()
        {
            abilityImage.fillAmount = 0;
            stop_direction = false;
        }

        private Vector3 spawnpos;
        // Update is called once per frame
      

        void Update()
        {
            base.Update();
            CooldownData();       

            getdmg = AbilityDamage;

            //nomana
            if (Ability.energy < thresholdrequirement && AbilityCooldownLeft <= 0)
            {
                manaactive = true;
                nomana.SetActive(true);
            }
            else
            {
                manaactive = false;
                nomana.SetActive(false);
                outrange.SetActive(false);
            }

            //outrange AND from cd
            if (Gun.fromCenterPLayerDistance > 60 && AbilityCooldownLeft <= 0)
            {
                if (manaactive == false)
                {
                    outrange.SetActive(true);
                }
            }
            else
            {
                outrange.SetActive(false);
            }

            if (!stop_direction)
            {
              //  targetDirection = Gun.target_hover;
            }
        
        }

        private void CooldownData()
        {
            //text that shows when ability from cd

            // if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0&& Gun.TrueDistanceOfCrosshair < 50 || latecast)
            if (latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
                Debug.Log("abilitycooldown");
            }

            if (AbilityCooldownLeft != 0)
            {
                textobjectcd.SetActive(true);
                abilityImage.fillAmount -= 1 / AbilityCooldown * Time.deltaTime;
                if (abilityImage.fillAmount <= 0)
                {
                    abilityImage.fillAmount = 0;
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
        }

        public void stopArrowRain()
        {
            anim.SetBool("arrowRainActive", false);
            //anim.ResetTrigger("basicAttack");
        }

        public IEnumerator PlayArrowSound()
        {
            yield return new WaitForSeconds(0.25f);
            aus[2].Play();
        }

        public IEnumerator startArrowVisual()
        {
            aus[1].pitch -= 0.1f;
            aus[1].Play();
      
            aus[1].pitch += 0.1f;

            var slightly_forward = (faraway_cross_aim.transform.position - effectTransform[0].position).normalized * .4f;

            // Debug.Log(targetDirection.position);
            for (int i = 0; i < 6; i++)
            {
                if (i == 0) { GameObject b = Instantiate(effect[0], effectTransform[0].position + new Vector3(0, 0.75f, 0) + slightly_forward, Quaternion.identity); }

                else
                {
                    GameObject b = Instantiate(effect[2], effectTransform[0].position + (new Vector3(0, 0.7f, 0)) + slightly_forward, Quaternion.identity);
                    GameObject c = Instantiate(effect[2], effectTransform[0].position + (new Vector3(0, 0.75f, 0)) + slightly_forward, Quaternion.identity);
                    GameObject d = Instantiate(effect[2], effectTransform[0].position + (new Vector3(0, 0.75f, 0)) + slightly_forward, Quaternion.identity);
                }

                yield return new WaitForSeconds(0.015f * i);
            }
            yield return new WaitForSeconds(.6f);
            Instantiate(effect[1], ActivePlayerManager.ActivePlayerGameObj.GetComponent<Gun>().oldpoint + new Vector3(0, 37, 0), effectTransform[1].rotation);

        }

        public override void Cast()
        {
            stop_direction = false;

            if (Gun.fromCenterPLayerDistance < 80)
            {
                Ability.globalCooldown = 0.5f; // because larger than 0.7 -> blocks double cast
                arrowRainSpawnPosition = this.transform.position + new Vector3(0, 0*heightarrows, 0); //= Gun.clonePosition2 + new Vector3(0,heightarrows,0);
                latecast = true;
               
                StartCoroutine(AvatarMoveLocalPosUp.manual_root(1.05f));
                StartCoroutine(arrowRain());
            }
            // Instantiate(effect[1], effectTransform[0].position, effectTransform[1].transform.rotation);
            aus[0].pitch -= 0.05f; 
            aus[0].Play();
            aus[0].pitch += 0.05f;
    
        }


        public IEnumerator arrowRain()
        {

            yield return new WaitForSeconds(0.01f);
            stop_direction = true;

            Vector3 spawnpos = arrowRainSpawnPosition;
            anim.SetTrigger("arrowRain");
            anim.SetBool("arrowRainActive", true);

            if (Ability.animationCooldown <= 1f)
            {
                Ability.animationCooldown = 1f;

            }


            yield return new WaitForSeconds(0.25f);

       
            if (Ability.globalCooldown <= 0.7f)
            {
                Ability.globalCooldown = 0.8f;
            }

            spawnpos = Gun.clonePosition2;
            yield return new WaitForSeconds(0.15f);
            yield return new WaitForSeconds(0.001f);


            yield return new WaitForSeconds(3f); //this decides how long the delay between charges MUST BE - causes bugs if too low -> thus cant upgrade abil
           // stop_direction = false;


        }
    }
}