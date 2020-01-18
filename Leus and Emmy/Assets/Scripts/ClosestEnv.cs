using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnv : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public float ClosestRightObstacle(GameObject body, LayerMask layer)
    {
        float width = body.GetComponent<Renderer>().bounds.size.x;
        float height = body.GetComponent<Renderer>().bounds.size.y;

        Vector2 center = body.GetComponent<Renderer>().bounds.center;

        RaycastHit2D hit1 = Physics2D.Raycast(center + new Vector2(width / 2, height / 2), Vector2.right, layer);
        RaycastHit2D hit2 = Physics2D.Raycast(center + new Vector2(width / 2, -height / 2), Vector2.right, layer);

        return Mathf.Min(hit1.distance, hit2.distance);
    }

    static public float ClosestLeftObstacle(GameObject body, LayerMask layer)
    {
        float width = body.GetComponent<Renderer>().bounds.size.x;
        float height = body.GetComponent<Renderer>().bounds.size.y;

        Vector2 center = body.GetComponent<Renderer>().bounds.center;

        RaycastHit2D hit1 = Physics2D.Raycast(center + new Vector2(-width / 2, height / 2), Vector2.left, layer);
        RaycastHit2D hit2 = Physics2D.Raycast(center + new Vector2(-width / 2, -height / 2), Vector2.left, layer);

        return Mathf.Min(hit1.distance, hit2.distance);
    }
    static public float ClosestDownObstacle(GameObject body, LayerMask layer)
    {
        float width = body.GetComponent<Renderer>().bounds.size.x;
        float height = body.GetComponent<Renderer>().bounds.size.y;

        Vector2 center = body.GetComponent<Renderer>().bounds.center;

        RaycastHit2D hit1 = Physics2D.Raycast(center + new Vector2(width / 2, -height / 2), Vector2.down, layer);
        RaycastHit2D hit2 = Physics2D.Raycast(center + new Vector2(-width / 2, -height / 2), Vector2.down, layer);

        return Mathf.Min(hit1.distance, hit2.distance);
    }
    static public float ClosestUpObstacle(GameObject body, LayerMask layer)
    {
        float width = body.GetComponent<Renderer>().bounds.size.x;
        float height = body.GetComponent<Renderer>().bounds.size.y;

        Vector2 center = body.GetComponent<Renderer>().bounds.center;

        RaycastHit2D hit1 = Physics2D.Raycast(center + new Vector2(width / 2, height / 2), Vector2.up, layer);
        RaycastHit2D hit2 = Physics2D.Raycast(center + new Vector2(-width / 2, height / 2), Vector2.up, layer);

        return Mathf.Min(hit1.distance, hit2.distance);
    }
}
