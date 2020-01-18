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
        float diameter = body.GetComponent<CircleCollider2D>().bounds.size.x;

        Vector2 center = body.GetComponent<CircleCollider2D>().bounds.center;

        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.right, Mathf.Infinity, layer);
        
        if (hit.collider == null)
        {
            return Mathf.Infinity;
        }

        return hit.distance - diameter / 2;
    }

    static public float ClosestLeftObstacle(GameObject body, LayerMask layer)
    {
        float diameter = body.GetComponent<CircleCollider2D>().bounds.size.x;

        Vector2 center = body.GetComponent<CircleCollider2D>().bounds.center;

        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.left, Mathf.Infinity, layer);
        if (hit.collider == null)
        {
            return Mathf.Infinity;
        }
        return hit.distance - diameter / 2;
    }
    
}
