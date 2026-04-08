using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WendigoController : MonoBehaviour
{

    NavMeshAgent agent;
    Transform player;

    public Transform target;
    Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 8;
        player = ActivePlayerManager.ActivePlayerGameObj.transform;
        // target =   GameObject.Find("heraklios_a_dizon@Jumping (2)").transform;
        target = ActivePlayerManager.ActivePlayerGameObj.transform;


    }

    // Update is called once per frame
    void Update()
    {
        player = ActivePlayerManager.ActivePlayerGameObj.transform;

        SetTargetPosition(targetPlayer);
        //  agent.SetDestination(GameObject.Find("heraklios_a_dizon@Jumping (2)").transform.position);
        targetPlayer = ActivePlayerManager.ActivePlayerGameObj.transform; // PlayerManager.instance.player.transform;
        target = ActivePlayerManager.ActivePlayerGameObj.transform.Find("targetforBossWendigoAA").transform;
        FaceTarget();

    }

    public void SetTargetPosition(Transform newTarget)
    {
        target = newTarget;
    }

    public void FaceTarget()
    {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 500);
        transform.rotation = lookRotation;
    }
}

