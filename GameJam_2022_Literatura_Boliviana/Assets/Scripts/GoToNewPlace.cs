using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = null;
    [SerializeField] private string goToPlaceName;

    private void Start()
    {
    }

    private void OnDisable()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (newPlaceName != null && newPlaceName != "")
        {
            if (collision.CompareTag("Player"))
            {
                if (goToPlaceName != null && goToPlaceName != "")
                {
                    PlayerController.instance.nextPlaceName = goToPlaceName;
                }

                if (newPlaceName == "AmarilloHouse")
                {
                    if (QuestManager.instance.currentQuest.quest != null &&
                        QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_1_INITIAL_CONVERSATION)
                    {
                        QuestManager.instance.QuestCompleted();
                        Debug.Log("In Amarillo House");
                    }
                }

                if (newPlaceName == "MusicPuzzle")
                {
                    if (QuestManager.instance.currentQuest.quest != null && 
                        QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_2_MUSIC_PUZZLE &&
                        QuestManager.instance.currentQuest.quest.gameObject.activeInHierarchy &&
                        !QuestManager.instance.currentQuest.questState)
                    {
                        SceneManager.LoadScene(newPlaceName);
                    }
                    return;
                }

                SceneManager.LoadScene(newPlaceName);
            }
        }
    }
}
