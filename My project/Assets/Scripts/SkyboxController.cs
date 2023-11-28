using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material dayMaterial;
    public Material nightMaterial;

    public Light sunLight;

    void Update()
    {
        float lightRotation = sunLight.transform.rotation.eulerAngles.x;

        if (lightRotation < 270f)
        {
            RenderSettings.skybox = dayMaterial;
        }
        else
        {
            RenderSettings.skybox = nightMaterial;
        }
    }


}
