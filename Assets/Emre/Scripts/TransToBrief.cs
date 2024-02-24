using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransToBrief : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("BriefScene",LoadSceneMode.Single);
    }
}
