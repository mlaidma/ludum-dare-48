using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

public class PlayerItem : MonoBehaviour
{

    [SerializeField] float maxIntensity = 1f;

    private Light2D itemLight;


    // Start is called before the first frame update
    void Start()
    {
        itemLight = GetComponentInChildren<Light2D>();        
    }
    

    public void SetLight(float intensity)
    {

        if(itemLight == null)
        {
            Debug.Log("No ItemLight");
            return;
        }

        itemLight.intensity = intensity * maxIntensity;
    }
}
