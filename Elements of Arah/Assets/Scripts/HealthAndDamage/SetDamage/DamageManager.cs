using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

public class DamageManager : MonoBehaviour
{
    public BasicAttack basicAttack;
    public BeamAbility beamAbility;
    public Avalanche avalanche;
    public FuriousHit furiousHit;

    public static int basicAttackDMG;
    public static int beamAbilityDMG;
    public static int avalancheDMG;
    public static int furiousHitDMG;

    private int sunshineMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        basicAttackDMG = (int) basicAttack.getdmg;
        beamAbilityDMG = beamAbility.AbilityDamage;
        avalancheDMG = avalanche.AbilityDamage;
        furiousHitDMG = furiousHit.AbilityDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if(!SunShine.SunShineActive)
            {
            basicAttackDMG = (int) basicAttack.getdmg;
            beamAbilityDMG = beamAbility.AbilityDamage;
            avalancheDMG = avalanche.AbilityDamage;
            furiousHitDMG = furiousHit.AbilityDamage;
        }

        if (SunShine.SunShineActive)
        {
            basicAttackDMG = (int)(basicAttack.getdmg* SunShine.SunShineMultiplier);
            beamAbilityDMG = (int)(beamAbility.AbilityDamage * SunShine.SunShineMultiplier) ;
            avalancheDMG = (int)(avalanche.AbilityDamage * SunShine.SunShineMultiplier);
            furiousHitDMG = (int)(furiousHit.AbilityDamage * SunShine.SunShineMultiplier);
        }
    }
}
