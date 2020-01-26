using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    private TextMeshProUGUI play_button;
    // Start is called before the first frame update
    void Start()
    {
        play_button = gameObject.GetComponent<TextMeshProUGUI>();
        play_button.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        play_button.fontSharedMaterial.SetFloat("_GlowPower", 1.0f);
        return;
    }

    private void OnMouseExit()
    {
        play_button.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        return;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("Intro");
    }
}
