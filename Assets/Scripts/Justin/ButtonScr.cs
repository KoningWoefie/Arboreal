using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitBtn() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void StartBtn() {

        // change all text to "Loading..."
        foreach (Text t in FindObjectsOfType<Text>()) {
            t.text = "Loading...";
        }

        // make all buttons non-interactable
        foreach (Button b in FindObjectsOfType<Button>()) {
            b.interactable = false;
        }

        SceneManager.LoadScene("Justin");
    }
}
