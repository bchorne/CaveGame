using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public Pickup pickup;
    public AN_Button aN_Button;
    public Image image;

    public void Pickup()
    {
        image.color = new Color(255, 255, 255, 255);
        Debug.Log("Key script");
        pickup.PlayerKeys++;
        if(pickup.PlayerKeys == 4)
        {
            aN_Button.Locked = false;
        }
        Destroy(gameObject);
    }
}
