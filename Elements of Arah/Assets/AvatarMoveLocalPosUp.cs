using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMoveLocalPosUp : MonoBehaviour
{
    public Transform tf;
    private float x;

    private bool goingup;
    private bool goingdown;
    public static bool isRooted;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        isRooted = false;
        StartCoroutine(timerup());
        x = 0.02f;
    }

    public IEnumerator timerup()
    {
        isRooted = true;
    
        yield return new WaitForSeconds(RFX1_TransformMotionPathFinderToPlayer.rootTimeSmoke);
        isRooted = false;
        goingdown = true;
    }

    public static IEnumerator manual_root(float root_duration)
    {
        isRooted = true;

        yield return new WaitForSeconds(root_duration);
        isRooted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tf.localPosition.y < 1.118788 && !goingdown)
        {
            x = x + 0.001f;
            transform.localPosition = new Vector3(0.8015f, transform.localPosition.y  +x, 0.59f);
        }

        if (goingdown)
        {

            x = x + 0.001f;
            transform.localPosition = new Vector3(0.801f, transform.localPosition.y - x, 0.59f);
        }
    }
}
