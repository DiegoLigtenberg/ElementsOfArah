using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;
using System;

public class TextMeshProStopWatchSecond : MonoBehaviour
{

    public TMP_Text current_timer_text;

    private float current_time;
    public static bool counting;
    private bool onlyonce;
    public Animator anim;
    private void Start()
    {
       current_timer_text = GetComponent<TMP_Text>();
       current_timer_text.enabled = false;
       counting = false;
    }

    void Update()
    {
      
        if (anim.GetBool("StartFight") && !onlyonce) 
        {
            counting = true;
            onlyonce = true;
        }

        if (counting)
        {
           current_timer_text.enabled = true;
            current_time += Time.deltaTime;
            current_timer_text.text = format_time(current_time);
        }
        if (counting && anim.GetBool("StartFight") && !onlyonce2)
        {
            if (!onlyonce2)
            {
                onlyonce2 = true;
                StartCoroutine(stopShowTimer());
            }
          
        }

    }

    public float get_current_time() { return current_time; }
    public void set_counting(bool input_counting) { counting = input_counting; }

    public string get_formatted_added()
    { return format_time(current_time); }

    private string format_time(float input_time)
    {
        float min = (int)(input_time / 60);
        float sec = (int)input_time % 60;
        float ms = (int)((input_time - (int)input_time) * 100.0f);
        
        return String.Format("{0:00}:{1:00}:{2:00}", min, sec, ms);
    }

    public void reset()
    { current_time = 0.0f; }

    private bool onlyonce2;
    public IEnumerator stopShowTimer()
    {
      
        yield return new WaitForSeconds(10);
        current_timer_text.enabled = false;
        
    }   
}
