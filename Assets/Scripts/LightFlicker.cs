//https://tutorials.daspete.at/unity3d/maze-runner/chapter-05
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float lightIntensity;
    public float flickerIntensity;

    public float lightTime;
    public float flickerTime;

    System.Random rg;

    Light flashlight;

    void Awake()
    {
        rg = new System.Random();
        flashlight = GetComponent<Light>();
    }

    void Start()
    {
        StartCoroutine(Flicker());
        Time.timeScale = 1.0f;
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            flashlight.intensity = lightIntensity;
            float lightingTime = lightTime + ((float)rg.NextDouble() - 0.5f);
            yield return new WaitForSeconds(lightingTime);

            int flickerCount = rg.Next(4, 9);

            for (int i = 0; i < flickerCount; i++)
            {
                float flickingIntensity = lightIntensity - ((float)rg.NextDouble() * flickerIntensity);
                flashlight.intensity = flickingIntensity;
                float flickingTime = (float)rg.NextDouble() * flickerTime;
                yield return new WaitForSeconds(flickingTime);
            }
        }
    }

}
