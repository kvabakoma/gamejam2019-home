using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bone;
    public float Speed = 2;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;
    public float AirFriction = 0.5f;

    private Vector3 startRotation, moveDirection = Vector3.zero;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        Bone.transform.localEulerAngles = new Vector3();
        startRotation = Bone.transform.localEulerAngles;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Move();
    }

    void Aim() {
        float x = Input.GetAxis("RightStickX");
        float y = Input.GetAxis("RightStickY");
        if (x != 0.0f || y != 0.0f) {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            Vector3 controllerAngle = new Vector3( 
                angle + 180,
                startRotation.y,
                startRotation.z
            );
            Bone.transform.localEulerAngles = controllerAngle;
            // Debug.Log(Bone.transform.localEulerAngles);
        }

        // Debug.Log("H: " + Input.GetAxis("Horizontal") + " | V: " + Input.GetAxis("Vertical"));
        // Debug.Log("HR: " + Input.GetAxis("RightStickX") + " | VR: " + Input.GetAxis("RightStickY"));
    }

    void Move() {
        if (characterController.isGrounded) {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * Speed;

            if (Input.GetButton("Jump") || Input.GetAxis("JoystickJump") == 1)
            {
                moveDirection.y = JumpSpeed;
            }
        } else {
            moveDirection.x = Input.GetAxis("Horizontal") * Speed * .9f;
        }
        Debug.Log(Input.GetAxis("JoystickJump"));
        // Apply gravity
        moveDirection.y = moveDirection.y - (Gravity * Time.deltaTime);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);        
    }

    void Jump() {

    }
}
