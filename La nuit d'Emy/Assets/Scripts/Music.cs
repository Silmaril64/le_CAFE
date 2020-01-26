using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip song;
    float loop_time = 44.0f;
    float loop_duration = 32.0f;
    bool switched = false;
    // Start is called before the first frame update
    // Checking if going over Sample where looping occurs each frame
    void Update()
    {
        if (AS.timeSamples >= loop_time * song.frequency)
        {
            AS.timeSamples -= Mathf.RoundToInt(loop_duration * song.frequency);
        }
    }

    // Update is called once per frame
    void Start()
    {
        AS = GetComponent<AudioSource>();
        song = AS.clip;
        AS.Play();
    }
}