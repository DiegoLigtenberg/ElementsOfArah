using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CreatingCharacters.Abilities;
using Random = UnityEngine.Random;



public class RFX1_TransformMotionBoss01Lock : MonoBehaviour
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

    [SerializeField] private int damage = 1;
    [SerializeField] private DamageTypes damageType;

    float lastStep, timeBetweenSteps = .001f;
    Vector3 forwardVec;

    //maak hier een list van -> loop over alle damage en kijk welke je wilt
    //[SerializeField] private DealDamage dealDamage;
    //[SerializeField]  private BasicAttack basicAttackDamage;
    private void Awake()
    {
        //Target = GameObject.Find("heraklios_a_dizon@Jumping (2)");
        Target = GameObject.Find(ActivePlayerManager.ActivePlayerName + "/targetforBoss01");


        // DeactivatedObjectsOnCollision[0] = GameObject.Find("Enemy (1)");
        //DeactivatedObjectsOnCollision[1] = GameObject.Find("Cube(1)");

    }


    void Start()
    {



        t = transform;
        if (Target != null) targetT = Target.transform;
        startQuaternion = t.rotation;
        startPositionLocal = t.localPosition;
        startPosition = t.position;
        oldPos = t.TransformPoint(startPositionLocal);
        Initialize();
        isInitialized = true;


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
    }

    void Update()
    {


        if (!dropFirstFrameForFixUnityBugWithParticles)
        {
            UpdateWorldPosition();
        }
        else dropFirstFrameForFixUnityBugWithParticles = false;
    }

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
                var fade = Vector3.Distance(t.position, targetT.position) / Vector3.Distance(startPosition, targetT.position);
                randomOffset *= fade;
            }
        }

        var frameMoveOffset = Vector3.zero;
        var frameMoveOffsetWorld = Vector3.zero;
        if (!isCollided && !isOutDistance)
        {
            //currentSpeed = Mathf.Clamp(currentSpeed - Speed*Dampeen*Time.deltaTime, MinSpeed, Speed);
            if (Target == null)
            {
                var currentForwardVector = (Vector3.forward + randomOffset) * Speed * Time.deltaTime;
                frameMoveOffset = t.localRotation * currentForwardVector;
                frameMoveOffsetWorld = startQuaternion * currentForwardVector;
            }
            else
            {

                if (Time.time - lastStep > timeBetweenSteps)
                {
                    timeBetweenSteps = timeBetweenSteps + 5.41f;
                    lastStep = Time.time;
                    forwardVec = (targetT.position - t.position).normalized;
                }

                var currentForwardVector = (forwardVec + randomOffset) * Speed * Time.deltaTime;

                //richt meer omhoog
                currentForwardVector = new Vector3(currentForwardVector.x, currentForwardVector.y + 0.002f, currentForwardVector.z);

                frameMoveOffset = currentForwardVector;
                frameMoveOffsetWorld = currentForwardVector;

            }
        }

        var currentDistance = (t.localPosition + frameMoveOffset - startPositionLocal).magnitude;
        Debug.DrawRay(t.position, frameMoveOffsetWorld.normalized * (Distance - currentDistance), Color.red, 2f);
        // Debug.DrawLine(t.position, frameMoveOffsetWorld.normalized * (Distance - currentDistance) * 100,Color.red,Mathf.Infinity);



        RaycastHit hit;
        if (!isCollided && Physics.SphereCast(t.position, 0.375f, frameMoveOffsetWorld.normalized, out hit, Distance, CollidesWith))
        {


            if (frameMoveOffset.magnitude + RayCastTolerance > hit.distance)
            {
                if (hit.collider.GetComponent<HealthPlayer>() != null)
                {
                    var health = hit.collider.GetComponent<HealthPlayer>();

                    if (health != null)
                    {
                        Debug.Log("dealt " + damage + " damage");
                        health.takeDamage(damage, damageType);
                    }
                }

                ///////////////////////////////////////////////////////////////////////
                timeBetweenSteps = 0.1f;
                Debug.Log("woot");
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
