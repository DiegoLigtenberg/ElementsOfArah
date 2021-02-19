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
    public TMP_Text textFirebreathcd;

    public FuriousHit firebreath;
    public BeamAbility beam;
    public SunShine sunshine;
    public Avalanche avalanche;

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

    // Start is called before the first frame update
    void Start()
    {

        textFirebreath = GetComponent<TMP_Text>();



    }
    private Color tempcollor;

    // Update is called once per frame
    void Update()
    {

     



        //firebreath statenumber
        if (this.gameObject.name == "AbilityStateNumber")
        {
            textFirebreath.text = firebreath.showImageNumber.ToString();
        }

        //firebreath
        if (this.gameObject.name == "FireBreathCooldownNumber")
        {

            if (firebreath.cooldownFireBreath > 1)
            {
                textFirebreath.text = firebreath.cooldownFireBreath.ToString("F0");
                image.color = startcolor;
                offcdimage.color = darkcolor;
                onlyonce = false;
            }
            if (firebreath.cooldownFireBreath <= 1 && firebreath.cooldownFireBreath > 0)
            {
                textFirebreath.text = firebreath.cooldownFireBreath.ToString("F1");
            }

            //only pop up when enough mana


            if (firebreath.showImageNumber == 3 && firebreath.cooldownFireBreath <= 0.5 && firebreath.cooldownFireBreath > 0.4)
            {
                if (Ability.energy > firebreath.basicrequirement)
                {
                    image.color = Color.Lerp(startcolor, endcolor, timeElapsed / lerpDuration); timeElapsed += Time.deltaTime; tempcollor = image.color;
                }
                else
                {
                    image.color = Color.Lerp(startcolor, endcolormana, timeElapsed / lerpDuration); timeElapsed += Time.deltaTime; tempcollor = image.color;
                }

            }
            if (firebreath.showImageNumber == 3 && firebreath.cooldownFireBreath <= 0.3 && firebreath.cooldownFireBreath > .1f)
            {
                if (!onlyonce)
                {
                    timeElapsed = 0;
                    onlyonce = true;
                }

                if (Ability.energy > firebreath.basicrequirement)
                {
                    image.color = Color.Lerp(tempcollor, startcolor, timeElapsed / lerpDuration); timeElapsed += Time.deltaTime;
                }
                else
                {
                    image.color = Color.Lerp(tempcollor, startcolor, timeElapsed / (lerpDuration*1.85f)); timeElapsed += Time.deltaTime;
                }
            }




            if (firebreath.cooldownFireBreath <= 0.1) { textFirebreath.text = "0,1"; image.color = startcolor; timeElapsed = 0; offcdimage.color = lightcolor; }

        }

        //avalanche
        if (this.gameObject.name == "AvalancheCooldownNumber")
        {
            if (avalanche.textcdleft > 1)
            {
                textFirebreath.text = avalanche.textcdleft.ToString("F0");
                image.color = startcolor;
                offcdimage.color = darkcolor;
                onlyonce2 = false;
            }
            if (avalanche.textcdleft <= 1 && avalanche.textcdleft > 0)
            {
                textFirebreath.text = avalanche.textcdleft.ToString("F1");
            }

            if (avalanche.textcdleft <= 0.3 && avalanche.textcdleft > 0.2)
            {
                if (Ability.energy > avalanche.thresholdrequirement)
                {
                    image.color = Color.Lerp(startcolor, endcolor, timeElapsed2 / lerpDuration2); timeElapsed2 += Time.deltaTime; tempcollor = image.color;
                }
                else
                {
                    image.color = Color.Lerp(startcolor, endcolormana, timeElapsed2 / lerpDuration2); timeElapsed2 += Time.deltaTime; tempcollor = image.color;
                }
            }
            if (avalanche.textcdleft <= 0.1 && avalanche.textcdleft > 0)
            {
                if (!onlyonce2)
                {
                    timeElapsed2 = 0;
                    onlyonce2 = true;
                }
                if (Ability.energy > avalanche.thresholdrequirement)
                {
                    image.color = Color.Lerp(tempcollor, startcolor, timeElapsed2 / lerpDuration2); timeElapsed2 += Time.deltaTime;
                }
                else
                {
                    image.color = Color.Lerp(tempcollor, startcolor, timeElapsed2 / (lerpDuration2 * 2.5f)); timeElapsed2 += Time.deltaTime;
                }
            }

            //reset when almost  from cd
            if (avalanche.textcdleft <= 0.025)
            {

                textFirebreath.text = avalanche.textcdleft.ToString("0,0"); image.color = startcolor; timeElapsed2 = 0; offcdimage.color = lightcolor;                
            }
        }





        //beam
        if (this.gameObject.name == "BeamCooldownNumber")
        {
            if (beam.textcdleft > 1)
            {
                textFirebreath.text = beam.textcdleft.ToString("F0");
                image.color = startcolor;
                offcdimage.color = darkcolor;
                onlyonce3 = false;
            }
            if (beam.textcdleft <= 1 && beam.textcdleft > 0)
            {
                textFirebreath.text = beam.textcdleft.ToString("F1");
            }
            if (beam.textcdleft <= 0.3 && beam.textcdleft > 0.2)
            {
                if (Ability.energy > beam.thresholdrequirement)
                {
                    image.color = Color.Lerp(startcolor, endcolor, timeElapsed3 / lerpDuration3); timeElapsed3 += Time.deltaTime; tempcollor = image.color;
                }
                else
                {
                    image.color = Color.Lerp(startcolor, endcolormana, timeElapsed3 / lerpDuration3); timeElapsed3 += Time.deltaTime; tempcollor = image.color;
                }
            }
            if (beam.textcdleft <= 0.1 && beam.textcdleft > 0)
            {
                if (!onlyonce3)
                {
                    timeElapsed3 = 0;
                    onlyonce3 = true;
                }
                if (Ability.energy > beam.thresholdrequirement)
                {
                    image.color = Color.Lerp(tempcollor, startcolor, timeElapsed3 / (lerpDuration3 * 2.5f)); timeElapsed3 += Time.deltaTime;
                }
            }

            //reset when almost  from cd
            if (beam.textcdleft <= 0.025)
            {
                textFirebreath.text = beam.textcdleft.ToString("0,0"); image.color = startcolor; timeElapsed3 = 0; offcdimage.color = lightcolor;
            }
        }

        //sunshine
        if (this.gameObject.name == "SunshineCooldownNumber")
        {
            if (sunshine.textcdleft > 1)
            {
                textFirebreath.text = sunshine.textcdleft.ToString("F0");
                image.color = startcolor;
                offcdimage.color = darkcolor;
                onlyonce4 = false;
            }
            if (sunshine.textcdleft <= 1 && sunshine.textcdleft > 0)
            {
                textFirebreath.text = sunshine.textcdleft.ToString("F1");
            }
            if (sunshine.textcdleft <= 0.3 && sunshine.textcdleft > 0.2)
            {
                if (Ability.energy > sunshine.ultimaterequirement)
                {
                    image.color = Color.Lerp(startcolor, endcolor, timeElapsed4 / lerpDuration4); timeElapsed4 += Time.deltaTime; tempcollor = image.color;
                }
                else
                {
                    image.color = Color.Lerp(startcolor, endcolormana, timeElapsed4 / lerpDuration4); timeElapsed4 += Time.deltaTime; tempcollor = image.color;
                }
            }
            if (sunshine.textcdleft <= 0.1 && sunshine.textcdleft > 0)
            {
                if (!onlyonce4)
                {
                    timeElapsed4 = 0;
                    onlyonce4 = true;
                }
                if (Ability.energy > sunshine.ultimaterequirement)
                {
                    image.color = Color.Lerp(tempcollor, startcolor, timeElapsed4 / (lerpDuration4*2.5f)); timeElapsed4 += Time.deltaTime;
                }
            }

            //reset when almost  from cd
            if (sunshine.textcdleft <= 0.025)
            {
                textFirebreath.text = sunshine.textcdleft.ToString("0,0"); image.color = startcolor; timeElapsed4 = 0; offcdimage.color = lightcolor;
            }
        }


    }
}
