using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class MarcoExplosionShot : Ability
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
        public int enhanced_attack;

        private void Awake()
        {
            //  abilityImage.fillAmount = 0;
            abilityType = 1;
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();

            if (CooldownHandler.casted > 0) { enhanced_attack = 2; }
            else { enhanced_attack = 1; }


            // Debug.Log(RapidFireMarco.rapidFireHits);
            getdmg = AbilityDamage;

            if (animboss.GetBool("Phasing") && !pyramid && !afterpyramid)
            {
                StartCoroutine(removePyramid());
            }
            if (first_hit_timer > 0f)
            {
                first_hit_timer -= Time.deltaTime;
            }
        }

        private int doublehit;
        private float first_hit_timer;
        public override void Cast()
        {
            latecast = true;
            doublehit += 1;
            Debug.Log("this is not even casting");

            oldCamTransform = curCamTransform.position;
            oldCamRotation = curCamTransform.rotation;

            RapidFireMarco.rapidFireHits = 0; // fix bug later, hardforce
            StartCoroutine(explosionShot());
            first_hit_timer = 0.6f;
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

        public IEnumerator explosionShot()
        {
            jumpCamTransform = curCamTransform.position;

            if (Ability.globalCooldown <= 0.05f)
            {
                Ability.globalCooldown = 0.05f;

            }

            anim.SetTrigger("Explosion Shot");
            anim.SetBool("casted", true);

            yield return new WaitForSeconds(0.001f);

            if (Ability.animationCooldown <= 0.7f)
            {
                Ability.animationCooldown = 0.7f;
            }

            yield return new WaitForSeconds(.3f);
            Instantiate(effect[0], curCamTransform.position, effectTransform[0].rotation);

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

   

        public void BowEventExplosionShot()
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
            anim.ResetTrigger("rapidFire");
            anim.ResetTrigger("isJumping");
            aus[0].Play();


        }
        public void remove_mana_delay()
        {
            //yield return new WaitForSeconds(0.25f);
            if ((RapidFireMarco.rapidFireHits >= 1 || RapidFireMarco.rapidFireHits == -1) && GetComponent<RapidFireMarco>().isFiring && first_hit_timer <= 0)
            {
                energy = energy - 10;
            }
            if (GetComponent<RapidFireMarco>().isFiring)
            {
                RapidFireMarco.rapidFireHits += 1;

            }
        }
        public void stopBowEventExplosionShot()
        {
            StartCoroutine(ExplosionShot());
        }

        public IEnumerator ExplosionShot()
        {
            yield return new WaitForSeconds(0.25f);
            remove_mana_delay();


            if (GetComponent<MarcoMovementController>().jumptimer > 0 && Gun.offsetcamera > 7)
            {


                Instantiate(effect[enhanced_attack], new Vector3(curCamTransform.position.x, jumpCamTransform.y, curCamTransform.position.z), oldCamRotation);
            }
            else
            {
                if (!GetComponent<RapidFireMarco>().isFiring)
                {
                    Instantiate(effect[enhanced_attack], curCamTransform.position, oldCamRotation);
                }
                else
                {
                    Instantiate(effect[2], curCamTransform.position, oldCamRotation); //als we wel rfc firen -> al1 bonus met 3 stacks!
                    Instantiate(effect[0], curCamTransform.position, effectTransform[0].rotation);

                }

            }
            aus[1].Play();

            GetComponent<AE_BowString>().InHand = false; ;
        }
    }
}