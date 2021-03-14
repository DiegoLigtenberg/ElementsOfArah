using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreatingCharacters.Abilities
{
    public class BasicAttackMarco : Ability
    {

        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private Vector3 oldCamTransform;
        private Quaternion oldCamRotation;
        public CooldownHandler ch;

        public RFX1_TransformMotion setdmg;
        private GameObject coldmg;
        public Image abilityImage;  //the hidden image in canvas
        private bool latecast; //puts dcd image on cd when latecasted
        public CooldownReducer cdr;
        [HideInInspector] public int getdmg;
        private bool pyramid; //fixes p1 bug when pyramid comes up
                              // Start is called before the first frame update
        private void Awake()
        {
            abilityImage.fillAmount = 0;
            abilityType = 1;
        }
        private void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();

            getdmg = AbilityDamage;

            if (animboss.GetBool("Phasing") && !pyramid && !afterpyramid)
            {
                StartCoroutine(removePyramid());
            }

        }

        private int doublehit;
        public override void Cast()
        {
            latecast = true;
            doublehit += 1;
            if (doublehit % 3 == 0)
            {
                anim.SetTrigger("basicAttackx2");
            }
            else
            {
                anim.SetTrigger("basicAttack");
            }


            anim.SetBool("casted", true);
            StartCoroutine(basicAttack());
            // Instantiate(effect[1], effectTransform[0].position, effectTransform[1].transform.rotation);
        }

        private bool afterpyramid;

        private void CooldownData()
        {
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
        }

        public IEnumerator basicAttack()
        {
            if (Ability.globalCooldown <= 0.05f)
            {
                Ability.globalCooldown = 0.05f;

            }
            yield return new WaitForSeconds(0.001f);
            if (doublehit % 3 == 0)
            {
                if (Ability.animationCooldown <= 1.3f)
                {
                    Ability.animationCooldown = 1.3f;
                }
            }
            else
            {
                if (Ability.animationCooldown <= 0.5f)
                {
                    Ability.animationCooldown = 0.5f;
                    // Ability.globalCooldown = 0.05f;

                }
            }

        }

        public IEnumerator removePyramid()
        {
            afterpyramid = true;

            yield return new WaitForSeconds(1f);
            pyramid = true;
            yield return new WaitForSeconds(2.5f);
            pyramid = false;

            yield return new WaitForSeconds(15f);
            afterpyramid = false;
        }
    }
}