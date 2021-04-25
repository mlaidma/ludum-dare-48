using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{

    private const float minIntensity = 0.8f;
    private const float maxIntensity = 1f;

    [SerializeField] float minFlickerWait = 0.1f;
    [SerializeField] float maxFlickerWait = 0.15f;

    Light2D light2D;
    System.Random rng;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponentInChildren<Light2D>();
        rng = new System.Random();

        StartCoroutine(Flicker());
    }


    IEnumerator Flicker()
    {   
        while(true)
        {
            float flicker = Random.Range(minIntensity, maxIntensity);
            float wait = Random.Range(minFlickerWait, maxFlickerWait);

            light2D.intensity = flicker;

            yield return new WaitForSeconds(wait);
        }
    }
}
