using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

public class CooldownHandler : MonoBehaviour
{

    [SerializeField] private List<CooldownData> abilitiesOnCooldown = new List<CooldownData>();



    public static CooldownHandler Instance;
    public static float alreadyCasting = 1f;
    private float cooldownreductionPCT = 1f;

    private float abil_queue_duration = 0.6f; // 0.6 seconds before global cooldown, can already cast ability to queue
    
    private void Awake()
    {
        outOfCombat = true;
        if(Instance == null)
        {
            Instance = this;
            
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        casted = 0;
    }

    [System.Serializable]
    public class CooldownData
    {
        public Ability ability;
        public float cooldown;
     

        public CooldownData(Ability ability, float cooldown)
        {
            this.ability = ability;
            this.cooldown = cooldown;
      
        }
               
    }

    [System.Serializable]
    public class DamageData
    {
        public Ability ability;
        public int abilitydamage;

        public DamageData(Ability ability, int abilitydamage)
        {
            this.ability = ability;
            this.abilitydamage = abilitydamage;
        }

    }



    public bool checkOutOfCombat()
    {
        prev = abilitiesOnCooldown.Count;
        if (prev > next) {countdown = 10;}
        next = abilitiesOnCooldown.Count;
        //in combat
        if (countdown > 0) {countdown -= Time.deltaTime;}
        return countdown <= 0;
    }


    public static bool outOfCombat;
    private int prev = 0;
    private int next = 0;
    private float countdown = 0;
    private void Update()
    {

        outOfCombat = checkOutOfCombat();


        for (int i = 0; i < abilitiesOnCooldown.Count; i++)
        {
            //the i's correspond to specific ability and this is what we show in HUD cooldown going down
            // Debug.Log($" {abilitiesOnCooldown[i]} is on cooldown for another { abilitiesOnCooldown[i].cooldown} seconds");            
            abilitiesOnCooldown[i].cooldown -= Time.deltaTime;
           
        }

        //als we terug gaan dan veranderd de index niet van de vorige sinds we iets removen!
        for (int i = abilitiesOnCooldown.Count -1; i >=0; i--)
        {
            if(abilitiesOnCooldown[i].cooldown <= 0) //als het 0 is ben je van cooldown af
            {
                abilitiesOnCooldown.RemoveAt(i);
                
            }
        }

        if (casted > 1) { casted = 1; }
        if (casted < 0) { casted = 0; }

    }
    private float enhance_timer;
    public static int casted;

    public IEnumerator delayEnhanced()
    {
        // instead of float 0.6 first_hit timer = > makes it so that also when delaying rfc after aa -> still only 2 shots when letting loose early -> still wait minimum of 0.4 sec for bug fix
        yield return new WaitForSeconds(Mathf.Min(Mathf.Max(0.9f - ActivePlayerManager.ActivePlayerGameObj.GetComponent<BasicAttackMarco>().AbilityCooldownLeft, 0.43f), 0.6f));
        casted +=  1;
    }

    public bool recasting;

    public void PutOnCooldown(Ability ability)
    {
        //hier moet ik switch van maken eigenlijk

        abilitiesOnCooldown.Add(new CooldownData(ability, ability.AbilityCooldown * cooldownreductionPCT));



        if (ability.AbilityName != "basic Attack Marco" && ActivePlayerManager.ActivePlayerNum == 1) //playernum 1 == marco
        {
            StartCoroutine( delayEnhanced());
            Debug.Log(ability.AbilityName);
        }



        /*
        if (ability.AbilityName == "Basic attack" && DashAbility.reduceCooldown == true) 
        {
            abilitiesOnCooldown.Add(new CooldownData(ability, .5f * ability.AbilityCooldown));
        }
        */

    }

    public void ReduceAbilityCooldownToValue(Ability ability, float value)
    {

        for (int i = 0; i < abilitiesOnCooldown.Count; i++)
        {
            CooldownData cooldownData = abilitiesOnCooldown[i];
            if (cooldownData.ability == ability)
            {
                cooldownData.cooldown = value;
                Debug.Log("set to value");
            }
        }    
    }

    public void ReduceAbilityCooldownByValue(Ability ability, float value)
    {

        for (int i = 0; i < abilitiesOnCooldown.Count; i++)
        {
            CooldownData cooldownData = abilitiesOnCooldown[i];
            if (cooldownData.ability == ability)
            {
                cooldownData.cooldown -= value;
                Debug.Log("set to value");
            }
        }
    }




    public float CooldownSeconds(Ability ability)
    {
        foreach (CooldownData cooldownData in abilitiesOnCooldown)
        {

            if (cooldownData.ability == ability)
            {
             //   Debug.Log(cooldownData.cooldown);
                return cooldownData.cooldown;
            }

        }

        return 0;
    }
    


    public bool IsOnCooldown(Ability ability)
    {
        foreach (CooldownData cooldownData in abilitiesOnCooldown)
        {
            
            //als cooldown in de lijst is met abilities die van cooldown zijn -> dan return true -> je kan ability casten 
            if(cooldownData.ability == ability)
            {
              //  Debug.Log($" {ability.AbilityName} is on cooldown for another { cooldownData.cooldown} seconds");

                //zorgt ervoor dat je cast queued!
                if (cooldownData.cooldown < abil_queue_duration) //0.6f
                {
                    alreadyCasting = cooldownData.cooldown;
                }
                else
                {
                    alreadyCasting = 1f;
                }

                return true;
           
            }
        }
        //als abilities niet in de lijst met abilities staat die on cooldown zijn -> return false -> je moet wachten tot ability weer wel in de lijst staat.
        return false;
           
    }
}
