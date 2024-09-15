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

    public BasicAttackMarco basicAttackMarco;
    public ChargeShotMarco chargeshotMarco;
    public RapidFireMarco rapidFireMarco;
    public ArrowRainMarco arrowRainMarco;
 

    //arah abilities
    public static int basicAttackDMG;
    public static int beamAbilityDMG;
    public static int avalancheDMG;
    public static int furiousHitDMG;
    private int sunshineMultiplier;

    //marco abilities
    public static int basicAttackMarcoDMG;
    public static int chargeshotMarcoDMG;
    public static int rapidFireMArcoDMG;
    public static int arrowRainMarcoDMG;




    // Start is called before the first frame update
    void Start()
    {
        basicAttackDMG = (int)basicAttack.getdmg;
        beamAbilityDMG = beamAbility.AbilityDamage;
        avalancheDMG = avalanche.AbilityDamage;
        furiousHitDMG = furiousHit.AbilityDamage;




        basicAttackMarcoDMG = basicAttackMarco.getdmg;
        rapidFireMArcoDMG = rapidFireMarco.getdmg;
        arrowRainMarcoDMG = arrowRainMarco.getdmg;
        chargeshotMarcoDMG = chargeshotMarco.getdmg;

    }

    // Update is called once per frame
    void Update()
    {
        if (!SunShine.SunShineActive)
        {
            basicAttackDMG = (int)basicAttack.getdmg;
            beamAbilityDMG = beamAbility.AbilityDamage;
            avalancheDMG = avalanche.AbilityDamage;
            furiousHitDMG = furiousHit.AbilityDamage;

            //marco
            basicAttackMarcoDMG = basicAttackMarco.getdmg;
        }

        if (SunShine.SunShineActive)
        {
            basicAttackDMG = (int)(basicAttack.getdmg * SunShine.SunShineMultiplier);
            beamAbilityDMG = (int)(beamAbility.AbilityDamage * SunShine.SunShineMultiplier);
            avalancheDMG = (int)(avalanche.AbilityDamage * SunShine.SunShineMultiplier);
            furiousHitDMG = (int)(furiousHit.AbilityDamage * SunShine.SunShineMultiplier);
        }

        basicAttackMarcoDMG = (int)basicAttackMarco.getdmg +   (20 *  Mathf.Max(0,RapidFireMarco.rapidFireHitsDMG-1));

        arrowRainMarcoDMG = (int)arrowRainMarco.getdmg;
        chargeshotMarcoDMG = (int)chargeshotMarco.getdmg;
    }
}
