using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;


public class TextMeshProTextFireBreath : MonoBehaviour
{

    public TMP_Text textFirebreath;

    public FuriousHit firebreath;
    public BeamAbility beam;
    public SunShine sunshine;
    public Avalanche avalanche;

    public Ability[] Abilities;
    public Image[] img;
    public Image[] ability_img;
    public TMP_Text[] ability_num_txt;
    public float[] time_elapsed;
    public float[] lerp_duration;

    public ArrowRainMarco arrowrainmarco;

    public Image image;
    public Image offcdimage;

    public Color startcolor;
    public Color endcolor;
    public Color endcolormana;
    public Color darkcolor;
    public Color lightcolor;

    private float timeElapsed;
    public float lerpDuration;
    private float timeElapsed2;
    public float lerpDuration2;
    private float timeElapsed3;
    public float lerpDuration3;
    private float timeElapsed4;
    public float lerpDuration4;
    private bool onlyonce; //firebreath
    private bool onlyonce2; //avalanche
    private bool onlyonce3; //beam
    private bool onlyonce4; //sunshine

    private GameObject active_player;
    private Color tempcollor;
    // Start is called before the first frame update
    void Start()
    {

        //textFirebreath = GetComponent<TMP_Text>();

        active_player = ActivePlayerManager.ActivePlayerGameObj;

        // 0 aa
        // 1 charge shot
        // 2 rapid fire
        // 3 arrow rain
        // 4 ultimate
        // 5 dash

        int abil_len = active_player.GetComponents<Ability>().Length;
        Abilities = new Ability[abil_len];

        lerp_duration = new float[abil_len];
        time_elapsed = new float[abil_len];
        for (int i = 0; i < lerp_duration.Length; i++)
        {
            lerp_duration[i] = 0.22f;
        }


        //img = new Image[abil_len];
        //ability_img = new Image[abil_len];
        //ability_num_txt =  new TMP_Text[abil_len];





    }


    public void MarcoUIControl()
    {
        //firebreath statenumber
        if (this.gameObject.name == "AbilityStateNumber")
        {
            textFirebreath.text = firebreath.showImageNumber.ToString();
        }

        for (int i = 0; i < active_player.GetComponent<AbilityManager>().Abilities.Length; i++)
        {
            Ability ability = active_player.GetComponent<AbilityManager>().Abilities[i];
            Ability_UI(ability, i);


        }
    }

    public void ArahUIControl()
    {
        //firebreath statenumber
        textFirebreath.text = firebreath.showImageNumber.ToString();


        for (int i = 0; i < active_player.GetComponent<AbilityManager>().Abilities.Length; i++)
        {
            Ability ability = active_player.GetComponent<AbilityManager>().Abilities[i];
            Ability_UI(ability, i);
        }
    }

    // Update is called once per frame
    void Update()
    {



        if (ActivePlayerManager.ActivePlayerNum == 0)
        {
            ArahUIControl();
        }

        if (ActivePlayerManager.ActivePlayerNum == 1)
        {

            MarcoUIControl();
        }



    }



    public void LerpAbility(Ability ability, int abil_type, int i)
    {

    }

    private float lerp_start_1;
    private float lerp_start_2;
    private float lerp_end_1;
    private float lerp_end_2;
    private bool furious_hit_exception;

