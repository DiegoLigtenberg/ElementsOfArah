using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;
using CreatingCharacters.Player;

public class ParticleSpawnLocChanger : MonoBehaviour
{


    private float timer;
    private float increments = 0.01f;
    public Transform tf;
    private float smoothing = 0.009f;
    public RFX1_EffectSettingProjectile speed;

    private bool wasrunningleft;
    private bool wasrunningright;
    private bool onlyonce;

  
    public void ParticleDelay()
    {

        GetComponent<ParticleSystemRenderer>().GetComponent<Renderer>().enabled = true;


    }
    float c;
    // Start is called before the first frame update
    void Awake()
    {
        if (this.gameObject.name.Contains("PTrail"))
        {

            if (Ability.animationCooldown < 0.2f)
            {

                //rechts
                if (Input.GetKey(KeyCode.D))
                {
                    tf.localPosition = new Vector3(tf.localPosition.x + 1.1f, tf.localPosition.y, tf.localPosition.z);
                    wasrunningright = true;
                }
                //links
                if (Input.GetKey(KeyCode.A))
                {
                    tf.localPosition = new Vector3(tf.localPosition.x - 0.85f, tf.localPosition.y, tf.localPosition.z);
                    wasrunningleft = true;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y, tf.localPosition.z + 1f);
                }
                if (Input.GetKey(KeyCode.S))
                { 
                    tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y, tf.localPosition.z - 1f);
                }




                if (this.gameObject.name.Contains("PTrail") && DashAbility.AACorrection <= 0)
                {
                    if (ThirdPersonMovement.scaledvelocity > 0.12f && ThirdPersonMovement.scaledvelocity < 0.6f)
                    {
                        c = 0.9f - ThirdPersonMovement.scaledvelocity;
                    }


                    //Debug.Log(ThirdPersonMovement.scaledvelocity);
                    tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + c + Mathf.Clamp(4 * ThirdPersonMovement.scaledvelocity, -3.5f, 2.85f), tf.localPosition.z);
                }

                if (DashAbility.AACorrection >= 0)
                {
                    tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y - 1.15f, tf.localPosition.z);
                }



                c = 0;


            }

            if (Gun.offsetcamera < 9)
            {
                Invoke("ParticleDelay", 0f);

            }
            else
            {
                Invoke("ParticleDelay", 0.0f);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(DashAbility.Beamready);



        if (this.gameObject.name.Contains("PTrail"))
        {


            /*
            // Debug.Log(Gun.offsetcamera);
            if (Gun.offsetcamera < 5)
            {
                //   Debug.Log("this happened");
                speed.SpeedMultiplier = 1.5f;
            }
            if (Gun.offsetcamera >= 5 && Gun.offsetcamera < 10)
            {
                speed.SpeedMultiplier = 14.5f;
            }

            if (Gun.offsetcamera >= 10 && Gun.offsetcamera < 20)
            {
                speed.SpeedMultiplier = 16;
            }
            if (Gun.offsetcamera >= 20 && Gun.offsetcamera < 60)
            {
                speed.SpeedMultiplier = 17;
            }
            if (Gun.offsetcamera >= 60 && Gun.offsetcamera < 100)
            {
                speed.SpeedMultiplier = 17;
            }
            else
            {
                speed.SpeedMultiplier = 17.5f;
            }
            //   Debug.Log(Gun.offsetcamera);
            // Debug.Log(tf.localPosition.x);
            */
            float temp = (0.00025f - Gun.offsetcamera / 400000);


        //debug.log(temp);
        timer += Time.deltaTime;
            if (timer >= increments)
            {


                //hardforce
                if (Gun.offsetcamera < 9 && tf.localPosition.x > -5f)
                {
                    tf.localPosition = new Vector3(tf.localPosition.x - smoothing + (6f * temp), tf.localPosition.y, tf.localPosition.z);
                }

                //normaal sloom

                else
                {
                    if (!wasrunningleft && !wasrunningright && tf.localPosition.x > -5f && !onlyonce)
                    {
                        tf.localPosition = new Vector3(tf.localPosition.x - smoothing + temp, tf.localPosition.y, tf.localPosition.z);
                    }

                    if (tf.localPosition.x < -5f)
                    {
                        if (wasrunningleft)
                        {

                            onlyonce = true;
                            tf.localPosition = new Vector3(tf.localPosition.x - smoothing + temp + 1, tf.localPosition.y, tf.localPosition.z);
                        }
                    }


                    if (tf.localPosition.x > 0.45)
                    {
                        if (wasrunningright)
                        {

                            onlyonce = true;
                            tf.localPosition = new Vector3(tf.localPosition.x - smoothing + temp - .025f, tf.localPosition.y, tf.localPosition.z);
                        }


                    }

                }

                smoothing = smoothing + 0.00002f;
                timer = 0;

            }



        }
    }
}
