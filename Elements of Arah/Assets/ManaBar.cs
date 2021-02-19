using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public static int a = 5;

    private void Start()
    {
        slider.value = 0;
    }

    public void SetMaxMana(float maxManaPCT)
    {
        slider.maxValue = maxManaPCT;
        slider.value = maxManaPCT;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetMana(float manaPCT)
    {
        slider.value = manaPCT;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
