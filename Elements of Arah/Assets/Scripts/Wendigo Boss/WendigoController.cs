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
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 8;
        player = GameObject.Find(ActivePlayerManager.ActivePlayerName).transform;
        // target =   GameObject.Find("heraklios_a_dizon@Jumping (2)").transform;

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find(ActivePlayerManager.ActivePlayerName).transform;

        SetTargetPosition(targetPlayer);
        //  agent.SetDestination(GameObject.Find("heraklios_a_dizon@Jumping (2)").transform.position);
        targetPlayer = PlayerManager.instance.player.transform;

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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        transform.rotation = lookRotation;
    }
}

