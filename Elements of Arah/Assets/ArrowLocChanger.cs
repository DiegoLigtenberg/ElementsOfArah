using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;
using CreatingCharacters.Player;

public class ArrowLocChanger : MonoBehaviour
{


    private float timer;
    private float increments = 0.01f;
    public Transform tf;
    private float smoothing = 0.009f;
    public RFX1_EffectSettingProjectile speed;

    private bool wasrunningleft;
    private bool wasrunningright;
    private bool onlyonce;


    /*
    public void ParticleDelay()
    {

        GetComponent<ParticleSystemRenderer>().GetComponent<Renderer>().enabled = true;


    }
    */
    private float c;
    // Start is called before the first frame update
    private Vector3 startpos;
    private float offset;
    private float temp;
    private float fill;
    void Awake()
    {
        //Debug.Log(ActivePlayerManager.ActivePlayerGameObj.gameObject.GetComponent<MarcoMovementController>().jumptimer);
        //  if (ActivePlayerManager.ActivePlayerGameObj.GetComponent<MarcoMovementController>().jumptimer > 0)
        {
            startpos = tf.transform.position;
            tf.position = new Vector3(tf.position.x, GameObject.Find("Center").transform.position.y, tf.position.z);
            offset = (startpos.y - tf.position.y);
            //Debug.Log(offset);
            //Debug.Log("down"); tf.localPosition += new Vector3(0, 1.5f, 0);
        }
        smoothing = 0.009f;
        timer = 0;
    }
    public float consta;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ThirdPersonMovement.RealYvelocity);
        // Debug.Log(DashAbility.Beamready);
        //  consta = 10f;
        //Debug.Log(Gun.offsetcamera);
        if (Gun.offsetcamera > 7) {

            if (this.gameObject.name.Contains("Arrow") && c >= 0)
            {
                if (offset > 0)
                {
                    tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + (1.0f / consta) * offset, tf.localPosition.z);
                    c += (1.0f / consta) * offset;
                    //Debug.Log("how we got here");
                }

                //move arrow bit up in travel duration
                else if (offset < 0)
                {
                    //veliocty y smaller than 1 is  down //ThirdPersonMovement.RealYvelocity  >5  ||
                    if (ActivePlayerManager.ActivePlayerGameObj.GetComponent<MarcoMovementController>().jumptimer > 1f)  {
                        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + (consta / consta) * offset, tf.localPosition.z);
                       // Debug.Log("DOWNER");
                      
                    c += (1.0f / consta) * offset; // could make this abs value to make this work properly
                    }
                    //jump
                    else 
                    {
                        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + (1.0f / consta) * offset, tf.localPosition.z);
                        c += (1.0f / consta) * offset;
                       // Debug.Log("UPPer");
                    }
                 //   Debug.Log(c);
                }
                if (c >= offset)
                {
                   // Debug.Log("done");
                    c = -100;
                }
                else
                {
                    if (c >= 0)
                    {
                        timer += Time.deltaTime;
                    }

                }
            }





        }
    }
}
