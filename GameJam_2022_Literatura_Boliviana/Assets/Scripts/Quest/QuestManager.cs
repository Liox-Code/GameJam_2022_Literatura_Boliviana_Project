using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [System.Serializable]
    public class QuestObject
    {
        public Quest quest;
        public bool questState = false;
    }

    public QuestObject[] questsObject;
    [HideInInspector] public QuestObject currentQuest = null;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        QuestStarted();
    }

    private QuestObject SetCurrentQuest(int questIndex)
    {
        QuestObject newCurrentQuest = null;

        if (questIndex < questsObject.Length && questIndex >= 0)
        {
            newCurrentQuest = questsObject[questIndex];
        }

        return newCurrentQuest;
    }

    private QuestObject NextQuest(int questIndex)
    {
        return SetCurrentQuest((questIndex + 1)) ?? SetCurrentQuest(questIndex);
    }
    private QuestObject Previous(int questIndex)
    {
        return SetCurrentQuest((questIndex - 1)) ?? SetCurrentQuest(questIndex);
    }

    public void QuestStarted()
    {
        int currentQuestIndex = Array.FindIndex(questsObject, questObjetc => questObjetc.Equals(currentQuest));
        Debug.Log(currentQuestIndex);
        if (currentQuestIndex >= 0 && 
            currentQuestIndex < questsObject.Length)
        {
            currentQuest = NextQuest(currentQuestIndex);
            Debug.Log("nEXXT");
        }
        if (currentQuestIndex < 0 || currentQuestIndex >= questsObject.Length)
        {
            currentQuest = SetCurrentQuest(0);
        }

        if (currentQuest.quest &&
            !currentQuest.quest.gameObject.activeInHierarchy &&
            !currentQuest.questState)
        {
            OnMissionStart?.Invoke();
            currentQuest.quest.gameObject.SetActive(true);
            currentQuest.quest.StartQuest();
        }
    }

    public void QuestCompleted()
    {
        if (currentQuest != null &&
            currentQuest.quest.gameObject.activeInHierarchy &&
            !currentQuest.questState)
        {
            currentQuest.quest.CompleteQuest();
            currentQuest.questState = true;
            currentQuest.quest.gameObject.SetActive(false);
        }
    }
}
