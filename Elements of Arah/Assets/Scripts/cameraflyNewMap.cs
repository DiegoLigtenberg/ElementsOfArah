using UnityEngine;

public class cameraflyNewMap : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float sprintMultiplier = 2.5f;
    [SerializeField] float verticalSpeed = 6f;

    [Header("Look")]
    [SerializeField] float lookSensitivity = 2f;
    [SerializeField] bool holdRightMouseToLook = true;

    float pitch;

    void Update()
    {
        HandleLook();
        HandleMove();
    }

    void HandleLook()
    {
        if (holdRightMouseToLook && !Input.GetMouseButton(1))
            return;

        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y + mouseX, 0f);
    }

    void HandleMove()
    {
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);

        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            move += transform.forward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            move -= transform.forward;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            move += transform.right;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            move -= transform.right;
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
            move += Vector3.up;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftControl))
            move -= Vector3.up;

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        transform.position += move * speed * Time.deltaTime;
    }
}
