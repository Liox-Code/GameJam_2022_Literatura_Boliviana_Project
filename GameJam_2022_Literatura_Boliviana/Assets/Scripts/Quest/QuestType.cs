using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestType : MonoBehaviour
{
    public enum QuestId
    {
        QUEST_0_INIT,
        QUEST_1_INITIAL_CONVERSATION,
        QUEST_2_MUSIC_PUZZLE,
        QUEST_3_FIND_HEARTH
    }

    public QuestId questId;
}
