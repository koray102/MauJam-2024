using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMission : MonoBehaviour
{
    public string sceneName;

    public void StartMissions()
    {
        SceneManager.LoadScene(sceneName);
    }
}
