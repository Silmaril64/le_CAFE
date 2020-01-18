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
    const float jumpSpeed = 10.0f;
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

    void AddForce(float hor, float ver)
    {

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
        if (Mathf.Abs(hor) > epsilon)
        {
            AddForce(hor, 0);
        }
        switch (etat)
        {
            case 0:
                if (Vector3.Magnitude(body.velocity) <= epsilon)
                {
                    etat = 1;
                } 
                if (TODO: jump){
                    etat = 3;
                    AddForce(0, jumpSpeed);
                }
                break;
            case 1:
                if (Vector3.Magnitude(body.velocity) <= epsilon && Mathf.Abs(hor) <= epsilon)
                {
                    etat = 0;
                }
                if (Vector3.Magnitude(body.velocity) > maxSpeed + epsilonSpeed)
                {
                    etat = 2;
                }
                if (TODO: jump){
                    etat = 3;
                    AddForce(0, jumpSpeed);
                }
                break;
            case 2:
                if (Vector3.Magnitude(body.velocity) <= epsilon && Mathf.Abs(hor) <= epsilon)
                {
                    etat = 0;
                }
                if (TODO: jump){
                    etat = 3;
                    AddForce(0, jumpSpeed);
                }
                break;
            case 3:
                
                if (IsWall() != 0)
                {
                    etat = 4;
                    savedMomentum = Vector3.Magnitude(body.velocity);
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
                if (ClosestEnv.ClosestRightObstacle() < epsilon) // on est tombé au sol
                {
                    etat = 0;
                }
                else if (ClosestEnv.ClosestLeftObstacle() < epsilon) // on a le mur à gauche
                {
                    if (hor > epsilon)
                    {
                        etat = 3;
                    }
                }
                else if (ClosestEnv.ClosestRightObstacle() < epsilon) // on a le mur à droite
                {
                    if ( hor < -epsilon)
                    {
                        etat = 3;
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
