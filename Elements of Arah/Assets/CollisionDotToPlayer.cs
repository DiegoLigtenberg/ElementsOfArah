using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDotToPlayer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private DamageTypes damageType;
    public Rigidbody rb;
    public ParticleSystem part;
    public AudioClip clip;
    /*
    [SerializeField] private BoxCollider bc;
    public GameObject[] EffectsOnCollision;
    [HideInInspector] public List<GameObject> CollidedInstances;
    [HideInInspector] public float HUE = -1;
    public bool CollisionEffectInWorldSpace = true;
    public float DestroyTimeDelay = 5;
    */

    private float timer;
    private float cooldown = .7f;


    private void Start()
    {


    }

    private void Update()
    {


    }



    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.collider.tag);
        
    }


    public IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(.1f);
        this.gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            if (Time.time > timer)
        {

            timer = Time.time + cooldown;
            // Damage the enemy

        
           
                Debug.Log(other.gameObject.name);

                var health = other.GetComponent<HealthPlayer>();




                if (health != null)
                {
                    Debug.Log("dealt " + damage + " damage");
                    health.takeDamage(damage, damageType);

                    //StartCoroutine(DelayDestroy());
                    // this.gameObject.SetActive(false);

                }

            }
        
        }
    }
}

/*

     if (other.tag  == "Enemy")
        {
            CollidedInstances.Clear();
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, other.gameObject.transform.position, new Quaternion()) as GameObject;

CollidedInstances.Add(instance);

                if (HUE > -0.9f)
                {
                    var color = instance.AddComponent<RFX1_EffectSettingColor>();
var hsv = RFX1_ColorHelper.ColorToHSV(color.Color);
hsv.H = HUE;
                    color.Color = RFX1_ColorHelper.HSVToColor(hsv);
                }

                instance.transform.LookAt(other.gameObject.transform.position);
                if (!CollisionEffectInWorldSpace) instance.transform.parent = transform;
                Destroy(instance, DestroyTimeDelay);

            }
        }
 */
