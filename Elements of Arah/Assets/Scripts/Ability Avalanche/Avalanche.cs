using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




namespace CreatingCharacters.Abilities
{
    public class Avalanche : Ability
    {

        public Animator anim;
        public GameObject[] effect;
        public Transform[] effectTransform;

        [HideInInspector] public Vector3 avalancheSpawnPosition;
        [HideInInspector] public Transform curCamTransform;

        private Vector3 oldSpawnPosition;
        private Quaternion oldSpawnRotation;

        public Image abilityImage; //the hidden image in canvas
        public Image tickImage;

        Vector3 offset1 = new Vector3(0.2f, 0, 0);
        Vector3 offset2 = new Vector3(0, 0, 0.2f);
        Vector3 offset3 = new Vector3(-0.2f, 0, 0);
        Vector3 offset4 = new Vector3(0, 0, 0.2f);


        public float setTickCooldown;

        private bool latecast;

        public GameObject nomana;
        public GameObject outrange;

        public GameObject textobjectcd;
        [HideInInspector] public float textcdleft;

        private void Awake()
        {
            abilityType = 3;
            abilityImage.fillAmount = 0;
            tickImage.fillAmount = 0;

            abilityKey = InputManager.instance.getKeyCode("avalanche");

        }



        private void CooldownData()
        {
            //text that shows when ability from cd
            textcdleft = abilityCooldownLeft;

            // if (Input.GetKeyDown(abilityKey) && abilityCooldownLeft == 0&& Gun.TrueDistanceOfCrosshair < 50 || latecast)
            if (latecast)
            {
                latecast = false;
                abilityImage.fillAmount = 1;
                Debug.Log("abilitycooldown");
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

        private bool tickcooldownswitch;
        private void TickCooldownData()
        {
            /*
             if (Ability.TickCooldown > 0 && !tickcooldownswitch && abilityCooldownLeft <=0  )//&& abilityImage.fillAmount < 0.1f)
             {

                 tickcooldownswitch = true;
                 latecast = false;
                 tickImage.fillAmount = 1;
                 Debug.Log("tickcooldown");
             }
             */

            //////////////version of global cd

            /*
            if (Ability.globalCooldown > 0 && !tickcooldownswitch && abilityCooldownLeft <= 0)//&& abilityImage.fillAmount < 0.1f)
            {

                tickcooldownswitch = true;
                latecast = false;
                tickImage.fillAmount = 1;
                Debug.Log("tickcooldown");
            }

            if (tickCooldown != 0)
            {           
                
                            //settick is 2.4
                tickImage.fillAmount -= 1 / 0.9f * Time.deltaTime;

                if (tickImage.fillAmount <= 0)
                {
                    tickImage.fillAmount = 0;
                    tickcooldownswitch = false;
                }
            }
            */
        }


        private bool manaactive;
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            CooldownData();
            //TickCooldownData();

            //nomana
            if (Ability.energy < 70 && abilityCooldownLeft <= 0)
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
            if (Gun.fromCenterPLayerDistance > 60 && abilityCooldownLeft <= 0)
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

            avalancheSpawnPosition = Gun.clonePosition2;

        }

        //eingelijk moet ik wel range hebben
        public override void Cast()
        {
            //if (Ability.tickCooldown <= 0)
            {

                //  if (Gun.TrueDistanceOfCrosshair < 50 && Ability.globalCooldown <= 0)
                {
                    latecast = true;
                    RFX1_TransformMotion.turnoff = false;
                    oldSpawnPosition = avalancheSpawnPosition;
                    oldSpawnRotation = effectTransform[0].rotation;

                    StartCoroutine(AvalancheAttack());

                    // StartCoroutine(GetComponent<CooldownReducer>().ShortBuff(abilityType));
                }

            }
        }


        public virtual IEnumerator AvalancheAttack()
        {
            Ability.tickCooldown = setTickCooldown;
            yield return new WaitForSeconds(0.001f);
            Ability.animationCooldown = 0.6f;
            Ability.globalCooldown = .5f;

            // Instantiate(effect[4], oldSpawnPosition , new Quaternion(.71f, 0, 0, .71f));  //schijnbaar hoeft niet hahaha
            anim.SetInteger("skillNumber", 4);
            anim.SetTrigger("playSkill");
            anim.SetBool("isAvalanching", true);
            //portal
            Instantiate(effect[0], oldSpawnPosition, effectTransform[0].rotation);

            //rockets
            Instantiate(effect[3], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            Instantiate(effect[4], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);

            anim.SetBool("isAvalanching", false);

            Instantiate(effect[3], oldSpawnPosition + offset2, new Quaternion(.71f, 0, 0, .71f));
            Instantiate(effect[4], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);

            Instantiate(effect[3], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            Instantiate(effect[4], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);

            Instantiate(effect[3], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            Instantiate(effect[4], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);

            Instantiate(effect[3], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            Instantiate(effect[4], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);

            Instantiate(effect[3], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            Instantiate(effect[4], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));

            yield return new WaitForSeconds(1.35f);
            //Nuke
            Instantiate(effect[1], oldSpawnPosition - new Vector3(0, 10, 0), new Quaternion(0, 0, 0, 0));  //schijnbaar hoeft niet hahaha
            Instantiate(effect[2], oldSpawnPosition, new Quaternion(.71f, 0, 0, .71f));

            yield return null;
        }
    }
}

//move-able ability version
/*
//portal
Instantiate(effect[0], oldSpawnPosition, effectTransform[0].rotation);

//rockets
Instantiate(effect[2], avalancheSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);

Instantiate(effect[0], avalancheSpawnPosition, effectTransform[0].rotation);
Instantiate(effect[2], avalancheSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);
Instantiate(effect[0], avalancheSpawnPosition, effectTransform[0].rotation);
Instantiate(effect[2], avalancheSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);
Instantiate(effect[0], avalancheSpawnPosition, effectTransform[0].rotation);
Instantiate(effect[2], avalancheSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            yield return new WaitForSeconds(.5f);
Instantiate(effect[0], avalancheSpawnPosition, effectTransform[0].rotation);
Instantiate(effect[2], avalancheSpawnPosition, new Quaternion(.71f, 0, 0, .71f));     
            
            yield return new WaitForSeconds(.5f);
//Nuke
Instantiate(effect[0], avalancheSpawnPosition, effectTransform[0].rotation);
Instantiate(effect[2], avalancheSpawnPosition, new Quaternion(.71f, 0, 0, .71f));
            oldSpawnPosition = avalancheSpawnPosition;
            yield return new WaitForSeconds(.9f);
Instantiate(effect[1], oldSpawnPosition - new Vector3(0, 10, 0), new Quaternion(0, 0, 0, 0));  //schijnbaar hoeft niet hahaha
*/
