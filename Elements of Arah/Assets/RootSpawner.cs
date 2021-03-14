using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CreatingCharacters.Abilities;
using Random = UnityEngine.Random;
using CreatingCharacters.Abilities;

public class RootSpawner : MonoBehaviour
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
    public PathFindDestroy pfd;

    public static float rootTimeSmoke;
    public float setRootTimeSmoke;

    // Start is called before the first frame update
    void Start()
    {
        rootTimeSmoke = RFX1_TransformMotionPathFinderToPlayer.rootTimeSmoke;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool onlyonce;
    public IEnumerator delayedAnimation()
    {
        onlyonce = true;
        yield return new WaitForSeconds(0.1f);

        foreach (var effect in EffectsOnCollision)
        {
            // var instance = Instantiate(effect, hit.point + hit.normal * CollisionOffset, new Quaternion()) as GameObject;
            var instance = Instantiate(effect, GameObject.Find(ActivePlayerManager.ActivePlayerName).transform.position, GameObject.Find(ActivePlayerManager.ActivePlayerName).transform.rotation) as GameObject;
            Ability.animationCooldown = rootTimeSmoke;
            pfd.destroyme();
            CollidedInstances.Add(instance);
            if (HUE > -0.9f)
            {
                var color = instance.AddComponent<RFX1_EffectSettingColor>();
                var hsv = RFX1_ColorHelper.ColorToHSV(color.Color);
                hsv.H = HUE;
                color.Color = RFX1_ColorHelper.HSVToColor(hsv);
            }
            //  instance.transform.LookAt(hit.point + hit.normal + hit.normal * CollisionOffset);

            instance.transform.LookAt(GameObject.Find(ActivePlayerManager.ActivePlayerName).transform);
            if (!CollisionEffectInWorldSpace) instance.transform.parent = transform;
            Destroy(instance, DestroyTimeDelay);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerPhasingTrigger") && !AvatarMoveLocalPosUp.isRooted && !onlyonce)
        {



            StartCoroutine(delayedAnimation());

        }
    }

    public class RFX1_CollisionInfo : EventArgs
    {
        public RaycastHit Hit;
    }
}
