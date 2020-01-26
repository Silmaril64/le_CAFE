using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        foreach (GameObject e in objs)
        {
            Destroy(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
