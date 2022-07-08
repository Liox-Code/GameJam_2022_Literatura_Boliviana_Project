using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractWith : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject textSign;
    [SerializeField] private GameObject objectDescription;

    private bool isInteractionActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_WATCH_THROUGH_WINDOW)
            {
                if (PlayerController.instance != null) PlayerController.instance.playerInteract += interactWithObject;
                textSign.SetActive(true);
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_WATCH_THROUGH_WINDOW)
            {
                if (PlayerController.instance != null) PlayerController.instance.playerInteract -= interactWithObject;
                textSign.SetActive(false);
                canvas.SetActive(false);
            }
        }
    }

    private void interactWithObject()
    {
        isInteractionActive = !isInteractionActive;
        PlayerController.instance.isTalking = isInteractionActive;
        objectDescription.SetActive(isInteractionActive);

        if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_WATCH_THROUGH_WINDOW && !isInteractionActive)
        {
            QuestManager.instance.QuestCompleted();
            if (PlayerController.instance != null) PlayerController.instance.playerInteract -= interactWithObject;
        }
    }
}
