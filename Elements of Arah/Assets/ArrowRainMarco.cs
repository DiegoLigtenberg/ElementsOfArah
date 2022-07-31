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
        public static Transform targetDirection;
        public int ability_range;

        public float heightarrows;
        public static bool stop_direction;

        public GameObject textobjectcd;
        public GameObject nomana;
        private bool manaactive;
        public GameObject outrange;
        //[HideInInspector] public float textcdleft;

        private void OnEnable()
        {
            abilityImage.fillAmount = 0;
        }

        private Vector3 spawnpos;
        // Update is called once per frame
      

        void Update()
        {
            base.Update();
            CooldownData();

       

            getdmg = AbilityDamage;

            //nomana
            if (Ability.energy < 70 && AbilityCooldownLeft <= 0)
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
            anim.ResetTrigger("basicAttack");
        }

        public override void Cast()
        {
            
            if (Gun.fromCenterPLayerDistance < 80)
            {
                Ability.globalCooldown = 0.75f; // because larger than 0.7 -> blocks double cast
                arrowRainSpawnPosition = this.transform.position + new Vector3(0, 0*heightarrows, 0); //= Gun.clonePosition2 + new Vector3(0,heightarrows,0);
                latecast = true;

                stop_direction = true;
                StartCoroutine(AvatarMoveLocalPosUp.manual_root(1.05f));
                StartCoroutine(arrowRain());

             
            }
                

           

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
            if (Ability.globalCooldown <= 0.7f)
            {
                Ability.globalCooldown = 0.5f;
            }

            spawnpos = Gun.clonePosition2;
            yield return new WaitForSeconds(0.15f);
            yield return new WaitForSeconds(0.001f);

            Vector3 middle = new Vector3(0, 0, 0);
            Vector3 left = new Vector3(-1, 0, 0);
            Vector3 right = new Vector3(1, 0, 0);
            Vector3 down = new Vector3(0, 0, -1);
            Vector3 up = new Vector3(0, 0, 1);
            
    
       
           // Debug.Log(targetDirection.position);
            for (int i = 0; i <5; i++)
            {
                  GameObject b = Instantiate(effect[0], effectTransform[0].position +  (i* new Vector3(0,0,0))  + new Vector3(0, 0f* 30, 0), Quaternion.identity);
    
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(.4f);
            Instantiate(effect[1],spawnpos + new Vector3(0,24,0), effectTransform[5].rotation);


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

            yield return new WaitForSeconds(4f); //this decides how long the delay between charges MUST BE - causes bugs if too low -> thus cant upgrade abil
            stop_direction = false;


        }
    }
}