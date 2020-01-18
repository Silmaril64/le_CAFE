using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementEmmi : MonoBehaviour
{
    public GameObject Emmi;
    Rigidbody2D body;

    // On pourra utiliser le produit scalaire de la normale et de la gravité, que l'on soustrait à la normale !
    const float gravity = 10.0f;
    const float maxSpeed = 20.0f; // The player max speed, if the player go over this limit (+epsilon), he starts rolling
    const float epsilonSpeed = 2.0f;
    const float maxRollingSpeed = 40.0f; // The real max speed thanks to physics
    const float mass = 50.0f;
    const Vector2 jumpForce = new Vector2(0, 10.0f);
    const float momentumDecrease = 1.0f;

    // Variables
    float epsilon = Mathf.Pow(10, -3);
    float hor;
    float savedMomentum;
    int etat; // Etats: 
    // - 0: au sol, idle
    // - 1: au sol, déplacement
    // - 2: au sol, roule
    // - 3: en l'air
    // - 4: sur le mur



    // Start is called before the first frame update
    void Start()
    {
        body = Emmi.GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.mass = mass;
        etat = 0;
    }

    void addVelocity(float hor)
    {
        body.velocity += new Vector2(hor * epsilonSpeed, 0);
    }

    int IsWall() //0: rien, 1: gauche, 2: droite
    {
        //raycast en haut et en bas du perso
        return 0;
    }

    bool IsGrounded()
    {
        //raycast des deux cotés de la base
        return 1 == 0;
    }

    void FixedUpdate()
    {
        // Partie contrôles
        hor = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hor) > epsilon && ( body.velocity.x * hor < 0 || Mathf.abs(body.velocity.x) < maxSpeed ))
        {
            addVelocity(hor);
        }
        switch (etat)
        {
            case 0:
                if (body.velocity.magnitude > epsilon)
                {
                    etat = 1;
                } 
                if (Input.GetAxisRaw("Jump") > epsilon)
                {
                    etat = 3;
                    body.AddForce(jumpForce, ForceMode2D.Impulse);
                }
                break;
            case 1:
                if (body.velocity.magnitude <= epsilon && Mathf.Abs(hor) <= epsilon)
                {
                    etat = 0;
                }
                if (body.velocity.magnitude > maxSpeed + epsilonSpeed)
                {
                    etat = 2;
                }
                if (Input.GetAxisRaw("Jump") > epsilon)
                {
                    etat = 3;
                    body.AddForce(jumpForce, ForceMode2D.Impulse);
                }
                break;
            case 2:
                if (body.velocity.magnitude <= epsilon && Mathf.Abs(hor) <= epsilon)
                {
                    etat = 0;
                }
                if (Input.GetAxisRaw("Jump") > epsilon)
                {
                    etat = 3;
                    body.AddForce(jumpForce, ForceMode2D.Impulse);
                }
                break;
            case 3:
                
                if (IsWall() != 0)
                {
                    etat = 4;
                    savedMomentum = body.velocity.magnitude;
                }
                if (IsGrounded())
                {
                    etat = 1;
                }
                break;
            case 4:
                if (savedMomentum > jumpSpeed)
                {
                    savedMomentum -= momentumDecrease;
                }
                if (ClosestEnv.ClosestDownObstacle(body, 0) < epsilon) // on est tombé au sol
                {
                    etat = 0;
                }
                else if (ClosestEnv.ClosestLeftObstacle(body, 0) < epsilon) // on a le mur à gauche
                {
                    if (hor > epsilon)
                    {
                        etat = 3;
                    }
                    if (Input.GetAxisRaw("Jump") > epsilon)
                    {
                        etat = 3;
                        float tempo = Mathf.Max(jumpSpeed, savedMomentum)/2;
                        addVelocity(tempo,tempo);
                    }
                }
                else if (ClosestEnv.ClosestRightObstacle(body, 0) < epsilon) // on a le mur à droite
                {
                    if ( hor < -epsilon)
                    {
                        etat = 3;
                    }
                    if (Input.GetAxisRaw("Jump") > epsilon)
                    {
                        etat = 3;
                        float tempo = Mathf.Max(jumpSpeed, savedMomentum)/2;
                        addVelocity(-tempo,tempo);
                    }
                }
                else // on a plus de mur
                {
                    etat = 3;
                }
                break;

        }
    }

}
