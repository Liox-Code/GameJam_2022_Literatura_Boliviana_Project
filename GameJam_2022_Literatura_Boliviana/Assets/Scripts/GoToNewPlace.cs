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

                GameManager.instance.changeScene(newPlaceName);
            }
        }
    }
}
