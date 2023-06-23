using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
   // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFade()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += (Time.deltaTime/3))
        {
            // set color with i as alpha
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
