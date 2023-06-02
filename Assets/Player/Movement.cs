// Controls player movement
// source: https://www.youtube.com/watch?v=tXDgSGOEatk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    [SerializeField] float gravity = -30;
    [SerializeField] float jumpHeight = 2f;

    Vector2 horizontalInput;
    Vector3 verticalVelocity;
    public bool isGrounded;
    public bool jump;

    public void ReceiveInput (Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    void Update()
    {
        //Move player based on horizontal input
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);
        
        //Gravity exists
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
        
        //Check if on ground, reset fallspeed
        isGrounded = controller.isGrounded;
        if(isGrounded)
        {
            verticalVelocity.y = 0;
        }

        //Gives upward velocity when jump pressed && on ground
        if(jump)
        {
            if(isGrounded)
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
}
