using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    public Animator transition;

    public string sceneName;
    public float transitionTime = 1f;
    public bool gonext = false;

    void Update()
    {
        if(gonext)
            LoadNextLevel();

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void SetNextScene()
    {
        
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}