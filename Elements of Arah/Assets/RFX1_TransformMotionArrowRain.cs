using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CreatingCharacters.Abilities;
using Random = UnityEngine.Random;



public class RFX1_TransformMotionArrowRain : MonoBehaviour
{
    public float Distance = 30;
    public float Speed = 1;
    //public float Dampeen = 0;
    //public float MinSpeed = 1;
    public float TimeDelay = 0;
    public float RandomMoveRadius = 0;
    public float RandomMoveSpeedScale = 0;
    public GameObject Target;

    public LayerMask CollidesWith = ~0;


    public GameObject[] EffectsOnCollision;
    public float CollisionOffset = 0;
    public float DestroyTimeDelay = 5;
    public bool CollisionEffectInWorldSpace = true;
    public GameObject[] DeactivatedObjectsOnCollision;
    [HideInInspector] public float HUE = -1;
    [HideInInspector] public List<GameObject> CollidedInstances;

    private Vector3 startPosition;
    private Vector3 startPositionLocal;
    Transform t;
    Transform targetT;
    private Vector3 oldPos;
    private bool isCollided;
    private bool isOutDistance;
    private Quaternion startQuaternion;
    //private float currentSpeed;
    private float currentDelay;
    private const float RayCastTolerance = 0.15f;
    private bool isInitialized;
    private bool dropFirstFrameForFixUnityBugWithParticles;
    public event EventHandler<RFX1_CollisionInfo> CollisionEnter;
    Vector3 randomTimeOffset;

    [SerializeField] public int damage = 1;
    [SerializeField] private DamageTypes damageType;

    public static bool turnoff = false;

    public DashAbility dashability;


    float parabola;

    float speedfactor;

    float heightdif;
    float startheight;

    private float forward_timer;

    //maak hier een list van -> loop over alle damage en kijk welke je wilt
    //[SerializeField] private DealDamage dealDamage;
    //[SerializeField]  private BasicAttack basicAttackDamage;

    private Gun gun;

    private float xy_dist_towards_target;
    private float xy_dist_once;

    private void Awake()
    {

        dashability = GameObject.Find(ActivePlayerManager.ActivePlayerName).GetComponent<DashAbility>();
        speedfactor = Random.Range(1, 1.5f);
        forward_timer = 1f;
    }


    void Start()
    {
        gun = ActivePlayerManager.ActivePlayerGameObj.GetComponent<Gun>();
        Target = gun.hover_clone_trans.gameObject; //GameObject.Find("Warrior Idle/HitMeHere");
        //Target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        parabola = 55;
        t = transform;
        if (Target != null) targetT = Target.transform;
        xy_dist_towards_target = (new Vector3(t.transform.position.x, 0, t.transform.position.z) - new Vector3(targetT.transform.position.x, 0, targetT.transform.position.z)).magnitude;
        xy_dist_once = xy_dist_towards_target;
        if (Target != null) targetT.transform.position = targetT.transform.position + (targetT.transform.position - ActivePlayerManager.ActivePlayerGameObj.transform.position).normalized * (xy_dist_once/50);


        startQuaternion = t.rotation;
        startPositionLocal = t.localPosition;
        startPosition = t.position;
        oldPos = t.TransformPoint(startPositionLocal);
        Initialize();
        isInitialized = true;
        speedfactor = 1;
        startheight = t.position.y;
    }


    void OnEnable()
    {
        if (isInitialized) Initialize();
    }

    void OnDisable()
    {
        if (isInitialized) Initialize();
    }

    private void Initialize()
    {
        isCollided = false;
        isOutDistance = false;
        //currentSpeed = Speed;
        currentDelay = 0;
        startQuaternion = t.rotation;
        t.localPosition = startPositionLocal;
        oldPos = t.TransformPoint(startPositionLocal);
        OnCollisionDeactivateBehaviour(true);
        dropFirstFrameForFixUnityBugWithParticles = true;
        randomTimeOffset = Random.insideUnitSphere * 10;
        end = t.transform.position.y;
    }

    private float begin;
    private float end;
    private float heightvel;
    private bool onlyonce;
    private Vector3 search_pos;

    void Update()
    {
        heightdif = startheight - t.position.y;

        begin = t.transform.position.y;
        heightvel = Mathf.Abs(begin - end);
        end = t.transform.position.y;
        //Debug.Log(heightdif);

        if (!dropFirstFrameForFixUnityBugWithParticles)
        {
            UpdateWorldPosition();
        }
        else dropFirstFrameForFixUnityBugWithParticles = false;

        forward_timer -= Time.deltaTime;
    }

