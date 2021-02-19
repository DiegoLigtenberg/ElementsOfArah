using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageToPlayer : MonoBehaviour
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
    private void Start()
    {


    }

    private void Update()
    {


    }

    private void OnParticleCollision(GameObject other)
    {

        Debug.Log("particle hit");
        AudioSource.PlayClipAtPoint(clip, other.transform.position);
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
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Trigger")
        {


            var health = other.GetComponent<HealthPlayer>();

            if (health != null)
            {


                Debug.Log("dealt " + damage + " damage");
                health.takeDamage(damage, damageType);

                //StartCoroutine(DelayDestroy());
                this.gameObject.SetActive(false);




            }



            //als je speler niet raakt maar grond met dmgg indicator van basic attack
            if (other.tag != "Player" && other.tag != "Enemy" && other.tag != "BasicAttack" && other.tag != "Trigger")
            {
                if (this != null)
                {
                    StartCoroutine(DelayDestroy());
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
