using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransToBrief : MonoBehaviour
{
    public string sceneName;
    public float transitionTime = 1f;

    void OnEnable()
    {
        SceneManager.LoadScene(sceneName);
                
    }
}
