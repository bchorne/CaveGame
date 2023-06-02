//Controls inputs
//source: https://www.youtube.com/watch?v=tXDgSGOEatk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;

   PlayerControls controls;
   PlayerControls.MovementActions groundMovement;

   Vector2 horizontalInput;

    // How to get input
    // Movement.[action].performed += context => do something

    void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.Movement;
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        movement.ReceiveInput(horizontalInput);
    }
}
