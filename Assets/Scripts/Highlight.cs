// Controls emission on gameobject
// Source: https://www.youtube.com/watch?v=pzaxC-P3sgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private Color colour = Color.red;

    private List<Material> materials;

    //Finds all materials on the gameobject and adds them to list
    void Awake()
    {
        materials = new List<Material>();
        foreach(var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }
    
    //If highlight on, enable emission and set colour
    public void ToggleHighlight(bool on)
    {
        if(on)
        {
            //Emission first, then colour
            foreach(var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", colour);
            }
        }
        else
        {
            //Disable emissions
            foreach(var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
