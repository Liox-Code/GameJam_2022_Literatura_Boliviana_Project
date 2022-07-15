using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialog : MonoBehaviour
{
    private bool playerOnZone;

    private void Start()
    {

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
        if (PlayerController.instance != null) PlayerController.instance.playerInteract -= TalkNPC;
    }

    private void TalkNPC()
    {

        if (playerOnZone)
        {
            if (PlayerController.instance != null) PlayerController.instance.playerInteract -= TalkNPC;
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
            if (PlayerController.instance != null) PlayerController.instance.playerInteract += TalkNPC;
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
            if (PlayerController.instance != null) PlayerController.instance.playerInteract -= TalkNPC;
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
