using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//using UnityEditor.SceneManagement;

public class Crédits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("Crédits");
        //EditorSceneManager.OpenScene("Assets/Scenes/Crédits.unity", OpenSceneMode.Single);
    }
}
