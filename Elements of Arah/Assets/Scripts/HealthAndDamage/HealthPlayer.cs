using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using CreatingCharacters.Abilities;

public class HealthPlayer : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private int startingHealth = 50;
    [SerializeField] private DamageResistances damageResistance;
    [SerializeField] HealthBar healthBar;

    [HideInInspector] public int currentHealth;
    [HideInInspector] public float currentHealthPCT = 1;
    public CharacterController cc;
    public DashAbility da;
    public BeamAbility ba;
    public Avalanche aa;
    public BasicAttack bat;
    public SunShine sa;
    public Cinemachine.CinemachineBrain cb;
    public GameObject deathanimation;
    public GameObject takedamagepanel;
    private bool takedmgonce;
    public static bool playerisdeath;
   

    private void OnEnable()
    {
        currentHealth = startingHealth;
       
    }

    private bool cantdie;
    private void Update()
    {
        /*
        if (Input.GetKey(KeyCode.P))
        {
            cantdie = true;
        }
        */

    
    
    }

    private void Awake()
    {
        currentHealthPCT = 1;

        //zorgt ervoor dat je aan begin maeteen max hp hebt
        healthBar.SetMaxHealth(currentHealthPCT);
    }


    public void takeDamage(int damageAmount, DamageTypes damageType)
    {

        if (currentHealth > 0)
        {
            if (!takedmgonce)
            {
                takedmgonce = true;
                StartCoroutine(TakedmgPanel());
            }
            
            currentHealth -= damageResistance.CalculateDamageWithResistance(damageAmount, damageType);

            //procentueel hp
            currentHealthPCT = (float)currentHealth / (float)startingHealth;

            //set hp to current health
            healthBar.SetHealth(currentHealthPCT);

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

    }

    public IEnumerator TakedmgPanel()
    {
        if (!playerisdeath)
        {
            takedamagepanel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            takedamagepanel.SetActive(false);
            takedmgonce = false;
        }
        else
        {
            takedamagepanel.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            takedamagepanel.SetActive(false);
            takedmgonce = false;
        }
  
    }

    public IEnumerator delaycamera()
    {
        yield return new WaitForSeconds(DashAbility.Beamready + 0.2f);
        da.enabled = false;
        cb.enabled = false;
    }

    public IEnumerator Die()
    {
        //  Debug.Log("should be dying");
        //zorgt ervoor dat warrior niet gaat sliden
        if (!cantdie)
        {
            deathanimation.SetActive(true);
            playerisdeath = true;
            phasingToMiddle.transition_contact_TC = false;
            P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
            P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            Ability.globalCooldown = 0;
            //cc.enabled = false;

            Debug.Log(DashAbility.Beamready);
            if (! (DashAbility.Beamready > 0.1f))
            {
                da.enabled = false;
                cb.enabled = false;
                StartCoroutine(delaycamera());
            }
           
            ba.enabled = false;
            aa.enabled = false;
            bat.enabled = false;
            sa.enabled = false;
           
           
           

            if (anim.GetInteger("Phase") > 0)
            {
                anim.SetInteger("Phase", 0);
                P1_Troll_Walk.fixbug = true;
            }
            else { P1_Troll_Walk.fixbug = false; }
           

            anim.SetTrigger("isDeath");
            yield return new WaitForSeconds(2.15f);
            anim.ResetTrigger("isDeath");
            SceneManager.LoadScene("Saved");
            //play animation

        }


    }

}
