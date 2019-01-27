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
    public GameObject Canvas;
    public bool playerIsLookingLeft = true, canJump, playerIsAlive = true;

    private Vector3 startRotation, moveDirection = Vector3.zero;
    private CharacterController characterController;
    

    // Start is called before the first frame update
    void Start()
    {
        startRotation = Bone.transform.localEulerAngles;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAlive) {
            Aim();
            Move();
            Flip();
        } else {
            DefeathScene();
        }
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
            
            if (animController.GetBool("Grounded") == false) {  
                animController.SetBool("Grounded", true); 
            }
            // We are grounded, so recalculate
            // move direction directly from axes

            // canJump = true;

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            animController.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); 

            if (Input.GetButton("Jump") || Input.GetAxis("JoystickJump") == 1) {
                moveDirection.y = jumpSpeed;
                // canJump = false;
                // Invoke("SetCanJumpToTrue",.2f);
            }
            
        } else {
            if (animController.GetBool("Grounded") == true) {  
                Debug.Log("Start Jump");              
                animController.SetBool("Grounded", false); 
            }
            // movement in-air
            moveDirection.x = Input.GetAxis("Horizontal") * speed * .9f;
            animController.SetFloat("Speed", 0.0f); 
            /* if (canJump) {
                 if (Input.GetButton("Jump") || Input.GetAxis("JoystickJump") == 1) {
                    Debug.Log("Double Jump");
                    moveDirection.y += jumpSpeed;
                }
                canJump = false;
            } */
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
    
    void SetCanJumpToTrue() {
        canJump = true;
    }

    public void Defeath() {
        animController.SetTrigger("Defeath");
        animController.SetBool("IsDefeathed", true);
        playerIsAlive = false;
        // Canvas.SetActive(true);
        // Debug.Log("Sammy Lost");
    }

    private void DefeathScene() {
        if (!characterController.isGrounded) {
            characterController.Move(new Vector3(moveDirection.x, -gravity, 0) * Time.deltaTime);
        }
    }

    
}
