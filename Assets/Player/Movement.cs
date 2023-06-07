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

    private bool squeezing;
    private float sqSpeed; //Speed before entering the squeeze zone

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

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Squeeze"))
        {
            float mult = coll.GetComponent<SqueezeCollider>().speedMulti;
            sqSpeed = speed;
            squeezing = true;

            speed = speed * mult;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Squeeze"))
        {
            speed = sqSpeed;
            squeezing = false;
        }
    }
}
