//Controls inputs
//source: https://www.youtube.com/watch?v=tXDgSGOEatk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    Vector2 horizontalInput;
    PlayerControls controls;
    PlayerControls.MovementActions groundMovement;

    [SerializeField] MouseLook mouseLook;
    Vector2 mouseInput;

    // How to get input
    // Movement.[action].performed += context => do something

    void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.Movement;

        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }

    void OnEnable()
    {
        controls.Enable();
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnDisable()
    {
        controls.Disable();
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }
}
