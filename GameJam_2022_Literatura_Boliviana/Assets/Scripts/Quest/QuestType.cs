using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestType : MonoBehaviour
{
    public enum QuestId
    {
        QUEST_INIT,
        QUEST_INITIAL_CONVERSATION,
        QUEST_MUSIC_PUZZLE,
        QUEST_FIND_HEARTH,
        QUEST_WATCH_THROUGH_WINDOW
    }

    public QuestId questId;
}
