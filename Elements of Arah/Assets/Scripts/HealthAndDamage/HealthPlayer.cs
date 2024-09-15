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
    public Ability[] arahAbilityArray;
    public Ability[] marcoAbilityArray;
    public Cinemachine.CinemachineBrain cb;
    public GameObject deathanimation;
    public GameObject takedamagepanel;
    private bool takedmgonce;
    public static bool playerisdeath;
    private bool cantdie;

    private void OnEnable()
    {
        currentHealth = startingHealth;

    }

    private void Update()
    {

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
        cb.enabled = false;
    }

    public IEnumerator Die()
    {
        if (!cantdie)
        {
            deathanimation.SetActive(true);
            playerisdeath = true;
            phasingToMiddle.transition_contact_TC = false;
            P2_Troll_EnterP2WalkMiddle.dodgedIntakill = false;
            P3_Troll_EnterP3WalkMiddle.dodgedIntakill = false;
            Ability.globalCooldown = 0;

            //working with arah abilities when dead
            foreach (Ability ability in arahAbilityArray)
            {
                if (ability.AbilityName != "Dash Ability") { ability.enabled = false; }
                else
                {
                    if (!(DashAbility.Beamready > 0.1f))
                    {
                        ability.enabled = false;
                        cb.enabled = false;
                        StartCoroutine(delaycamera());
                    }
                }
            }
            //working with Marco Abilities
            foreach (Ability ability in marcoAbilityArray) { ability.enabled = false; cb.enabled = false; }


            anim.SetTrigger("isDeath");
          //  anim.GetComponent<AudioManager>().StarPlayerDied();
            yield return new WaitForSeconds(2.15f);
            anim.ResetTrigger("isDeath");
            SceneManager.LoadScene("Saved");
            //play animation

        }
    }

}
