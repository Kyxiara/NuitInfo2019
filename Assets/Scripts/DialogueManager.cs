using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    APIManager api_manager;

    public Text dialogBoxText;
    public Text dialogueInputText;
    public bool dialogueActive;

    private string port;
    public QuestManagement questManagement;

    private Inventory _inventory;

    public Item rib;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.instance;
        this.gameObject.SetActive(false);
        api_manager = FindObjectOfType<APIManager>();
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
                dialogBoxText.text = "Player: " + dialogueInputText.text;
                Debug.Log(dialogueInputText.text);
                string res = api_manager.POST(port, dialogueInputText.text);
                if (res.Trim().Equals("<new>"))
                {
                    res = "New quest unlocked : CROUS !";
                    questManagement.questCrousActive = true;
                }
                else if (res.Contains("<new-rib>"))
                {
                    res = "Go fetch the RIB !";
                    questManagement.textQuestCrousHolder.GetComponent<Text>().text = "1) Get an accomodation at the CROUS :\n\t" +
                        "- Go fetch the RIB";
                }
                else if (res.Trim().Equals("<done>"))
                {
                    res = "Quest done : CROUS.";
                    questManagement.questCrousActive = false;
                    _inventory.Remove(rib);
                }
                dialogBoxText.text += "\n" + res;
                Debug.Log(res);
            }
        }
    }

    public void ShowBox(string port)
    {
        if (!dialogueActive)
        {
            this.port = port;
            string res;
            if (port == "5006" && _inventory.haveItem(rib))
                res = api_manager.POST(port, "/get_files");
            else if (port == "5006" && questManagement.questCrousActive)
                res = api_manager.POST(port, "/crous_eligible");
            else
                res = api_manager.POST(port, "/start");
            dialogueActive = true;
            gameObject.SetActive(true);
            dialogBoxText.text = res;
        }
    }
}
