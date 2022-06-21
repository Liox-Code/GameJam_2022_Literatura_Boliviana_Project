using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialog : MonoBehaviour
{
    NPCInputActions npcController;

    private bool playerOnZone;

    private void Start()
    {
        npcController = new NPCInputActions();
        npcController.Enable();
        npcController.NPC.OpenDialog.started += ctx => TalkNPC();

        if (!QuestManager.instance.questObject[2].quest.gameObject.activeInHierarchy
            && !QuestManager.instance.questObject[2].questState
            && QuestManager.instance.questObject[2].quest.questId == QuestType.QuestId.QUEST_0_INITIAL_CONVERSATION
            && SceneManager.GetActiveScene().name == "AmarilloHouse")
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        if ((QuestManager.instance.questObject[2].quest.gameObject.activeInHierarchy
            || QuestManager.instance.questObject[2].questState) 
            && QuestManager.instance.questObject[2].quest.questId == QuestType.QuestId.QUEST_0_INITIAL_CONVERSATION
            && SceneManager.GetActiveScene().name == "Desert")
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        npcController.Disable();
    }

    private void TalkNPC()
    {
        if (playerOnZone)
        {
            DialogManager.instance.ShowDialog();
            if (gameObject.GetComponentInParent<NPCMovement>() != null)
            {
                gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnZone = false;
        }
    }
}
