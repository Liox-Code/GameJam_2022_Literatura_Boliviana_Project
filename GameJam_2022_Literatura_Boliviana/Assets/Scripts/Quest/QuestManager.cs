using System.Collections;
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

    public QuestObject[] questObject;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
