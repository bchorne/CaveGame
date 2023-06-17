// Detects raycast hits on layermask from player camera
// Source: https://www.youtube.com/watch?v=pzaxC-P3sgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    [SerializeField] 
    private LayerMask pickableLayerMask;
    [SerializeField]
    private Transform playerCameraTransform;
    [SerializeField]
    private GameObject pickupUI;
    [SerializeField]
    private float hitRange = 3;

    private RaycastHit hit;

    [SerializeField]
    private Transform pickupParent;
    [SerializeField]
    private GameObject pickupItem;

    void Update()
    {
        Debug.DrawRay(
            playerCameraTransform.position,
            playerCameraTransform.forward * hitRange,
            Color.green);

        if(hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickupUI.SetActive(false);
        }
        //If raycast hits a pickup, highlight it and enable tooltip
        if(Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit, hitRange, pickableLayerMask
        ))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickupUI.SetActive(true);
        }
    }

    public void Interact()
    {
        if(hit.collider != null)
        {
            if(hit.collider.GetComponent<TorchPickup>())
            {
                Debug.Log("It torch");
                pickupItem = hit.collider.gameObject;

                pickupItem.transform.position = Vector3.zero;
                pickupItem.transform.rotation = Quaternion.identity;
                pickupItem.transform.SetParent(pickupParent.transform, false);
                pickupItem.layer = 8; //Give torch layer, for camera culling
                foreach (Transform child in pickupItem.transform) //Recursively apply the layer to the torch children
                {
                    child.gameObject.layer = 8;
                }
                return;
            }
        }
    }
}
