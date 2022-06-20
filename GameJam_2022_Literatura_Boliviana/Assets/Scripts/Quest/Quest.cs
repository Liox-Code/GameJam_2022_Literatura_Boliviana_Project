using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public enum QuestId
    {
        QUEST_1_MUSIC_PUZZLE,
        QUEST_2_FIND_HEARTH
    }

    public QuestId questId;

    public void StartQuest()
    {
        Debug.Log($"Comenzar mision {questId}");
    }

    public void CompleteQuest()
    {
        Debug.Log($"Mision completada {questId}");
        foreach (QuestManager.QuestObject objectQuestId in QuestManager.instance.questObject)
        {
            if (objectQuestId.quest.questId == questId)
            {
                objectQuestId.questState = true;
                gameObject.SetActive(false);
            }
        }
    }

}
