/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0,0,0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = true;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    [SerializeField] public int damage = 1;
    [SerializeField] private DamageTypes damageType;
    private float timer;
    private int max_hits;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        if(name.Contains("dmg") )
        damage = DamageManager.arrowRainMarcoDMG ;
        max_hits = 5;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
      
       
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            if (timer <= 0)
            {
                //only allows for 5 hits of dmg (otherwise visual noise)
                if (max_hits>0)
                {
                    if (name.Contains("dmg"))
                    {
                        var health = other.gameObject.GetComponent<Health>();
                        health.takeDamage(damage, damageType);
                        timer = 0.2f;
                        max_hits -= 1;
                    }
                       
                }               
            }
        }

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);     
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { instance.transform.rotation = Quaternion.Euler(rotationOffset); }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }
                Destroy(instance, DestroyTimeDelay);
            }
        }
        if (DestoyMainEffect == true)
        {
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }
}
