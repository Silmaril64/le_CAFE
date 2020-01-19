using UnityEngine.SceneManagement;
using UnityEngine;
//using UnityEditor.SceneManagement;

public class LVL2: MonoBehaviour
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
       SceneManager.LoadScene("LVL2");
       // EditorSceneManager.OpenScene("Assets/Scenes/LVL2.unity", OpenSceneMode.Single);
    }
}