using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Abilities;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class AE_BowString : MonoBehaviour
{
    public float StringTint = 0.01f;
    public Transform Point1;
    public Transform Point2;
    public Transform HandBone;
    public bool InHand;
    public GameObject[] prefab;
    public GameObject bow;
    public GameObject arrow;
    public GameObject bowhand;
    public GameObject otherhand;
    public Transform curCamTransform;

    LineRenderer lineRenderer;
    Vector3 prevHandPos;
    const float tensionTime = 0.03f;
    float currentTensionTime;
    public Animator animator;
    // Use this for initialization
    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.widthMultiplier = StringTint;

        if (Point1 == null || Point2 == null || HandBone == null) return;

        lineRenderer.positionCount = 3;
        prevHandPos = (Point1.position + Point2.position) / 2;
        lineRenderer.SetPosition(0, Point1.position);
        lineRenderer.SetPosition(1, prevHandPos);
        lineRenderer.SetPosition(2, Point2.position);
        animator = GetComponent<Animator>();
    }

    public void BowEvent()
    {
        InHand = true;
        //GENIUS MOVE TO RIVEN AA STYLE
        // yield return new WaitForSeconds(0.2f);
        if (Ability.globalCooldown <= 0.6f)
        {

            Ability.globalCooldown = 0.6f;
        }

    }

    public void stopBowEvent()
    {
        Instantiate(prefab[1], otherhand.transform.position, curCamTransform.transform.rotation);
        animator.SetTrigger("fallBack");
        InHand = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Point1 == null || Point2 == null || HandBone == null) return;

        lineRenderer.widthMultiplier = StringTint;

        if (InHand)
        {
            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(0, Point1.position);
            lineRenderer.SetPosition(1, HandBone.position);
            lineRenderer.SetPosition(2, Point2.position);
            currentTensionTime = 0;
            prevHandPos = HandBone.position;

        }
        else
        {
            currentTensionTime += Time.deltaTime;
            lineRenderer.positionCount = 3;
            var defaultPos = (Point1.position + Point2.position) / 2;
            lineRenderer.SetPosition(0, Point1.position);
            lineRenderer.SetPosition(1, Vector3.Lerp(prevHandPos, defaultPos, Mathf.Clamp01(currentTensionTime / tensionTime)));
            lineRenderer.SetPosition(2, Point2.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //tartCoroutine(delayaa());
            //   Instantiate(prefab[0], otherhand.transform.position, otherhand.transform.rotation); //curcamtransform for clones

        }
    }




    #region //make smooth
    //make smooth line renderer
    private void LateUpdate()
    {
        if (Point1 == null || Point2 == null || HandBone == null) return;

        lineRenderer.widthMultiplier = StringTint;

        if (InHand)
        {
            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(0, Point1.position);
            lineRenderer.SetPosition(1, HandBone.position);
            lineRenderer.SetPosition(2, Point2.position);
            currentTensionTime = 0;
            prevHandPos = HandBone.position;

        }
        else
        {
            currentTensionTime += Time.deltaTime;
            lineRenderer.positionCount = 3;
            var defaultPos = (Point1.position + Point2.position) / 2;
            lineRenderer.SetPosition(0, Point1.position);
            lineRenderer.SetPosition(1, Vector3.Lerp(prevHandPos, defaultPos, Mathf.Clamp01(currentTensionTime / tensionTime)));
            lineRenderer.SetPosition(2, Point2.position);

        }
    }

    private void FixedUpdate()
    {
        if (Point1 == null || Point2 == null || HandBone == null) return;

        lineRenderer.widthMultiplier = StringTint;

        if (InHand)
        {
            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(0, Point1.position);
            lineRenderer.SetPosition(1, HandBone.position);
            lineRenderer.SetPosition(2, Point2.position);
            currentTensionTime = 0;
            prevHandPos = HandBone.position;

        }
        else
        {
            currentTensionTime += Time.deltaTime;
            lineRenderer.positionCount = 3;
            var defaultPos = (Point1.position + Point2.position) / 2;
            lineRenderer.SetPosition(0, Point1.position);
            lineRenderer.SetPosition(1, Vector3.Lerp(prevHandPos, defaultPos, Mathf.Clamp01(currentTensionTime / tensionTime)));
            lineRenderer.SetPosition(2, Point2.position);
        }


    }
    #endregion
}
