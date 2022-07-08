using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class QuestPickItem : MonoBehaviour
{
    AudioSource audioSource;

    public string itemName;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (QuestManager.instance.currentQuest.quest == null)
        {
            Debug.LogWarning("CurrentQuest quest is Null");
            return;
        }

        if (QuestManager.instance.currentQuest.quest.questId != QuestType.QuestId.QUEST_FIND_HEARTH)
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

            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_FIND_HEARTH)
            {
                audioSource.Play();
                StartCoroutine(HideItem());
                QuestManager.instance.QuestCompleted();
            }
        }
    }

    IEnumerator HideItem()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
