using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private float maxRange;

        public Image abil_img;
        public Image img;
        public Color startcolor;
        public Color endcolor;
        public Color endcolormana;
        private Color tempcollor;
        public Color darkcolor;
        public Color lightcolor;
        public float time_elapsed;
        private float lerp_duration;
        private bool onlylerponce;

        private void OnEnable()
        {
            abilityImage.fillAmount = 0;
            stop_direction = false;
            maxRange = 60;

            abilityKey = InputManager.instance.getKeyCode("arrowrain");
          
            Debug.Log(abilityKey);
        }

        private Vector3 spawnpos;
        // Update is called once per frame

        private bool getAbilityConditions()
        {
            //if at least 1 condition isn't met, then there is a violation thus this function should return false
            bool condition_1 = Gun.fromCenterPLayerDistance < maxRange; // max range
            bool[] conditions = { condition_1, };
            return !conditions.All(x => x); //  not all <=> at least one not -> we return true if at least 1 condition is not met, aka violated
        }

        void Update()
        {
            base.Update();
            CooldownData();

            abilityConditionsViolated = getAbilityConditions();

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
            if (Gun.fromCenterPLayerDistance > maxRange && AbilityCooldownLeft <= 0)
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

            if (highlight_timer > 0) { highlight_timer -= Time.deltaTime; }
            if (lerping && onlylerponce)
            {
                highlight_color();
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
                abilityImage.fillAmount = abilityCooldownLeft / AbilityCooldown;
                if (abilityCooldownLeft <= 0.03f)
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


        private bool lerping;
        private float highlight_timer;

        public void highlight_color()
        {
            if (highlight_timer < 0.025f)
            {

                img.color = startcolor;
                time_elapsed = 0;
                abil_img.color = lightcolor;

            }
            else
            {
                img.color = Color.Lerp(startcolor, endcolor, time_elapsed / lerp_duration);
                time_elapsed += Time.deltaTime;
                tempcollor = img.color;
            }
        }

        public void Update_AbilityUI_CD_Reductione(float reduction_value)
        {
            textobjectcd.SetActive(true);
            AbilityCooldownLeft = reduction_value;
        }


        public IEnumerator PotentialReset()
        {
            onlylerponce = true;
            yield return new WaitForSeconds(0.69f);
            if (FrictionMarco.friction_active && FrictionMarco.friction_stacks >=0)
            {
                float cd_time = 2.7f;
              //  Debug.Log(this.abilityCooldownLeft);
                CooldownHandler.Instance.ReduceAbilityCooldownToValue(this, cd_time);

               // Debug.Log(this.abilityCooldownLeft);
                this.Update_AbilityUI_CD_Reductione(cd_time);
                lerping = true;
                //color start
                this.img.color = startcolor;
                this.abil_img.color = darkcolor;
                highlight_timer = .22f;

                //wait lerp
                yield return new WaitForSeconds(.62f);

                lerping = false;
                FrictionMarco.friction_stacks += 0;

            }
            onlylerponce = false;
        }

        public override void Cast()
        {
            stop_direction = false;

            // reset unique targets hit for arrow rain
            ArrowRainDamage.uniqueTargets = new List<GameObject>();

            if (Gun.fromCenterPLayerDistance < 80)
            {
                Ability.globalCooldown = 0.5f; // because larger than 0.7 -> blocks double cast
                arrowRainSpawnPosition = this.transform.position + new Vector3(0, 0*heightarrows, 0); //= Gun.clonePosition2 + new Vector3(0,heightarrows,0);
                latecast = true;
               
                StartCoroutine(AvatarMoveLocalPosUp.manual_root(1.05f));
                StartCoroutine(arrowRain());

                StartCoroutine(PotentialReset());

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