    public void Ability_UI(Ability ability, int i)
    {

        // dash
        if (ability.abilityType == 0) { }


        if (i == 0)
        {
            return;

        }
        if (i >= 5)
        {
            return;

        }

        if (ability.AbilityName == "Furious Hit")
        {
            lerp_start_1 = 0.5f;
            lerp_start_2 = 0.3f;
            lerp_end_1 = 0.4f;
            lerp_end_2 = 0.1f;
            furious_hit_exception = true;
        }
        else
        {
            lerp_start_1 = 0.3f;
            lerp_start_2 = 0.1f;
            lerp_end_1 = 0.2f;
            lerp_end_2 = 0f;
            furious_hit_exception = false;
        }
        if (ability.AbilityCooldownLeft < 0.025f)
        {
            ability_num_txt[i].text = ability.AbilityCooldownLeft.ToString("0,0");
            img[i].color = startcolor;
            time_elapsed[i] = 0;
            ability_img[i].color = lightcolor;

    
        }
        if (ability.AbilityCooldownLeft > 1)
        {
            ability_num_txt[i].text = ability.AbilityCooldownLeft.ToString("F0");
            img[i].color = startcolor;
            ability_img[i].color = darkcolor;
            onlyonce = false;
        }

        //reset when almost  from cd




        if (ability.AbilityCooldownLeft <= 1 && ability.AbilityCooldownLeft > 0)
        {
            ability_num_txt[i].text = ability.AbilityCooldownLeft.ToString("F1");
        }







        if (ability.AbilityCooldownLeft <= lerp_start_1 && ability.AbilityCooldownLeft > lerp_end_1)
        {
            if (furious_hit_exception && firebreath.showImageNumber == 3 || !furious_hit_exception)
            {
                switch (ability.abilityType)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        if (Ability.energy > ability.basicrequirement) { img[i].color = Color.Lerp(startcolor, endcolor, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        else { img[i].color = Color.Lerp(startcolor, endcolormana, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        break;
                    case 3:
                        if (Ability.energy > ability.thresholdrequirement) { img[i].color = Color.Lerp(startcolor, endcolor, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        else { img[i].color = Color.Lerp(startcolor, endcolormana, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        break;
                    case 4:
                        if (Ability.energy > ability.ultimaterequirement) { img[i].color = Color.Lerp(startcolor, endcolor, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        else { img[i].color = Color.Lerp(startcolor, endcolormana, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        break;
                    default:
                        break;
                }
            }

        }
        if (ability.AbilityCooldownLeft <= lerp_start_2 && ability.AbilityCooldownLeft > lerp_end_2)
        {
            if (!onlyonce2)
            {
                time_elapsed[i] = 0;
                onlyonce = true;
            }
            if (furious_hit_exception && firebreath.showImageNumber == 3 || !furious_hit_exception)
            {
                switch (ability.abilityType)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        if (Ability.energy > ability.basicrequirement) { img[i].color = Color.Lerp(tempcollor, endcolor, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        else { img[i].color = Color.Lerp(tempcollor, endcolormana, time_elapsed[i] / lerp_duration[i] * 2.5f); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        break;
                    case 3:
                        if (Ability.energy > ability.thresholdrequirement) { img[i].color = Color.Lerp(tempcollor, endcolor, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        else { img[i].color = Color.Lerp(tempcollor, endcolormana, time_elapsed[i] / lerp_duration[i] * 2.5f); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        break;
                    case 4:
                        if (Ability.energy > ability.ultimaterequirement) { img[i].color = Color.Lerp(tempcollor, endcolor, time_elapsed[i] / lerp_duration[i]); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        else { img[i].color = Color.Lerp(tempcollor, endcolormana, time_elapsed[i] / lerp_duration[i] * 1.85f); time_elapsed[i] += Time.deltaTime; tempcollor = img[i].color; }
                        break;
                    default:
                        break;
                }
            }
        }




    }


    ////firebreath
    //if (this.gameObject.name == "FireBreathCooldownNumber")
    //{

    //    if (firebreath.AbilityCooldownLeft > 1)
    //    {
    //        textFirebreath.text = firebreath.AbilityCooldownLeft.ToString("F0");
    //        image.color = startcolor;
    //        offcdimage.color = darkcolor;
    //        onlyonce = false;
    //    }
    //    if (firebreath.AbilityCooldownLeft <= 1 && firebreath.AbilityCooldownLeft > 0)
    //    {
    //        textFirebreath.text = firebreath.AbilityCooldownLeft.ToString("F1");
    //    }

    //    //only pop up when enough mana
    //    if (firebreath.showImageNumber == 3 && firebreath.AbilityCooldownLeft <= 0.5 && firebreath.AbilityCooldownLeft > 0.4)
    //    {
    //        if (Ability.energy > firebreath.basicrequirement)
    //        {
    //            image.color = Color.Lerp(startcolor, endcolor, timeElapsed / lerpDuration); timeElapsed += Time.deltaTime; tempcollor = image.color;
    //        }
    //        else
    //        {
    //            image.color = Color.Lerp(startcolor, endcolormana, timeElapsed / lerpDuration); timeElapsed += Time.deltaTime; tempcollor = image.color;
    //        }

    //    }
    //    if (firebreath.showImageNumber == 3 && firebreath.AbilityCooldownLeft <= 0.3 && firebreath.AbilityCooldownLeft > .1f)
    //    {
    //        if (!onlyonce)
    //        {
    //            timeElapsed = 0;
    //            onlyonce = true;
    //        }

    //        if (Ability.energy > firebreath.basicrequirement)
    //        {
    //            image.color = Color.Lerp(tempcollor, startcolor, timeElapsed / lerpDuration); timeElapsed += Time.deltaTime;
    //        }
    //        else
    //        {
    //            image.color = Color.Lerp(tempcollor, startcolor, timeElapsed / (lerpDuration * 1.85f)); timeElapsed += Time.deltaTime;
    //        }
    //    }




    //    if (firebreath.AbilityCooldownLeft <= 0.1) { textFirebreath.text = "0,1"; image.color = startcolor; timeElapsed = 0; offcdimage.color = lightcolor; }

    //}

    ////avalanche
    //if (this.gameObject.name == "AvalancheCooldownNumber")
    //{
    //    if (avalanche.AbilityCooldownLeft > 1)
    //    {
    //        textFirebreath.text = avalanche.AbilityCooldownLeft.ToString("F0");
    //        image.color = startcolor;
    //        offcdimage.color = darkcolor;
    //        onlyonce2 = false;
    //    }
    //    if (avalanche.AbilityCooldownLeft <= 1 && avalanche.AbilityCooldownLeft > 0)
    //    {
    //        textFirebreath.text = avalanche.AbilityCooldownLeft.ToString("F1");
    //    }

    //    if (avalanche.AbilityCooldownLeft <= 0.3 && avalanche.AbilityCooldownLeft > 0.2)
    //    {
    //        if (Ability.energy > avalanche.thresholdrequirement)
    //        {
    //            image.color = Color.Lerp(startcolor, endcolor, timeElapsed2 / lerpDuration2); timeElapsed2 += Time.deltaTime; tempcollor = image.color;
    //        }
    //        else
    //        {
    //            image.color = Color.Lerp(startcolor, endcolormana, timeElapsed2 / lerpDuration2); timeElapsed2 += Time.deltaTime; tempcollor = image.color;
    //        }
    //    }
    //    if (avalanche.AbilityCooldownLeft <= 0.1 && avalanche.AbilityCooldownLeft > 0)
    //    {
    //        if (!onlyonce2)
    //        {
    //            timeElapsed2 = 0;
    //            onlyonce2 = true;
    //        }
    //        if (Ability.energy > avalanche.thresholdrequirement)
    //        {
    //            image.color = Color.Lerp(tempcollor, startcolor, timeElapsed2 / lerpDuration2); timeElapsed2 += Time.deltaTime;
    //        }
    //        else
    //        {
    //            image.color = Color.Lerp(tempcollor, startcolor, timeElapsed2 / (lerpDuration2 * 2.5f)); timeElapsed2 += Time.deltaTime;
    //        }
    //    }

    //    //reset when almost  from cd
    //    if (avalanche.AbilityCooldownLeft <= 0.025)
    //    {

    //        textFirebreath.text = avalanche.AbilityCooldownLeft.ToString("0,0"); image.color = startcolor; timeElapsed2 = 0; offcdimage.color = lightcolor;
    //    }
    //}





    ////beam
    //if (this.gameObject.name == "BeamCooldownNumber")
    //{
    //    if (beam.AbilityCooldownLeft > 1)
    //    {
    //        textFirebreath.text = beam.AbilityCooldownLeft.ToString("F0");
    //        image.color = startcolor;
    //        offcdimage.color = darkcolor;
    //        onlyonce3 = false;
    //    }
    //    if (beam.AbilityCooldownLeft <= 1 && beam.AbilityCooldownLeft > 0)
    //    {
    //        textFirebreath.text = beam.AbilityCooldownLeft.ToString("F1");
    //    }
    //    if (beam.AbilityCooldownLeft <= 0.3 && beam.AbilityCooldownLeft > 0.2)
    //    {
    //        if (Ability.energy > beam.thresholdrequirement)
    //        {
    //            image.color = Color.Lerp(startcolor, endcolor, timeElapsed3 / lerpDuration3); timeElapsed3 += Time.deltaTime; tempcollor = image.color;
    //        }
    //        else
    //        {
    //            image.color = Color.Lerp(startcolor, endcolormana, timeElapsed3 / lerpDuration3); timeElapsed3 += Time.deltaTime; tempcollor = image.color;
    //        }
    //    }
    //    if (beam.AbilityCooldownLeft <= 0.1 && beam.AbilityCooldownLeft > 0)
    //    {
    //        if (!onlyonce3)
    //        {
    //            timeElapsed3 = 0;
    //            onlyonce3 = true;
    //        }
    //        if (Ability.energy > beam.thresholdrequirement)
    //        {
    //            image.color = Color.Lerp(tempcollor, startcolor, timeElapsed3 / (lerpDuration3 * 2.5f)); timeElapsed3 += Time.deltaTime;
    //        }
    //    }

    //    //reset when almost  from cd
    //    if (beam.AbilityCooldownLeft <= 0.025)
    //    {
    //        textFirebreath.text = beam.AbilityCooldownLeft.ToString("0,0"); image.color = startcolor; timeElapsed3 = 0; offcdimage.color = lightcolor;
    //    }
    //}

    ////sunshine
    //if (this.gameObject.name == "SunshineCooldownNumber")
    //{
    //    if (sunshine.AbilityCooldownLeft > 1)
    //    {
    //        textFirebreath.text = sunshine.AbilityCooldownLeft.ToString("F0");
    //        image.color = startcolor;
    //        offcdimage.color = darkcolor;
    //        onlyonce4 = false;
    //    }
    //    if (sunshine.AbilityCooldownLeft <= 1 && sunshine.AbilityCooldownLeft > 0)
    //    {
    //        textFirebreath.text = sunshine.AbilityCooldownLeft.ToString("F1");
    //    }
    //    if (sunshine.AbilityCooldownLeft <= 0.3 && sunshine.AbilityCooldownLeft > 0.2)
    //    {
    //        if (Ability.energy > sunshine.ultimaterequirement)
    //        {
    //            image.color = Color.Lerp(startcolor, endcolor, timeElapsed4 / lerpDuration4); timeElapsed4 += Time.deltaTime; tempcollor = image.color;
    //        }
    //        else
    //        {
    //            image.color = Color.Lerp(startcolor, endcolormana, timeElapsed4 / lerpDuration4); timeElapsed4 += Time.deltaTime; tempcollor = image.color;
    //        }
    //    }
    //    if (sunshine.AbilityCooldownLeft <= 0.1 && sunshine.AbilityCooldownLeft > 0)
    //    {
    //        if (!onlyonce4)
    //        {
    //            timeElapsed4 = 0;
    //            onlyonce4 = true;
    //        }
    //        if (Ability.energy > sunshine.ultimaterequirement)
    //        {
    //            image.color = Color.Lerp(tempcollor, startcolor, timeElapsed4 / (lerpDuration4 * 2.5f)); timeElapsed4 += Time.deltaTime;
    //        }
    //    }

    //    //reset when almost  from cd
    //    if (sunshine.AbilityCooldownLeft <= 0.025)
    //    {
    //        textFirebreath.text = sunshine.AbilityCooldownLeft.ToString("0,0"); image.color = startcolor; timeElapsed4 = 0; offcdimage.color = lightcolor;
    //    }
    //}




}
