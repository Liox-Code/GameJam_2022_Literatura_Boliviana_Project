using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public QuestType.QuestId questId;

    public void StartQuest()
    {
        Debug.Log($"Comenzar mision {questId}");
        if (questId == QuestType.QuestId.QUEST_1_MUSIC_PUZZLE)
        {
            DialogManager.instance.ShowMessage("Ve a la ventana central en la casa de Amarillo.");
        }
        if (questId == QuestType.QuestId.QUEST_2_FIND_HEARTH)
        {
            DialogManager.instance.ShowMessage("Encuentra y recoge una bolsa de organos alrededor de la casa de Amarillo.");
        }
    }

    public void CompleteQuest()
    {
        foreach (QuestManager.QuestObject objectQuestId in QuestManager.instance.questObject)
        {
            if (objectQuestId.quest.questId == questId)
            {
                objectQuestId.questState = true;
                gameObject.SetActive(false);
                QuestManager.instance.MisionCompleted(questId);
            }
        }
    }

}
