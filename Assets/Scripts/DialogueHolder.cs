using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialogue;
    private DialogueManager dialogueManager;
    private bool onTrigger;
    private GiveItem giveItem;
    public QuestManagement questManagement;


    private void Awake()
    {
        giveItem = GetComponent<GiveItem>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        onTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onTrigger)
        
            if (giveItem != null)
            {
                if (questManagement.questCrousActive == true)
                    giveItem.give();
            }
            else
            {
                dialogueManager.ShowBox(dialogue);
            }
        }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueManager.dialogueActive)
        {
            onTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }
}
