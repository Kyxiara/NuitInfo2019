using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogBoxText;
    public Text dialogueInputText;
    public bool dialogueActive;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
                dialogueActive = false;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                dialogBoxText.text += "\nPlayer: " + dialogueInputText.text;
            }
        }
    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        gameObject.SetActive(true);
        dialogBoxText.text = dialogue;
    }
}
