using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

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
        private Vector3 jumpCamTransform;
        private Quaternion oldCamRotation;
        public Image abilityImage;   //the hidden image in canvas
        private bool latecast;       //puts dcd image on cd when latecasted
        [HideInInspector] public int getdmg;
        private bool pyramid; //fixes p1 bug when pyramid comes up
                              // Start is called before the first frame update
        private ThirdPersonMovement thirdPersonPlayer;
        public AudioSource[] aus;


        private void Awake()
        {
            abilityImage.fillAmount = 0;
            abilityType = 1;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
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

            oldCamTransform = curCamTransform.position;
            oldCamRotation = curCamTransform.rotation;
           

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
            jumpCamTransform = curCamTransform.position;

            if (Ability.globalCooldown <= 0.05f)
            {
                Ability.globalCooldown = 0.05f;

            }
            if (doublehit % 3 == 0)
            {
                anim.SetTrigger("basicAttackx2");
               
            }
            else
            {
                anim.SetTrigger("basicAttack");
                
            }
            anim.SetBool("casted", true);

            yield return new WaitForSeconds(0.001f);
            if (doublehit % 3 == 0)
            {
                if (Ability.animationCooldown <= 1.3f)
                {
                    Ability.animationCooldown = 1.0f;
                }
            }
            else
            {
                if (Ability.animationCooldown <= 0.4f)
                {
                    Ability.animationCooldown = 0.4f;
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

        public void BowReAim()
        {
            oldCamTransform = curCamTransform.position;
            oldCamRotation = curCamTransform.rotation;
            jumpCamTransform = curCamTransform.position;
        }

        public void BowEvent()
        {        
            GetComponent<AE_BowString>().InHand = true;
          
            //GENIUS MOVE TO RIVEN AA STYLE
            // yield return new WaitForSeconds(0.2f);
            if (Ability.globalCooldown <= 0.6f)
            {

                Ability.globalCooldown = 0.6f;
            }
            anim.ResetTrigger("basicAttack");
            anim.ResetTrigger("basicAttackx2");
            aus[0].Play();

  
        }

        public void stopBowEvent()
        {
            if (GetComponent<MarcoMovementController>().jumptimer > 0)
            {
                Instantiate(effect[1], jumpCamTransform, oldCamRotation);
            }
            else
            {
                Instantiate(effect[1], curCamTransform.position, oldCamRotation);

            }
            aus[1].Play();

            GetComponent<AE_BowString>().InHand = false;;

     
        }
    }
}