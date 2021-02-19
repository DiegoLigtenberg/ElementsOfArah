using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WendigoAbilities : MonoBehaviour
{

    //INSTANTIATE
    private WendigoController wendigoMovement;
    public Animator anim;
    public NavMeshAgent agent;

    public GameObject[] effectP1;
    public Transform[] effectTransformP1;
    public GameObject[] particleEffectP1;
    public GameObject[] platformPositionsP1;
    public ParticleSystem particlesystemP1;
    public MeshRenderer staffOrbP1;
    public MeshCollider meshColP1;
    public SphereCollider sphereColP1;
    public int platformcount;

    public GameObject[] effectP2;
    public Transform[] effectTransformP2;

    public GameObject[] effectP22;
    public Transform[] effectTransformP22;

    public GameObject[] effectP3;
    public Transform[] effectTransformP3;

    public GameObject[] effectMagnitude;
    public Transform[] effectTransformMagnitude;

    public GameObject[] effectTransition;
    public Transform[] effectTransformTransition;

    public Rigidbody rb;
    public Vector3 PlayerPositionSpawnPosition;

    public static bool isaaing = false;

    public HealthPlayer hp;
    public Health hpm;
    public static int amntMinions;
    private bool minionhasspawned;

    // Start is called before the first frame update
    void Start()
    {
        wendigoMovement = GetComponent<WendigoController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startSpinVanish_P1()
    {
        StartCoroutine(SpinVanish_P1());
    }

    public void startBasicAttack_P1()
    {
        StartCoroutine(BasicAttack_P1());
    }

    /// ///////////////////////////////////////////////////////////////////////////

    public IEnumerator BasicAttack_P1()
    {

        yield return new WaitForSeconds(.2f);
        
        yield return new WaitForSeconds(.2f);
        particleEffectP1[0].gameObject.SetActive(false);
        particleEffectP1[1].gameObject.SetActive(true);

        yield return new WaitForSeconds(.1f);
        
        yield return new WaitForSeconds(.05f);
        Vector3 position = effectTransformP1[0].position;
        yield return new WaitForSeconds(.05f);
        Instantiate(effectP1[0], position, effectTransformP1[0].rotation);


        yield return new WaitForSeconds(1.5f);
        particleEffectP1[0].gameObject.SetActive(true);
        particleEffectP1[1].gameObject.SetActive(false);

        yield return null;
    }


    public IEnumerator skullDelayVanish()
    {
        Vanish[] vanishcomponents2 = GetComponentsInChildren<Vanish>();

        yield return new WaitForSeconds(1f);
        int b = 0;
        foreach (Vanish vanish in vanishcomponents2)
        {
            if (b == 3 || b == 4 || b == 5)
            {
                vanish.NextMaterial();
                vanish.startVanishing();
            }
            b += 1;
        }
    }
    
    private IEnumerator SpinVanish_P1()
    {
        platformcount++;
        int randomplatform = Random.Range(0, 5);

        //change particle to big
        particleEffectP1[0].gameObject.SetActive(false);
        particleEffectP1[1].gameObject.SetActive(true);

        //start vanishing after 1 second delay
        yield return new WaitForSeconds(0.5f);
        Vanish[] vanishcomponents = GetComponentsInChildren<Vanish>();
        int i = 0;
        //Debug.Log(vanishcomponents.Length);
        foreach (Vanish vanish in vanishcomponents)
        {
            Debug.Log(vanish.gameObject.name);
            if (i == 0 || i == 1 || i == 2 || i == 6)
            {
                vanish.NextMaterial();
                vanish.startVanishing();
            }
            else
            {
                StartCoroutine(skullDelayVanish());
            }
            i += 1;
        }
        //put off navmeshagent to teleport and remove orb
        yield return new WaitForSeconds(.4f);
        agent.enabled = false;
        staffOrbP1.enabled = false;
        meshColP1.enabled = false;
        sphereColP1.enabled = false;

        //stop emitting particles - smooth stop

        // particlesystemP1.enableEmission = false;

        //yield return new WaitForSeconds(1.0f);


        //remove particle system and teleport
        if (platformcount <= 3)
        {
            this.transform.position = platformPositionsP1[randomplatform].transform.position;
        }
        else
        {
            this.transform.position = platformPositionsP1[5].transform.position;
            platformcount = 0;
        }
       
        yield return new WaitForSeconds(3.3f);

        //particleEffectP1[0].gameObject.SetActive(false);
        // particleEffectP1[1].gameObject.SetActive(false);
       // this.transform.position = platformPositionsP1[randomplatform].transform.position; //the old spot


        //start particle system
        yield return new WaitForSeconds(0.9f);
        staffOrbP1.enabled = true;
        meshColP1.enabled = true;
        sphereColP1.enabled = true;
        particlesystemP1.enableEmission = true;
       
        particleEffectP1[0].gameObject.SetActive(true);
        particleEffectP1[1].gameObject.SetActive(false);


        //agent.enabled = true;


        yield return null;
    }



}