    private Vector3 temp_forward_vec;
    void UpdateWorldPosition()
    {
        currentDelay += Time.deltaTime;
        if (currentDelay < TimeDelay)
            return;

        Vector3 randomOffset = Vector3.zero;
        if (RandomMoveRadius > 0)
        {

            randomOffset = GetRadiusRandomVector() * RandomMoveRadius;
            if (Target != null)
            {
                if (targetT == null) targetT = Target.transform;
                if (Target != null) targetT.transform.position = targetT.transform.position   + (targetT.transform.position - ActivePlayerManager.ActivePlayerGameObj.transform.position).normalized * (xy_dist_once/50);

                var fade = Vector3.Distance(t.position, targetT.position) / Vector3.Distance(startPosition, targetT.position);         
                if (!onlyonce)
                {
                    randomOffset *= fade;
                    onlyonce = true;
      
                }
            }
        }

        var frameMoveOffset = Vector3.zero;
        var frameMoveOffsetWorld = Vector3.zero;
        if (!isCollided && !isOutDistance)
        {
            //currentSpeed = Mathf.Clamp(currentSpeed - Speed*Dampeen*Time.deltaTime, MinSpeed, Speed);
            if (Target == null)
            {
                Speed = Speed * speedfactor;  //+ (heightdif / 40);
                var currentForwardVector = (Vector3.forward + randomOffset) * (Speed) * Time.deltaTime;
                frameMoveOffset = t.localRotation * currentForwardVector;
                frameMoveOffsetWorld = startQuaternion * currentForwardVector;


            }
            else
            {

             

                xy_dist_towards_target = (new Vector3(t.transform.position.x, 0, t.transform.position.z) - new Vector3(targetT.transform.position.x, 0, targetT.transform.position.z)).magnitude;


                Speed = 50 + Random.Range(-30, 30) + (heightdif / 1.2f);
                // Debug.Log(Speed);



                // locks forward vector to old position when the thrown arrows are 1.1 sec (forward timres) in air, then they go to just downards position

                // var forwardVec = (search_pos     + new Vector3(0, parabola, 0) - t.position).normalized * (Speed/ 40);
                if (forward_timer <= 0 || xy_dist_towards_target < 15f && forward_timer - 0.5f * forward_timer <= 0)
                {
                    temp_forward_vec = (new Vector3(targetT.position.x, -1000000, targetT.position.z) + new Vector3(0, parabola, 0) + t.position).normalized * (Speed / 50);

                    //if (xy_dist_towards_target > 0) { Debug.Log("WTF"); randomOffset = Vector3.zero; }
                    randomOffset *= .3f;
                    t.LookAt(targetT.position + new Vector3(0, -100000f, 0));
                }
                else
                {
                    temp_forward_vec = (targetT.position + new Vector3(0, parabola, 0) - t.position).normalized * (Speed / 40);
                    t.LookAt(targetT.position + new Vector3(0, parabola, 0));
                }



                var currentForwardVector = (temp_forward_vec + randomOffset) * (Speed) * Time.deltaTime;
                //   Debug.Log(currentForwardVector);

                frameMoveOffset = currentForwardVector;
                frameMoveOffsetWorld = currentForwardVector;
                parabola -= Speed * Time.deltaTime;
                randomOffset = Vector3.zero;
                //   Speed;

                //Debug.Log(speedfactor);
                // Debug.Log(Speed);
                //Debug.Log(forwardVec);
            }
        }
     

        var currentDistance = (t.localPosition + frameMoveOffset - startPositionLocal).magnitude;
        Debug.DrawRay(t.position, frameMoveOffsetWorld.normalized * (Distance - currentDistance), Color.red, 2f);
        // Debug.DrawLine(t.position, frameMoveOffsetWorld.normalized * (Distance - currentDistance) * 100,Color.red,Mathf.Infinity);



        RaycastHit hit;
        if (!isCollided && Physics.Raycast(t.position, frameMoveOffsetWorld.normalized, out hit, Distance, CollidesWith))
        {


            if (frameMoveOffset.magnitude + RayCastTolerance > hit.distance)
            {
                if (hit.collider.GetComponent<Health>() != null)
                {
                    var health = hit.collider.GetComponent<Health>();

                    if (health != null)
                    {
                        Debug.Log("dealt " + damage + " damage");


                        if (!this.gameObject.name.Contains("CollisionAvalanche"))
                        {

                            if (this.gameObject.name.Contains("Collision basicattack"))
                            {

                                if (!this.gameObject.name.Contains("Collision NODAMAGE"))
                                {
                                    //damage is deactivated because we want to damage different from arrow hits!
                                   // if (dashability.orbCount == 0) { health.takeDamage(damage, damageType); }
                                   // if (dashability.orbCount == 1) { health.takeDamage(damage, damageType); }
                                   // if (dashability.orbCount == 2) { health.takeDamage(damage, damageType); }
                                   // if (dashability.orbCount == 3) { health.takeDamage(damage, damageType); }
                                }



                            }

                            if (!this.gameObject.name.Contains("Collision basicattack"))
                            {
                               // health.takeDamage(damage, damageType);
                            }

                        }

                        if (this.gameObject.name.Contains("Collision basicattack"))
                        {
                            //   if (dashability.orbCount == 0) { health.takeDamage(0, DamageTypes.Fire); }
                            if (SunShine.SunShineActive)
                            {
                             //   if (dashability.orbCount == 1) { health.takeDamage(10, DamageTypes.Elemental); }
                               // if (dashability.orbCount == 2) { health.takeDamage(20, DamageTypes.Elemental); }
                               // if (dashability.orbCount == 3) { health.takeDamage(30, DamageTypes.Elemental); }
                            }
                        }

                    }
                }



                ///////////////////////////////////////////////////////////////////////
                ///
                if (hit.collider.tag == "Trigger")
                {
                    turnoff = true;

                }
                //////////////////////////////////////////////////////////////////////////////////////////

                //Debug.Log("woot");
                isCollided = true;
                t.position = hit.point;
                oldPos = t.position;
                OnCollisionBehaviour(hit);
                OnCollisionDeactivateBehaviour(false);
                return;

            }
        }
        if (!isOutDistance && currentDistance + RayCastTolerance > Distance)
        {
            isOutDistance = true;
            OnCollisionDeactivateBehaviour(false);

            if (Target == null)
                t.localPosition = startPositionLocal + t.localRotation * (Vector3.forward + randomOffset) * Distance;
            else
            {
                var forwardVec = (targetT.position - t.position).normalized;
                t.position = startPosition + forwardVec * Distance;

            }
            oldPos = t.position;
            return;
        }

        t.position = oldPos + frameMoveOffsetWorld;
        oldPos = t.position;
    }

