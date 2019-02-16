using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;
    public Canvas MenuCanvas;

    private void Start()
    {
        MenuCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (MenuCanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape)){
                MenuCanvas.enabled = false;
            }else if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Quitting application");
                if (Application.isEditor)
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                } else
                {

                }
                Application.Quit();
            }
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuCanvas.enabled = true;
        }
        foreach (char letter in Input.inputString)
        {

            wordManager.TypeLetter(letter);
        }
    }
}
