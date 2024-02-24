using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteTextChar : MonoBehaviour
{

    public float typingSpeed;
    public string line;

    [SerializeField] TextMeshProUGUI dialogueText;

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";

        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayLine(line));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
