//Controls camera movement via mouse
//source: https://www.youtube.com/watch?v=tXDgSGOEatk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 50f;
    [SerializeField] float sensitivityY = 0.5f;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    public bool wiggle = false; //Returns true if mouse has moved horizontally since last frame

    void Update()
    {
        //Horizontal look
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        //Vertical look
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        float temp = mouseInput.x * sensitivityX;
        if (temp != mouseX)
        {
            //Mouse has moved horizontally since last frame
            wiggle = true;
            mouseX = temp;
        }
        else
        {
            wiggle = false; 
        }

        mouseY = mouseInput.y * sensitivityY;
    }
}
