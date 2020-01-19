using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public GameObject blackscreen;
    
    float m_chrono = 0f;
    // Start is called before the first frame update
    void Start()
    {
        blackscreen.transform.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        GetComponent<CanvasRenderer>().SetAlpha(1f);
    }

    // Update is called once per frame
    void Update()
    {
        m_chrono += Time.deltaTime;
        if (m_chrono > 36)
        {
            while (blackscreen.transform.GetComponent<Image>().color.a < 1.0f)
            {
                blackscreen.transform.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, blackscreen.transform.GetComponent<Image>().color.a + 0.01f);
                
            }
        }
        if (m_chrono > 40)
        {
            //EditorSceneManager.LoadScene("Intro");
        }
    }
}
