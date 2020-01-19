using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBehavior : MonoBehaviour
{
    float t_start0 = 5f;
    float t_Start1 = 10f;
    float t_Start2 = 20f;
    float t_Start3 = 33f;

    Rigidbody2D m_body;
    float m_chrono;
    Vector2 rotationCenter;
    float ray;
    float angle;
    float angularSpeed;
    float rotDir;
    int state;
    float spd;

    float start1_x = -1.10f;
    float start1_y = 1.10f;

    float start_x = -21.10f;
    float start_y = 1.10f;

    float posX, posY;
    // Start is called before the first frame update
    void Start()
    {
        spd = 4f; //anglspd / 2pi * perim;
        ray = 2f;
        angularSpeed = 2f;
        angle = 0f;
        rotDir = 1f;
        state = 0;

        transform.position = new Vector2(-21.10f, 1.10f);


        m_body = GetComponent<Rigidbody2D>();
        m_chrono = 0f;
        posX = start1_x;
        posY = start1_y;
        rotationCenter = new Vector3(start1_x - 2f, start1_y, 0);

        m_body.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_chrono += Time.deltaTime;
        if (m_chrono < 5f) { }
        else if (m_chrono > 5f && m_chrono < 10f)
        {
            transform.position = transform.position + new Vector3(spd * Time.deltaTime, 0, 0);
        }
        else
        {
            posX = rotationCenter.x + Mathf.Cos(angle) * ray;
            posY = rotationCenter.y + Mathf.Sin(angle) * ray;

            transform.position = new Vector2(posX, posY);

            angle += rotDir * Time.deltaTime * angularSpeed;

            if (angle > 4f * Mathf.PI && state == 0)
            {
                state = 1;
                angle = Mathf.PI;
                rotationCenter += new Vector2(2f * ray, 0);
                rotDir = -1f;
            }

            else if (angle < -4f * Mathf.PI && state == 1)
            {
                rotationCenter += new Vector2(1.5f * ray, 0);
                angularSpeed = angularSpeed * 2 * Mathf.PI * ray / (2 * Mathf.PI * ray * 0.5f);
                ray = 0.5f * ray;
                rotDir = 1f;
                angle = Mathf.PI;
                state = 2;
            }

            else if (angle > 4f * Mathf.PI && state == 2)
            {
                transform.position = m_body.position + new Vector2(Time.deltaTime * spd, Time.deltaTime * spd);
            }

        }
    }
}
