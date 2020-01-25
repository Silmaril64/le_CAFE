using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementEmmi : MonoBehaviour
{
    public GameObject Emmi;
    Rigidbody2D body;
    Animator anim;

    // On pourra utiliser le produit scalaire de la normale et de la gravité, que l'on soustrait à la normale !
    float gravity = 2f;// = 10.0f;
    float gravFact;
    float maxSpeed;// = 40.0f; // The player max speed, if the player go over this limit (+epsilon), he starts rolling
    const float epsilonSpeed = 2.0f;
    const float maxRollingSpeed = 20.0f; // The real max speed thanks to physics
    float mass;// = 50.0f;
    float ray;
    float wallJumpTimer;
    bool isLocked;
    float lockTime;
    bool Moving_Right;
    bool isSpawning;
    float SpawningTime;
    float SpawningTimer = 0f;
    float jumpSpeed;// = 300.0f;
    Vector2 jumpForce;// = new Vector2(0, jumpSpeed);
    const float momentumDecrease = 1.0f;
    public LayerMask layer_mask;

    // Variables
    float epsilon = Mathf.Pow(10, -2);
    float hor;
    float savedMomentum;
    public int etat; // Etats: 
    // - 0: au sol, idle
    // - 1: au sol, déplacement
    // - 2: au sol, roule
    // - 3: en l'air
    // - 4: sur le mur



    // Start is called before the first frame update
    void Start()
    {
        isSpawning = true;
        SpawningTime = 1.5f;
        layer_mask = ~layer_mask;
        //layer_mask = ~LayerMask.GetMask("Player");
        Emmi.layer = 8;//LayerMask.GetMask("Player");
        body = Emmi.GetComponent<Rigidbody2D>();
        Moving_Right = true;
        ray = body.GetComponent<CircleCollider2D>().bounds.size.x;
        mass = 4 / 3 * Mathf.PI * Mathf.Pow(ray, 3);
        maxSpeed = ray*2f;
        jumpSpeed = 4f;
        jumpForce = new Vector2(0, jumpSpeed);
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.mass = mass;
        body.gravityScale = gravity;
        gravFact = 1f;
        etat = 0;
        lockTime = 0.5f;
        anim = GetComponent<Animator>();
    }

    void addVelocity(float hor, float ver)
    {
        body.velocity += new Vector2(2*hor, ver);
    }



    int IsWall() //0: rien, 1: gauche, 2: droite
    {
        if (ClosestEnv.ClosestLeftObstacle(Emmi, layer_mask) <= epsilon)
        {
            return 1;
        }
        if (ClosestEnv.ClosestRightObstacle(Emmi, layer_mask) <= epsilon)
        {
            return 2;
        }
        //raycast en haut et en bas du perso
        return 0;
    }

    bool IsGrounded()
    {

        
        Vector2 center = body.GetComponent<CircleCollider2D>().bounds.center;
        float diameter = body.GetComponent<CircleCollider2D>().bounds.size.x;


        RaycastHit2D hitbottom = Physics2D.Raycast(center + new Vector2(0, - diameter / 2), Vector2.down, Mathf.Infinity, layer_mask);

        RaycastHit2D hitleft = Physics2D.Raycast(center + new Vector2(-diameter/2 + epsilon, 0), Vector2.down, Mathf.Infinity, layer_mask);
        RaycastHit2D hitright = Physics2D.Raycast(center + new Vector2(diameter / 2 - epsilon, 0), Vector2.down, Mathf.Infinity, layer_mask);
        return (Mathf.Min(hitleft.distance, hitright.distance) <= (diameter / 2 + epsilon) || hitbottom.distance <= epsilon);
        /*if (Physics2D.OverlapCircle(center, diameter / 2 + epsilon, layer_mask) != null && (ClosestEnv.ClosestLeftObstacle(Emmi, layer_mask) >= epsilon || ClosestEnv.ClosestRightObstacle(Emmi, layer_mask) >= epsilon))
        {

            return true;
        }
        return false;*/
    }

    void FixedUpdate()
    {
        // Partie contrôles
        Debug.Log(hor);
        hor = Input.GetAxis("Horizontal");
        if(isLocked)
        {
            wallJumpTimer += Time.fixedDeltaTime;
            if (wallJumpTimer > lockTime)
            {
                isLocked = false;
            }
        }

        else if (Mathf.Abs(hor) > epsilon && ( body.velocity.x * hor < 0 || Mathf.Abs(body.velocity.x) < maxSpeed )&& !isSpawning)
        {
            if (etat == 3)
            {
                addVelocity(hor / 2, 0);
            }
            else
            {
                addVelocity(hor, 0);
            }
        }

        
        switch (etat)
        {
///////////////////////////////////////////////////////////////////////////
            case 0:
                isLocked = false;
                if (isSpawning)
                {
                    SpawningTimer += Time.fixedDeltaTime;
                    if (SpawningTimer >= SpawningTime)
                    {
                        isSpawning = false;
                    }

                }
                else if (IsWall() != 0)
                {
                    etat = 4;
                    savedMomentum = body.velocity.magnitude * body.mass;
                }
                else if (!IsGrounded())
                {
                    etat = 3;
                }
                else if (Input.GetAxisRaw("Jump") > epsilon)
                {
                    etat = 3;
                    body.AddForce(jumpForce, ForceMode2D.Impulse);
                }
                else if (body.velocity.magnitude > epsilon)
                {
                    etat = 1;
                }
                break;
///////////////////////////////////////////////////////////////////////////////
            case 1:

                isLocked = false;
                if (body.velocity.y < 0)
                {
                    body.gravityScale = gravFact * gravity;
                }
                else if (body.velocity.y > 0)
                {
                    body.gravityScale = gravity / 100; 
                }
                else
                {
                    body.gravityScale = gravity;
                }

                if (Mathf.Abs(hor) <= epsilon)
                {
                    body.velocity = new Vector2(body.velocity.x * 3 / 4, body.velocity.y);
                }

                if (IsWall() != 0)
                {
                    etat = 4;
                    savedMomentum = body.velocity.magnitude * body.mass;
                }
                else if (!IsGrounded())
                {
                    etat = 3;
                }
                else if (Input.GetAxisRaw("Jump") > epsilon)
                {
                    etat = 3;
                    body.AddForce(jumpForce, ForceMode2D.Impulse);
                }
                else if (body.velocity.magnitude <= epsilon && Mathf.Abs(hor) <= epsilon)
                {
                    etat = 0;
                }
                else if (body.velocity.magnitude > maxSpeed + epsilonSpeed)
                {
                    etat = 2;
                }
                break;
/////////////////////////////////////////////////////////////////////////////////////////////
            case 2:

                isLocked = false;

                if (body.velocity.y < 0)
                {
                    body.gravityScale = gravFact * gravity;
                }
                else if (body.velocity.y > 0)
                {
                    body.gravityScale = gravity / 100;
                }
                else
                {
                    body.gravityScale = gravity;
                }

                if (IsWall() != 0)
                {
                    etat = 4;
                    savedMomentum = body.velocity.magnitude * body.mass;
                }
                else if (!IsGrounded())
                {
                    etat = 3;
                }
                else if (Input.GetAxisRaw("Jump") > epsilon)
                {
                    etat = 3;
                    body.AddForce(jumpForce, ForceMode2D.Impulse);
                }
                else if (body.velocity.magnitude <= epsilon && Mathf.Abs(hor) <= epsilon)
                {
                    etat = 0;
                }
                break;
///////////////////////////////////////////////////////////////////////////////////////////////
            case 3:

                body.gravityScale = gravity;
                /* 
                //Frottement de l'air
                
                if (Mathf.Abs(hor) <= epsilon)
                {
                    body.velocity = new Vector2(body.velocity.x * 9 / 10, body.velocity.y);
                }
                */
                if (IsWall() != 0)
                {
                    etat = 4;
                    savedMomentum = body.velocity.magnitude * body.mass;
                }
                else if (IsGrounded())
                {
                    etat = 1;
                }
                break;
/////////////////////////////////////////////////////////////////////////////////////////////
            case 4:

                body.gravityScale = gravity;

                isLocked = false;

                float wjs = Mathf.Max(jumpSpeed, Mathf.Log(savedMomentum/(jumpSpeed*mass) * Mathf.Exp(1))/jumpSpeed);

                if (savedMomentum > jumpSpeed)
                {
                    savedMomentum -= momentumDecrease;
                }

                if (body.velocity.y < 0)
                {
                    body.velocity = new Vector2(body.velocity.x, body.velocity.y);// * 9 / 10);
                }

                if (IsWall() == 0 && IsGrounded()) // on est tombé au sol
                {
                    etat = 0;
                }
                else if (ClosestEnv.ClosestLeftObstacle(Emmi, layer_mask) < epsilon) // on a le mur à gauche
                {
                    
                    if (hor > epsilon)
                    {
                        etat = 3;
                    }
                    if (Input.GetAxisRaw("Jump") > epsilon)
                    {
                        wallJumpTimer = 0;
                        isLocked = true;
                        etat = 3;
                        Vector2 tempo = new Vector2(wjs, wjs);

                        body.AddForce(tempo, ForceMode2D.Impulse);
                    }
                }
                else if (ClosestEnv.ClosestRightObstacle(Emmi, layer_mask) < epsilon) // on a le mur à droite
                {
                    if ( hor < -epsilon)
                    {
                        etat = 3;
                    }
                    if (Input.GetAxisRaw("Jump") > epsilon)
                    {
                        wallJumpTimer = 0;
                        isLocked = true;
                        etat = 3;

                        Vector2 tempo = new Vector2(- wjs, wjs);

                        body.AddForce(tempo, ForceMode2D.Impulse);
                    }
                }
                else // on a plus de mur
                {
                    etat = 3;
                }
                break;

        }

        anim.SetInteger("State", etat);
        Moving_Right = body.velocity.x > 2* epsilon;
        if (!Moving_Right && Emmi.transform.localScale.x > 0 || Moving_Right && Emmi.transform.localScale.x < 0)
        {
            Emmi.transform.localScale = new Vector3(-Emmi.transform.localScale.x, Emmi.transform.localScale.y, Emmi.transform.localScale.z);
        }
    }

}
