using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class SunShine : Ability
    {

            
        public Animator anim;
        public GameObject sunShineEffect;


        public GameObject[] effect;
        public Transform[] effectTransform;
        public Image abilityImage;  //the hidden image in canvas
        private bool latecast; //puts dcd image on cd when latecasted

        public static bool SunShineActive;
        public static float SunShineMultiplier = 1.5f;

        private DashAbility dash;


       private GameObject sunShineObj;

        public GameObject nomana;

        public GameObject textobjectcd;
        [HideInInspector] public float textcdleft;

        // Start is called before the first frame update
        void Start()
        {
            SunShineActive = false;
            SunShineMultiplier = 1.5f;
            // abilityType = 4;
            abilityImage.fillAmount = 0;
            SunShineActive = false;
            dash = GameObject.Find("heraklios_a_dizon@Jumping (2)").GetComponent<DashAbility>();
            //Debug.Log(dash);

            abilityKey = InputManager.instance.getKeyCode("sunshine");
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();

            try
            {
                if (dash.isactivated && spawned)
                {
                    Debug.Log(sunShineObj);
                    sunShineObj.SetActive(false);
                }
                if (!dash.isactivated && spawned)
                {
                    sunShineObj.SetActive(true);
                }
            }
            catch
            {
                Debug.Log("CATCH Sunshine: first update: sunshine particles not yet spawned");
            }
            if (Ability.energy < 90 && abilityCooldownLeft <= 0)
            {
                nomana.SetActive(true);
            }
            else
            {
                nomana.SetActive(false);
            }
        }


        public override void Cast()
        {
            latecast = true;
            StartCoroutine(sunShine());
            //  StartCoroutine(GetComponent<CooldownReducer>().ShortBuff(abilityType + 1));
            if (Ability.globalCooldown <= 0.8)
            {
                Ability.globalCooldown = .8f;
            }
          

        }

        private bool spawned;
        public virtual IEnumerator sunShine()
        {
            spawned = true;
            anim.SetInteger("skillNumber", 5);
            anim.SetTrigger("playSkill");

            yield return new WaitForSeconds(0.001f);
            if (Ability.animationCooldown <= 0.8f)
            {
                Ability.animationCooldown = 0.8f;
                

            }
    
            if (Ability.globalCooldown <= 0.8f)
            {
                Ability.globalCooldown = .8f;
            }


            sunShineObj = Instantiate(sunShineEffect, sunShineEffect.transform.position, sunShineEffect.transform.rotation);
            Instantiate(effect[1], sunShineEffect.transform.position, sunShineEffect.transform.rotation);
            SunShineActive = true;
            yield return new WaitForSeconds(14.5f);
            SunShineActive = false;
            Destroy(sunShineObj);
            spawned = false;

          //  Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);

            yield return null;
        }

        private void CooldownData()
        {

            textcdleft = abilityCooldownLeft;
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && GetComponent<BeamAbility>().usingBeamP == false && latecast || latecast && GetComponent<BeamAbility>().usingBeamP == false)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            if (abilityCooldownLeft != 0)
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

    }
}