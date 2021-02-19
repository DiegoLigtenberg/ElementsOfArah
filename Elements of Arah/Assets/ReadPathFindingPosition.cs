using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;
public class ReadPathFindingPosition : MonoBehaviour
{
    public static Vector3 pathFindPos;
    public Transform player;
    public static float distancetoPlayer;

    public PathFindDestroy pfd;

    public GameObject[] effectMagnitude;
    public Transform[] effectTransformMagnitude;

    private float lastStep_1, timeBetweenSteps_1 = .08f;
    GameObject pathfind;
    // Start is called before the first frame update
    void Start()
    {
        ReadPathFindingPosition.distancetoPlayer = 1f;
    }



    private Vector3 lastpos1 = new Vector3(0,0,0);

    private Vector3 lastpos2 = new Vector3(0, 0, 0);

    private int i;
    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("heraklios_a_dizon@Jumping (2)/targetforBoss PathFind").transform;

        pathFindPos = new Vector3(transform.position.x, 65f, transform.position.z);

        distancetoPlayer = (pathFindPos - new Vector3(player.transform.position.x, 65, player.transform.position.z)).magnitude;

        // Debug.Log(pathFindPos);

        if (Time.time - lastStep_1 > timeBetweenSteps_1)
        {
            lastStep_1 = Time.time;

       
            lastpos2 = lastpos1;



            pathfind = Instantiate(effectMagnitude[0], ReadPathFindingPosition.pathFindPos, Quaternion.identity);


            lastpos1 = pathfind.transform.position;

            if (lastpos1 == lastpos2 && i != 0)
            {
                pfd.destroyme();
                //Debug.Log("it worked!!");
            }


            i++;


        }


        //was voor oude maar nu wil je niet dat weg gaat als je over jumped;
        if (ReadPathFindingPosition.distancetoPlayer < 0.52 && ReadPathFindingPosition.distancetoPlayer != 0)
        {
         //   pfd.destroyme();
        }
        else if (Ability.animationCooldown >= .8f && ReadPathFindingPosition.distancetoPlayer < 0.8f && ReadPathFindingPosition.distancetoPlayer != 0)
        {
            pfd.destroyme();
        }


      //  Debug.Log(distancetoPlayer);

    }
}
