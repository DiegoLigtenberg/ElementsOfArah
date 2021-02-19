using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public static int a = 5;

    public void SetMaxHealth(float maxHealthPCT)
    {
        slider.maxValue = maxHealthPCT;
        slider.value = maxHealthPCT;

       fill.color =  gradient.Evaluate(1f);
    }

    public void SetHealth(float healthPCT)
    {
        slider.value = healthPCT;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
