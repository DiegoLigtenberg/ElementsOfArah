using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreatingCharacters.Abilities
{
    public class BasicAttack : Ability
    {
        public Animator anim;
        public Animator animboss;
        public GameObject[] effect;
        public Transform[] effectTransform;
        public Transform curCamTransform;
        private Vector3 oldCamTransform;
        private Quaternion oldCamRotation;
     
        public Image abilityImage;  //the hidden image in canvas
        private bool latecast; //puts dcd image on cd when latecasted
        public DashAbility dashability;
        public int getdmg;
        private bool pyramid;
        private void Awake()
        {
            abilityImage.fillAmount = 0;
            abilityType = 1;
        }

        private void Start()
        {
            dashability = GetComponent<DashAbility>();
   
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

        private void CooldownData()
        {
            if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0  && GetComponent<BeamAbility>().usingBeamP == false && latecast || latecast && GetComponent<BeamAbility>().usingBeamP == false )
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

        public override void Cast()
        {
            latecast = true;
            
            //zorgt voor casten als je niet klikt
            if ( true )//Ability.globalCooldown <= 0 )// && GetComponent<BeamAbility>().usingBeamP == false)
            {

                oldCamTransform = curCamTransform.position;
                oldCamRotation = curCamTransform.rotation;


                StartCoroutine(basicAttack());

              //  Ability.globalCooldown = 0.5f; //we willen geen global cooldown voor deze ability
            

            }
       
        }

        public virtual IEnumerator basicAttack()
        {
            
            anim.SetInteger("skillNumber", 2);
            anim.SetTrigger("playSkill");

            // nice als je kleine delay wilt voor je nieuwe abil cast         
            if (Ability.globalCooldown <= 0.05f)
            {
                Ability.globalCooldown = 0.05f;
              
            }
            
            

            yield return new WaitForSeconds(0.001f);
            if (Ability.animationCooldown <= 0.3f)
            {
                Ability.animationCooldown = 0.3f;
               // Ability.globalCooldown = 0.05f;
         
            }
      
      

          //  Ability.globalCooldown = 0.2f;
            yield return new WaitForSeconds(0.3f);



            //Instantiate(effect[0], effectTransform[0].position, effectTransform[0].rotation);
            //Instantiate(effect[0], new Vector3(oldCamTransform.x, curCamTransform.position.y, oldCamTransform.z), oldCamRotation);    //this would spawn it at cur location but its off
            if (pyramid && !DashAbility.PhasingBugFixAA )            {

             
                Instantiate(effect[0], new Vector3(oldCamTransform.x,curCamTransform.position.y,oldCamTransform.z), oldCamRotation);             
            }
            else
            {
                Instantiate(effect[0], oldCamTransform, oldCamRotation);
            }
         
          

            //GENIUS MOVE TO RIVEN AA STYLE
           // yield return new WaitForSeconds(0.2f);
            if (Ability.globalCooldown <= 0.6f)
            {
            
                Ability.globalCooldown = 0.6f;
            }

            if (GetComponent<FuriousHit>().cooldownFireBreath <= 0.6f)
            {
            //    GetComponent<FuriousHit>().cooldownFireBreath = 0.6f;
            }
          

            //hier kunnen effecten komen mocht ik dat willen
            yield return null;
        }

        private bool afterpyramid;
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