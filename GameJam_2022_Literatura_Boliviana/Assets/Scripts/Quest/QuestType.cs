using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestType : MonoBehaviour
{
    public enum QuestId
    {
        QUEST_0_INITIAL_CONVERSATION,
        QUEST_1_MUSIC_PUZZLE,
        QUEST_2_FIND_HEARTH
    }

    public QuestId questId;
}
