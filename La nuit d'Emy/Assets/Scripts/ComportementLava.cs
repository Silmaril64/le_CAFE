using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ComportementLava : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("MORT", true);
        StartCoroutine(waiter());
        
    }

    IEnumerator waiter()
        {
            //Wait for 4 seconds
            yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
}
