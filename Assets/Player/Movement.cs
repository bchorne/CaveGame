// Controls player movement
// source: https://www.youtube.com/watch?v=tXDgSGOEatk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] MouseLook mouse;
    [SerializeField] float speed = 11f;
    private float storedSpeed = 0f;
    Vector2 horizontalInput;

    [SerializeField] float gravity = -30;
    [SerializeField] float jumpHeight = 2f;
    Vector3 verticalVelocity;
    public bool isGrounded;
    private float groundedTimer;
    public bool jump;

    public GameObject player;
    private bool forceCrouch;
    private bool isCrouching;
    private float crouchSpeed;

    private bool squeezing;
    private float sqSpeed; //Speed before entering the squeeze zone
    public bool isMoving = false;
    private Vector3 pos;

    void Start()
    {
        crouchSpeed = speed * 0.5f;
    }
    
    public void ReceiveInput (Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    void Update()
    {
        //Check Squeezing

        if(squeezing && !mouse.wiggle && speed != 0)
        {
            storedSpeed = speed;
            speed = 0; //Halt player movement unless mouse has moved horizontally since last frame while in a Squeeze space
        }
        else if (mouse.wiggle && squeezing)
        {
            speed = storedSpeed;
        }

        if (pos != player.transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        pos = player.transform.position;

        //Move player based on horizontal input
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);
        
        //Gravity exists
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
        
        //Check if on ground, reset fallspeed to avoid falling faster than the speed of light
        //groundedTimer allows for jumping when going downhill
        isGrounded = controller.isGrounded;
        if(isGrounded)
        {
            groundedTimer = 0.3f;
            verticalVelocity.y = 0;
        }
        if(groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        //Gives upward velocity when jump pressed && on ground
        if(jump)
        {
            if(groundedTimer > 0)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    public void OnCrouchPressed()
    {
        //check first whether we are in a force crouch zone
        if(!forceCrouch || (forceCrouch && !isCrouching))
        {
            isCrouching = !isCrouching;
            CrouchToggle(isCrouching);
        }
    }

    //Reduce player height to simulate crouching
    void CrouchToggle(bool crouching)
    {
        if(crouching)
        {
            controller.height = 1;
            storedSpeed = speed;
            speed = crouchSpeed;
        }
        if(!crouching)
        {
            controller.height = 2;
            speed = storedSpeed;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Squeeze"))
        {
            float mult = coll.GetComponent<SqueezeCollider>().speedMulti;
            sqSpeed = speed;
            squeezing = true;

            speed = speed * mult;
        }
        
        //If in a force crouch zone, toggle crouch mode on
        if(coll.CompareTag("Crouch"))
        {
            //CrouchToggle(true);
            forceCrouch = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Squeeze"))
        {
            speed = sqSpeed;
            squeezing = false;
        }

        //When leaving a force crouch zone, toggle crouch mode off
        if(coll.CompareTag("Crouch"))
        {
            forceCrouch = false;
            if(isCrouching)
            {
                CrouchToggle(false);
                isCrouching = false;
            }
        }
    }
}
