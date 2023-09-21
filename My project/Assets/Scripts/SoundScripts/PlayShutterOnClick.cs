using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShutterOnClick : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            source.PlayOneShot(clip);
        }
    }
}
