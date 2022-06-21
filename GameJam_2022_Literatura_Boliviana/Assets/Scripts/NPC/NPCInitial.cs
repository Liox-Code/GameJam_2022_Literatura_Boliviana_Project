using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInitial : MonoBehaviour
{
    public bool startBlink;

    void Update()
    {

        if ((QuestManager.instance.questObject[2].quest.gameObject.activeInHierarchy
            || QuestManager.instance.questObject[2].questState)
            && QuestManager.instance.questObject[2].quest.questId == QuestType.QuestId.QUEST_0_INITIAL_CONVERSATION)
        {
            if (!startBlink)
            {
                startBlink = true;
                StartCoroutine(BlinkRountine());
            }
        }
    }

    IEnumerator BlinkRountine()
    {
        int blinkTime = 10;
        while (blinkTime > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(blinkTime * 0.01f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(blinkTime * 0.01f);
            blinkTime--;
        }
        gameObject.SetActive(false);
    }
}
