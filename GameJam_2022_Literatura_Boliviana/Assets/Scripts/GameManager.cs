using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

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

        SceneManager.activeSceneChanged += HidePlayer;
    }

    private void HidePlayer(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MusicPuzzle")
        {
            if (CameraController.instance != null)
            {
                CameraController.instance.transform.position = new Vector3(0,0,CameraController.instance.transform.position.z);
            }
            if(PlayerController.instance.gameObject != null)
            {
               Destroy(PlayerController.instance.gameObject);
            }
        }
        else
        {
            if (PlayerController.instance.gameObject != null)
            {
                CameraController.instance.followTarget = PlayerController.instance.gameObject;
            }
        }

        if (newScene.name == "AmarilloHouse")
        {
            if (QuestManager.instance == null)
            {
                return;
            }
        }
    }
}
