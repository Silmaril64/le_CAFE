using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float t_start0 = 5f;
    float t_Start1 = 10f;
    float t_Start2 = 20f;
    float t_Start3 = 33f;
    float spd = 2f;
    float m_chrono = 0f;

    float start_y;

    // Start is called before the first frame update
    void Start()
    {
        start_y = 10f;
        transform.position = new Vector3(0, 10f, -10);
        //transform.position = new Vector3(0, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        m_chrono += Time.deltaTime;
        Debug.Log(m_chrono);
        if (m_chrono < t_start0)
        {
            transform.position = transform.position + new Vector3(0, -spd * Time.deltaTime, 0);
        }   
    }
}
