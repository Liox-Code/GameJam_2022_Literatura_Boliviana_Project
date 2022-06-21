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
        QuestManager.OnMissionStart += ShowGoToNewPlace;
    }

    private void OnDisable()
    {
        QuestManager.OnMissionStart -= ShowGoToNewPlace;
    }

    private void ShowGoToNewPlace()
    {

    }

    private void HideGoToNewPlace()
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

                if (newPlaceName == "MusicPuzzle")
                {
                    if (QuestManager.instance.questObject[0].quest.gameObject.activeInHierarchy
                        && !QuestManager.instance.questObject[0].questState 
                        && QuestManager.instance.questObject[0].quest.questId == QuestType.QuestId.QUEST_1_MUSIC_PUZZLE)
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
