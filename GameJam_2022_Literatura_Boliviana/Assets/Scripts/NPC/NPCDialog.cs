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

        if (QuestManager.instance.currentQuest.quest == null)
        {
            Debug.LogWarning("CurrentQuest quest is Null");
            return;
        }

        if (QuestManager.instance.currentQuest.quest.questId != QuestType.QuestId.QUEST_INIT
            && SceneManager.GetActiveScene().name == "Desert")
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_INIT
            && !QuestManager.instance.currentQuest.questState
            && SceneManager.GetActiveScene().name != "Desert")
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        QuestManager.OnMissionStart -= NPCDisapear;
        npcController.Disable();
    }

    private void TalkNPC()
    {

        if (playerOnZone)
        {
            DialogManager.instance.ShowDialog();
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_INITIAL_CONVERSATION && SceneManager.GetActiveScene().name == "AmarilloHouse")
            {
                QuestManager.instance.QuestCompleted();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_INIT)
            {
                QuestManager.OnMissionStart += NPCDisapear;
            }
            playerOnZone = true;
            DialogManager.instance.Talk(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_INIT)
            {
                QuestManager.OnMissionStart -= NPCDisapear;
            }
            playerOnZone = false;
            DialogManager.instance.Talk(false);
        }
    }

    public void NPCDisapear()
    {
        QuestManager.OnMissionStart -= NPCDisapear;
        StartCoroutine(BlinkRountine());
    }

    IEnumerator BlinkRountine()
    {
        int blinkTime = 10;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        while (blinkTime > 0)
        {
            transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(blinkTime * 0.01f);
            transform.parent.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(blinkTime * 0.01f);
            blinkTime--;
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        transform.parent.gameObject.SetActive(false);
    }
}
