using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class FrictionMarco : Ability
    {

        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;

        private ThirdPersonMovement thirdPersonPlayer;
        public AudioSource[] aus;

        public static int rapidFireHits;
        public bool isFiring;

        public float first_hit_timer;

        public Image abilityImage;   //the hidden image in canvas
        public GameObject textobjectcd;
        public GameObject nomana;

        public static int friction_stacks;
        public static bool friction_active;

        private void Awake()
        {
            abilityImage.fillAmount = 0;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
            rapidFireHits = 0;
            friction_active = false;
            friction_stacks = 0;
        }

        public IEnumerator remove_firing()
        {
            yield return new WaitForSeconds(0.3f);
            isFiring = false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();

            if (Ability.energy < 75 && AbilityCooldownLeft <= 0)
            {
                nomana.SetActive(true);
            }
            else
            {
                nomana.SetActive(false);
            }


            getdmg = AbilityDamage;

            if (friction_stacks > 50) { friction_stacks = 50; }

        }


        public override void Cast()
        {
            //no double cast
            if (Ability.globalCooldown <= 0.8f)
            {
                Ability.globalCooldown = 0.8f;
            }

            StartCoroutine(CrownOfFire());
        }



        private void CooldownData()
        {
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0 && latecast || latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
            }

            if (abilityCooldownLeft != 0)
            {
                textobjectcd.SetActive(true);
                abilityImage.fillAmount -= 1 / AbilityCooldown * Time.deltaTime;
                if (abilityImage.fillAmount <= 0.05f)
                {
                    abilityImage.fillAmount = 0;
                }
            }
            else
            {
                textobjectcd.SetActive(false);
            }
        }

        public IEnumerator CrownOfFire()
        {
  

            yield return new WaitForSeconds(0.001f);

            if (Ability.globalCooldown <= 0.05f)
            {
                Ability.globalCooldown = 0.05f;
            }
            anim.SetTrigger("frictionShot");

            Instantiate(effect[0], effectTransform[0].position - new Vector3(0, 1.7f, 0), Quaternion.identity);

            friction_active = true;

            Ability.globalCooldown = 0.6f;

            if (Ability.animationCooldown <= 1.5f)
            {
                Ability.animationCooldown = 1.5f;
            }

            yield return new WaitForSeconds(13);
            friction_active = false;
        }
    }
}