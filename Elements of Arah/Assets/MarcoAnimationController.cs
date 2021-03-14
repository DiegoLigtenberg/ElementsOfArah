using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Player;
using CreatingCharacters.Abilities;

public class MarcoAnimationController : MonoBehaviour
{
    public Animator animator;
    private string currentState;
    public MarcoMovementController movement;

    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<MarcoMovementController>();
    }

    public IEnumerator castTime()
    {

        animator.SetBool("castTime", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("casted", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("castTime", false);
        animator.SetBool("casted", false);


    }


    public IEnumerator auto()
    {

        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("basicAttack");
        StartCoroutine(castTime());

        /*
        for (int i = 0; i < 1; i++)
        {
         
            yield return new WaitForSeconds(0.2f);
            animator.SetTrigger("basicAttack");
            StartCoroutine(castTime());
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(movement.velocity.x, 0 * movement.velocity.y, movement.velocity.z).magnitude;

        animator.SetFloat("velocityZ", Input.GetAxisRaw("Vertical"), 0.1f, Time.deltaTime);
        animator.SetFloat("velocityX", Input.GetAxisRaw("Horizontal"), 0.1f, Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(auto());

        }

        if (Input.GetKey(KeyCode.T))
        {
            animator.SetTrigger("test1");
        }


    }
}
