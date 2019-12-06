using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagement : MonoBehaviour
{
    public bool questCrousActive = false;
    public GameObject textQuestCrousHolder;

    // Update is called once per frame
    void Update()
    {
        if (questCrousActive && textQuestCrousHolder.activeSelf == false)
        {
            textQuestCrousHolder.SetActive(true);
        }

        if (!questCrousActive && textQuestCrousHolder.activeSelf == true)
        {
            textQuestCrousHolder.SetActive(false);
        }
    }
}
