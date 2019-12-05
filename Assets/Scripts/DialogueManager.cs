using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Return))
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;
        }
    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }
}
