using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class QuestPickItem : MonoBehaviour
{
    public string itemName;

    private void Start()
    {
        if (QuestManager.instance.questObject[1].questState || !QuestManager.instance.questObject[1].quest.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (QuestManager.instance == null)
            {
                return;
            }

            if (QuestManager.instance.questObject[1].quest.gameObject.activeInHierarchy && !QuestManager.instance.questObject[1].questState && QuestManager.instance.questObject[1].quest.questId == QuestType.QuestId.QUEST_2_FIND_HEARTH)
            {
                QuestManager.instance.questObject[1].quest.CompleteQuest();
                gameObject.SetActive(false);
            }
        }
    }
}
