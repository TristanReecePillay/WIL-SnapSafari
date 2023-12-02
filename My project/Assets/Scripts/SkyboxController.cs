using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material dayMaterial;
    //public Material morningMaterial;
    public Material nightMaterial;
    //public Material duskMaterial;

    public GameObject sunController;

    void Update()
    {
        if(sunController != null)
        {
            float lightRotation = sunController.transform.rotation.eulerAngles.x;

            if (lightRotation < 180f)
            {
                RenderSettings.skybox = dayMaterial;
            }
            else
            {
                RenderSettings.skybox = nightMaterial;
            }
        }
        
    }


}
