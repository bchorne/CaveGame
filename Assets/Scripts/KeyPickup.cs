using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPickup : MonoBehaviour
{
    public Pickup pickup;
    public GameObject dummyValve;
    public GameObject realValve;
    public TextMeshProUGUI collectedText;

    public void Pickup()
    {
        Debug.Log("Key script");
        pickup.PlayerKeys++;
        collectedText.text = pickup.PlayerKeys + " / 3";
        if(pickup.PlayerKeys == 3)
        {
            dummyValve.SetActive(false);
            realValve.SetActive(true);
        }
        Destroy(gameObject);
    }
}
