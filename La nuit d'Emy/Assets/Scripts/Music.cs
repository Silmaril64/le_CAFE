using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource AS;
    // Start is called before the first frame update
    void Update()
    {
        
    }

    // Update is called once per frame
    void Start()
    {
        AS = GetComponent<AudioSource>();
        AS.PlayDelayed( 12.0f);
    }
}