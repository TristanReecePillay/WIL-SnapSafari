using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurfaceEffect : MonoBehaviour
{
    public float reducedSpeed = 0.5f; // Adjust this value to control the reduction in speed.

    public FPSController playerController;

    public bool isInWater = false;

    public GameObject SwimSteps;


    void Start()
    {
        SwimSteps.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterDepth")) // Change "WaterSurface" to the appropriate tag of the invisible plane.
        {
            isInWater = true;
            ReduceMovementSpeed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WaterDepth"))
        {
            isInWater = false;
            RestoreMovementSpeed();
        }
    }

    private void ReduceMovementSpeed()
    {
        // Reduce the player's movement speed when they are in the water.
       if(playerController != null)
       {
            playerController.moveSpeed *= reducedSpeed;
          

        }

    }

    private void Update()
    {
        if (isInWater)
        {
            if (Input.GetKey("w"))
            {
                StartSwimSteps();
            }

            if (Input.GetKeyDown("s"))
            {
                StartSwimSteps();
            }

            if (Input.GetKeyDown("a"))
            {
                StartSwimSteps();
            }

            if (Input.GetKeyDown("d"))
            {
                StartSwimSteps();
            }

            if (Input.GetKeyUp("w"))
            {
                StopSwimSteps();
            }

            if (Input.GetKeyUp("s"))
            {
                StopSwimSteps();
            }

            if (Input.GetKeyUp("a"))
            {
                StopSwimSteps();
            }

            if (Input.GetKeyUp("d"))
            {
                StopSwimSteps();
            }
        }
        
    }
    private void RestoreMovementSpeed()
    {
        // Restore the player's movement speed when they exit the water.
        if (playerController != null)
        {
            playerController.moveSpeed /= reducedSpeed;
            StopSwimSteps();

        }
         


    }

    void StartSwimSteps()
    {
        SwimSteps.SetActive(true);
    }

    void StopSwimSteps()
    {
        SwimSteps.SetActive(false);
    }
}
