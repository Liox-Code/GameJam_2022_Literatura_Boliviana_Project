using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [System.Serializable]
    public class QuestObject
    {
        public string questDialog;
        public Quest quest;
        public bool questState = false;
    }

    public QuestObject[] questObject;

    public static Action OnMissionStart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void MisionStarted(QuestType.QuestId startedQuestId)
    {
        Debug.Log($"Started quest {startedQuestId}");
        if (startedQuestId == QuestType.QuestId.QUEST_1_MUSIC_PUZZLE)
        {
            OnMissionStart?.Invoke();;
        }
    }

    public void MisionCompleted(QuestType.QuestId completedQuestId)
    {
        Debug.Log(completedQuestId);
        if (completedQuestId == QuestType.QuestId.QUEST_1_MUSIC_PUZZLE)
        {
            DialogManager.instance.currentDialog = "Dialog_2";
            DialogManager.instance.ShowMessage("Vuelve a hablar con Amarillo.");
        }
        if (completedQuestId == QuestType.QuestId.QUEST_2_FIND_HEARTH)
        {
            DialogManager.instance.currentDialog = "Dialog_3";
            DialogManager.instance.ShowMessage("Felicidades, terminaste todas las quests.");
        }
    }
}
