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

        if (QuestManager.instance == null)
        {
            gameObject.SetActive(false);
            return;
        }

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
                audioSource.Play();
                QuestManager.instance.questObject[1].quest.CompleteQuest();
                StartCoroutine(HideItem());
            }
        }
    }

    IEnumerator HideItem()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
