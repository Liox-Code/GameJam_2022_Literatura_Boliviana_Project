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
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += HidePlayer;
    }

    private void OnEnable()
    {

        Debug.Log("enable");
    }

    private void HidePlayer(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MusicPuzzle")
        {
            if (CameraController.instance != null)
            {
                CameraController.instance.transform.position = new Vector3(0,0,CameraController.instance.transform.position.z);
            }
            Destroy(PlayerController.instance.gameObject);
        }
    }
}