    Vector3 GetRadiusRandomVector()
    {
        var x = Time.time * RandomMoveSpeedScale + randomTimeOffset.x;
        var vecX = Mathf.Sin(x / 7 + Mathf.Cos(x / 2)) * Mathf.Cos(x / 5 + Mathf.Sin(x));

        x = Time.time * RandomMoveSpeedScale + randomTimeOffset.y;
        var vecY = Mathf.Cos(x / 8 + Mathf.Sin(x / 2)) * Mathf.Sin(Mathf.Sin(x / 1.2f) + x * 1.2f);

        x = Time.time * RandomMoveSpeedScale + randomTimeOffset.z;
        var vecZ = Mathf.Cos(x * 0.7f + Mathf.Cos(x * 0.5f)) * Mathf.Cos(Mathf.Sin(x * 0.8f) + x * 0.3f);


        return new Vector3(vecX, vecY, vecZ);
    }

    void OnCollisionBehaviour(RaycastHit hit)
    {
        var handler = CollisionEnter;
        if (handler != null)
            handler(this, new RFX1_CollisionInfo { Hit = hit });
        CollidedInstances.Clear();
        foreach (var effect in EffectsOnCollision)
        {
            var instance = Instantiate(effect, hit.point + hit.normal * CollisionOffset, new Quaternion()) as GameObject;
            CollidedInstances.Add(instance);
            if (HUE > -0.9f)
            {
                var color = instance.AddComponent<RFX1_EffectSettingColor>();
                var hsv = RFX1_ColorHelper.ColorToHSV(color.Color);
                hsv.H = HUE;
                color.Color = RFX1_ColorHelper.HSVToColor(hsv);
            }
            instance.transform.LookAt(hit.point + hit.normal + hit.normal * CollisionOffset);
            if (!CollisionEffectInWorldSpace) instance.transform.parent = transform;
            Destroy(instance, DestroyTimeDelay);
        }
    }

    void OnCollisionDeactivateBehaviour(bool active)
    {
        foreach (var effect in DeactivatedObjectsOnCollision)
        {
            if (effect != null) effect.SetActive(active);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            return;

        t = transform;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(t.position, t.position + t.forward * Distance);

    }

    public enum RFX4_SimulationSpace
    {
        Local,
        World
    }

    public class RFX1_CollisionInfo : EventArgs
    {
        public RaycastHit Hit;
    }
}
