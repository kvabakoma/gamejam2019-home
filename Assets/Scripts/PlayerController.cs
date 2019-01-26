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
    public Animator animController;

    private Vector3 startRotation, moveDirection = Vector3.zero;
    private CharacterController characterController;
    private bool playerIsLookingLeft = true;
    

    // Start is called before the first frame update
    void Start()
    {
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
            // Debug.Log(Bone.transform.eulerAngles + " | " + Bone.transform.localEulerAngles);
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

            animController.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); 
        } else {
            // movement in-air
            moveDirection.x = Input.GetAxis("Horizontal") * speed * .9f;
            animController.SetFloat("Speed", 0.0f); 
        }
        
        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);          
    }

    void Flip() {
        // Debug.Log(Input.GetAxis("RightStickX"));
        if (Input.GetAxis("RightStickX") > 0 && playerIsLookingLeft) {
            transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            playerIsLookingLeft = !playerIsLookingLeft;
        } else if (Input.GetAxis("RightStickX") < 0 && !playerIsLookingLeft) {
            transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            playerIsLookingLeft = !playerIsLookingLeft;
        }
    }

    // void MyDebugStuff() {
    //     Debug.Log("----- START");
    //     transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    //     playerIsLookingLeft = !playerIsLookingLeft;
    //     Debug.Log("----- END");

    //     Invoke("MyDebugStuff", 10f);
    // }
}
