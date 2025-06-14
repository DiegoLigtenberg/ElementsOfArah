﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using CreatingCharacters.Abilities;

public class AE_PhysicsMotionTagShot : MonoBehaviour
{
    public bool UseCollisionDetect = true;
    public float Mass = 1;
    public float Speed = 10;
    public float RandomSpeedOffset = 0f;
    public float AirDrag = 0.1f;
    public bool UseGravity = true;
    public ForceMode ForceMode = ForceMode.Impulse;
    public float ColliderRadius = 0.05f;
    public bool FreezeRotation;

    public bool UseTargetPositionAfterCollision;
    public LayerMask CollidesWith = ~0;
    public GameObject EffectOnCollision;
    public bool CollisionEffectInWorldSpace = true;
    public bool LookAtNormal = true;
    public float CollisionEffectDestroyAfter = 5;

    public GameObject[] DeactivateObjectsAfterCollision;

    [HideInInspector] public float HUE = -1;

    public event EventHandler<AE_CollisionInfo> CollisionEnter;

    Rigidbody rigid;
    SphereCollider collid;
    ContactPoint lastContactPoint;
    Collider lastCollider;
    Vector3 offsetColliderPoint;
    bool isCollided;
    GameObject targetAnchor;
    bool isInitializedForce;
    float currentSpeedOffset;

    private ArrowStick arrowstick;
    private GameObject arrow_indicator;

    SphereCollider collid_impact;

    [SerializeField] public int damage = 1;
    [SerializeField] private DamageTypes damageType;



    void OnEnable()
    {
        foreach (var obj in DeactivateObjectsAfterCollision)
        {
            if (obj != null)
            {
                if (obj.GetComponent<ParticleSystem>() != null) obj.SetActive(false);
                obj.SetActive(true);
            }
        }
        currentSpeedOffset = Random.Range(-RandomSpeedOffset * 10000f, RandomSpeedOffset * 10000f) / 10000f;
        InitializeRigid();

        arrowstick = GetComponentInChildren<ArrowStick>();
        arrow_indicator = arrowstick.gameObject;
     
    }

    void InitializeRigid()
    {
        if (UseCollisionDetect)
        {
            collid = gameObject.AddComponent<SphereCollider>();
            collid.center -= new Vector3(0, 0, -1.5f);
            collid.radius = ColliderRadius;
        }

        isInitializedForce = false;


    }

    void InitializeForce()
    {
        rigid = gameObject.AddComponent<Rigidbody>();
        rigid.mass = Mass;
        rigid.drag = AirDrag;
        rigid.useGravity = UseGravity;
        if (FreezeRotation) rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.AddForce(transform.forward * (Speed + currentSpeedOffset), ForceMode);
        isInitializedForce = true;

    }

