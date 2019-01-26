using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bone;
    public float speed = 2;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float bodyAdditionalAngleLeft, bodyAdditionalAngleRight;

    private Vector3 startRotation, moveDirection = Vector3.zero;
    private CharacterController characterController;
    private bool playerIsLookingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        // Bone.transform.localEulerAngles = new Vector3(0, Bone.transform.localEulerAngles.y, Bone.transform.localEulerAngles.z);
        startRotation = Bone.transform.localEulerAngles;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Move();
        Flip();
    }

    void Aim() {
        float x = Input.GetAxis("RightStickX");
        float y = Input.GetAxis("RightStickY");
        if (x != 0.0f || y != 0.0f) {
            /*
            if (!playerIsLookingLeft) angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;            
            
            Vector3 controllerAngle = new Vector3( 
                angle + BodyAdditionalAngle,
                // angle + 180,
                startRotation.y,
                startRotation.z
            ); */
            Vector3 controllerAngle;

            if (playerIsLookingLeft) {
                float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;   
                    controllerAngle = new Vector3( 
                    angle + bodyAdditionalAngleLeft,
                    startRotation.y,
                    startRotation.z
                );
            } else {
                float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
                    controllerAngle = new Vector3( 
                    angle - bodyAdditionalAngleRight,
                    startRotation.y,
                    startRotation.z
                );
            }
            Bone.transform.localEulerAngles = controllerAngle;
            Debug.Log(Bone.transform.eulerAngles);
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
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump") || Input.GetAxis("JoystickJump") == 1)
            {
                moveDirection.y = jumpSpeed;
            }
        } else {
            // movement in-air
            moveDirection.x = Input.GetAxis("Horizontal") * speed * .9f;
        }
        
        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);        
    }

    void Flip() {
        if (transform.localScale.x > 0 && moveDirection.x > 0) {
            transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            Debug.Log("turn right");
            playerIsLookingLeft = false;
        } else if (transform.localScale.x < 0 && moveDirection.x < 0) {
            transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            Debug.Log("turn left");
            playerIsLookingLeft = true;
        }
    }
}
