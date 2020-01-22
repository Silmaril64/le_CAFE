using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmiScript : MonoBehaviour
{
    float t_start0 = 5f;
    float t_Start1 = 10f;
    float t_Start2 = 20f;
    float t_Start3 = 33f;


    float m_chrono;
    Rigidbody2D body;
    float spd;
    bool hasJumped;
    float timeStart = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        m_chrono = 0f;
        body.gravityScale = 1f;
        spd = 2f;
        hasJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_chrono += Time.deltaTime;
        if (m_chrono > 12.5f + t_Start1 + timeStart && !hasJumped)
        {
            body.AddForce(new Vector2(0,4f), ForceMode2D.Impulse);
            hasJumped = true;
        }
        else if (m_chrono > 12 + timeStart + t_Start1)
        {
            transform.position = body.position + new Vector2(4*spd * Time.deltaTime, 0);
        }
        if (m_chrono > 9 + timeStart + t_Start1)
        {
            transform.position = body.position + new Vector2(spd * Time.deltaTime, 0);
        }
        else if (m_chrono > 8 + timeStart + t_Start1)
        {

        }
        else if (m_chrono > 3 + timeStart + t_Start1)
        { }
        else if (m_chrono > timeStart + t_Start1)
            transform.position = body.position + new Vector2(spd * Time.deltaTime, 0);
        else if (m_chrono > t_Start1 + 3);
        else if (m_chrono > t_Start1)
        {
            transform.position = body.position + new Vector2(spd * Time.deltaTime, 0);
        }
    }
}