    public GameObject IMPACT;
    public IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(IMPACT);
    }

    public GameObject bomb;
    private int bomb_counter;


    void OnCollisionEnter(Collision collision)
    {
        GameObject empty = new GameObject("empty");
        IMPACT = Instantiate(empty, collision.GetContact(0).point, Quaternion.identity);
        Destroy(empty);
        StartCoroutine(Destroyer());



       
        try
        {
            GameObject impact = arrowstick.ExplosionDamage(arrow_indicator.transform.position, 3f);

            int rng = (int)Random.Range(0, 2);
            this.transform.SetParent(ActivePlayerManager.ActivePlayerGameObj.GetComponent<TagShotMarco>().boss_attach_transform[rng]);
            Debug.Log(ActivePlayerManager.ActivePlayerGameObj.GetComponent<TagShotMarco>().boss_attach_transform[rng]);

            if (impact.gameObject.name.Contains("Spine")  )
            {
                //bomb.transform.localPosition += new Vector3(Random.RandomRange(-.5f,.5f), 0, 0f); // z was -1.5f
           
            }
            //   bomb.transform.position = GameObject.Find(impact.name).transform.GetChild(0).transform.position;
            //note that boss shoudl be warrior -> change boss
            bomb.transform.position = ActivePlayerManager.ActivePlayerGameObj.GetComponent<TagShotMarco>().boss_tag_location[rng].transform.position;
            bomb.transform.rotation = ActivePlayerManager.ActivePlayerGameObj.GetComponent<TagShotMarco>().boss_tag_location[rng].transform.rotation;

            //Debug.Log(GameObject.Find(impact.name).transform.GetChild(0).name);
            //bomb.transform.rotation = GameObject.Find(impact.name).transform.GetChild(0).transform.rotation;

            // ActivePlayerManager.ActivePlayerGameObj.GetComponent<TagShotMarco>().bombhits[0] += 1; 


            //GetChild(0).transform.rotation;
            //   bomb.transform.localScale = GameObject.Find(impact.name).transform.GetChild(0).transform.localScale;
            /*
            if ( impact.gameObject.name.Contains("Arm"))
            {
                bomb.transform.localPosition += new Vector3(0, .1f, -.5f);
                Debug.Log("Arm UPDATED!");
            }
            if (impact.gameObject.name.Contains("Shoulder"))
            {
                bomb.transform.localPosition += new Vector3(0, 0, -0.5f);
                Debug.Log("Shoulder UPDATED!");
            }
            if (impact.gameObject.name.Contains("Leg"))
            {
                bomb.transform.localPosition += new Vector3(-.3f, -.4f, -1.2f);
                Debug.Log("Shoulder UPDATED!");
            }
            if (impact.gameObject.name.Contains("Foot"))
            {
                bomb.transform.localPosition += new Vector3(.5f, .3f, -.8f);
                Debug.Log("Shoulder UPDATED!");
            }
            */



            //  Debug.Log(GameObject.Find(impact.name).transform.GetChild(0).transform.position);
            // Debug.Log(GameObject.Find(impact.name).transform.GetChild(0).name);
        }
        catch
        {
            UseTargetPositionAfterCollision = true;
            Debug.Log("set to true");
        }



        //Checkforlayermask
        // if ((CollidesWith & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        if (collision.collider.tag != "Player")
        {

            if (isCollided && !UseCollisionDetect) return;
            foreach (ContactPoint contact in collision.contacts)
            {
                if (!isCollided)
                {
                    isCollided = true;
                    //offsetColliderPoint = contact.otherCollider.transform.position - contact.point;
                    // lastCollider = contact.otherCollider;
                    // lastContactPoint = contact;
                    if (UseTargetPositionAfterCollision)
                    {
                        if (targetAnchor != null) Destroy(targetAnchor);

                        targetAnchor = new GameObject();
                        targetAnchor.hideFlags = HideFlags.HideAndDontSave;
                        targetAnchor.transform.parent = contact.otherCollider.transform;
                        targetAnchor.transform.position = contact.point;
                        targetAnchor.transform.rotation = transform.rotation;
                        //targetAnchor.transform.LookAt(contact.normal);
                    }
                }

                if (collision.collider.GetComponent<Health>() != null)
                {
                    var health = collision.collider.GetComponent<Health>();

                    if (health != null)
                    {
                        Debug.Log("dealt " + damage + " damage");


                        if (!this.gameObject.name.Contains("CollisionAvalanche"))
                        {

                            if (this.gameObject.name.Contains("Collision basicattack"))
                            {
                                health.takeDamage(damage, damageType);

                                if (!this.gameObject.name.Contains("Collision NODAMAGE"))
                                {

                                    /*
                                        if (dashability.orbCount == 0) { health.takeDamage(damage, damageType); }
                                        if (dashability.orbCount == 1) { health.takeDamage(damage, damageType); }
                                        if (dashability.orbCount == 2) { health.takeDamage(damage, damageType); }
                                        if (dashability.orbCount == 3) { health.takeDamage(damage, damageType); }
                                    */
                                }

                            }

                            if (!this.gameObject.name.Contains("Collision basicattack"))
                            {
                                health.takeDamage(damage, damageType);
                            }

                        }

                        if (this.gameObject.name.Contains("Collision basicattack"))
                        {
                            //   if (dashability.orbCount == 0) { health.takeDamage(0, DamageTypes.Fire); }
                            /*
                            if (SunShine.SunShineActive)
                            {
                                if (dashability.orbCount == 1) { health.takeDamage(10, DamageTypes.Elemental); }
                                if (dashability.orbCount == 2) { health.takeDamage(20, DamageTypes.Elemental); }
                                if (dashability.orbCount == 3) { health.takeDamage(30, DamageTypes.Elemental); }
                            }
                            */
                        }

                    }


                }
                var handler = CollisionEnter;
                if (handler != null)
                    handler(this, new AE_CollisionInfo { ContactPoint = contact });

                if (EffectOnCollision != null)
                {
                    var instance = Instantiate(EffectOnCollision, contact.point, new Quaternion()) as GameObject;

                    if (HUE > -0.9f)
                    {
                        var color = instance.AddComponent<AE_EffectSettingColor>();
                        var hsv = AE_ColorHelper.ColorToHSV(color.Color);
                        hsv.H = HUE;
                        color.Color = AE_ColorHelper.HSVToColor(hsv);
                    }

                    if (LookAtNormal) instance.transform.LookAt(contact.point + contact.normal);
                    else instance.transform.rotation = transform.rotation;
                    if (!CollisionEffectInWorldSpace) instance.transform.parent = contact.otherCollider.transform.parent;
                    Destroy(instance, CollisionEffectDestroyAfter);
                }
            }

            foreach (var obj in DeactivateObjectsAfterCollision)
            {
                if (obj != null)
                {
                    var ps = obj.GetComponent<ParticleSystem>();
                    if (ps != null) ps.Stop();
                    else obj.SetActive(false);
                }
            }


            if (rigid != null) Destroy(rigid);
            if (collid != null) Destroy(collid);


        }
    }



    private void Update()
    {
        if (!isInitializedForce) InitializeForce();
        if (UseTargetPositionAfterCollision && isCollided && targetAnchor != null)
        {
            transform.position = targetAnchor.transform.position;
            transform.rotation = targetAnchor.transform.rotation;
        }
    }

    public class AE_CollisionInfo : EventArgs
    {
        public ContactPoint ContactPoint;
    }

    //private void Update()
    //{
    //    var kinetic = rigid.mass* Mathf.Pow(rigid.velocity.magnitude, 2) * 0.5f;
    //    Debug.Log(transform.localPosition.magnitude + "   time" + (Time.time - startTime) + "  speed" + (transform.localPosition.magnitude/ (Time.time - startTime)));
    //}

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = new Quaternion();
        isCollided = false;
        if (rigid != null) Destroy(rigid);
        if (collid != null) Destroy(collid);
    }

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            return;

        var t = transform;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(t.position, ColliderRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(t.position, t.position + t.forward * 100);
    }
}