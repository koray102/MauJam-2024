using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInverseMouse(bool invMouse)
    {

    }

    public void SetVolume(float volume)
    {
        
    }

    public void SetMouseX(float mouseX)
    {

    }
    public void SetMouseY(float mouseY)
    {

    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }
}
