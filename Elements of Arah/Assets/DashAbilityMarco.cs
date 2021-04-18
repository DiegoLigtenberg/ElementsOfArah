using CreatingCharacters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

namespace CreatingCharacters.Abilities
{
    [RequireComponent(typeof(ThirdPersonMovement))]
    public class DashAbilityMarco : Ability
    {
        public Animator anim;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashDuration;
        private ThirdPersonMovement thirdPersonPlayer;



        private void Awake()
        {
            thirdPersonPlayer = GetComponent<ThirdPersonMovement>();
        }


        // Start is called before the first frame update
        void Start()
        {

  
        }

        public override void Cast()
        {
            StartCoroutine(Dash());
           
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                anim.SetTrigger("Teleport");
            }

           // Debug.Log(Camera.main.transform.eulerAngles);
        }

        public IEnumerator Dash()
        {
            thirdPersonPlayer.ResetImpactY();
            thirdPersonPlayer.gravity = 0;
         

            yield return new WaitForSeconds(0.5f);
            thirdPersonPlayer.gravity = thirdPersonPlayer.gravity *2 ;
            thirdPersonPlayer.gravity = -9.81f;

            float magnitude = 0;
            if (Camera.main.transform.eulerAngles.x < 58 && !thirdPersonPlayer.isGrounded)
            {
                thirdPersonPlayer.ResetImpactY();
                magnitude = Camera.main.transform.eulerAngles.x * 0.8f;                     //general movement in camera direction
                thirdPersonPlayer.AddForce(transform.forward.normalized, magnitude * -0.5f);  //move backwards relative to player
                thirdPersonPlayer.AddForce(Vector3.up, magnitude * -0.3f);                  //move upwards
            }
            thirdPersonPlayer.AddForce(Camera.main.transform.forward, -magnitude);
            yield return null;

        }

    }
}