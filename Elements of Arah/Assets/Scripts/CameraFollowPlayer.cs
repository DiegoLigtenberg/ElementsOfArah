using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Player;
using Cinemachine;
using CreatingCharacters.Abilities;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField] private float lookSensitivty;
    [SerializeField] private float lookSmoothing;

    private Transform playerTransform;
    public Transform CorsairTransform;
    private Vector2 smoothedVelocity;

    private Vector2 currentLookingDirection;

    private CinemachineComposer composer;

    public Quaternion camRotation;

    [SerializeField] private ThirdPersonMovement thirdPersonPlayer;

    public GameObject archer_rot;
    public GameObject p_rot;

    private void Awake()
    {
        playerTransform = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camRotation.x = 0;


        archer_rot = GameObject.Find("archer rot");
        p_rot = GameObject.Find("archer@Standing Aim Recoil (2)");
    }



    private void Update()
    {

        if (!HealthPlayer.playerisdeath)
        {
            RotateCamera();
            //playerTransform.LookAt(GameObject.Find("Warrior Idle").transform);
        }
    }


    private void RotateCamera()
    {

        //rotation check
        if (ThirdPersonMovement.canmovecamera)
        {
            camRotation.x += Input.GetAxisRaw("Mouse Y");
            camRotation.y += Input.GetAxisRaw("Mouse X");
        }

        //if (!thirdPersonPlayer.isChargingDash)

        Vector2 cameraRotationInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));




        //1f als je wilt dat je omhoog kan springen!
        cameraRotationInput = Vector2.Scale(new Vector2(cameraRotationInput.x, 1f * cameraRotationInput.y), new Vector2(lookSensitivty * lookSmoothing, lookSensitivty * lookSmoothing));


        smoothedVelocity = Vector2.Lerp(smoothedVelocity, cameraRotationInput, 1 / lookSmoothing);

        currentLookingDirection += smoothedVelocity;


        //44 - 44
        currentLookingDirection.y = Mathf.Clamp(currentLookingDirection.y, -47, 32);


        transform.localRotation = Quaternion.AngleAxis(-currentLookingDirection.y, Vector3.right);
       
       // playerTransform.localRotation = Quaternion.AngleAxis(currentLookingDirection.x, playerTransform.up);
        if (Ability.globalCooldown <= 0)
        {
           p_rot.transform.localRotation =archer_rot.transform.localRotation;
           
          //  playerTransform.localRotation = Quaternion.AngleAxis(currentLookingDirection.x, playerTransform.up);
        }

        archer_rot.transform.localRotation = Quaternion.AngleAxis(currentLookingDirection.x, playerTransform.up);




        //volgt het blokje

        if (currentLookingDirection.y >= -15.7 && currentLookingDirection.y <= 23.7)
        {

            CorsairTransform.localRotation = Quaternion.AngleAxis(-currentLookingDirection.y, Vector3.right);
        }



        //IMPORTANT, THE Y VALUE IS HOW FAR YOU CAN GO UP AND DOWN
        // Debug.Log(currentLookingDirection);



    }
}


