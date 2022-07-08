using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    PlayerInputAction playerInputAction;

    private bool isInit;

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.UI.Restart.Enable();
        playerInputAction.UI.Restart.started += _ => OnRestart();
    }
    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && !Keyboard.current.rKey.wasPressedThisFrame && isInit)
        {
            SceneManager.LoadScene("Desert");
        }
    }

    private void HidePlayer(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MusicPuzzle")
        {
            if (CameraController.instance != null)
            {
                CameraController.instance.followTarget = null;
                CameraController.instance.transform.position = new Vector3(0,0,CameraController.instance.transform.position.z);
            }
            if(PlayerController.instance != null)
            {
                if (PlayerController.instance.gameObject.activeInHierarchy)
                {
                    PlayerController.instance.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (PlayerController.instance != null)
            {
                if (!PlayerController.instance.gameObject.activeInHierarchy)
                {
                    PlayerController.instance.gameObject.SetActive(true);
                }
                CameraController.instance.followTarget = PlayerController.instance.gameObject;
            }
        }

        //if (newScene.name == "AmarilloHouse")
        //{
        //    if (QuestManager.instance == null)
        //    {
        //        return;
        //    }
        //}

        if (newScene.name == "Init")
        {
            isInit = true;
        }
        else
        {
            isInit = false;
        }
    }

    private void OnRestart()
    {
        if (DialogManager.instance != null)
        {
            Destroy(DialogManager.instance.gameObject);
        }
        if (QuestManager.instance != null)
        {
            Destroy(QuestManager.instance.gameObject);
        }
        if (CameraController.instance != null)
        {
            Destroy(CameraController.instance.gameObject);
        }
        if (PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }
        changeScene("Init");
    }

    public void changeScene(string sceneName)
    {
        if (sceneName == "MusicPuzzle")
        {
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_MUSIC_PUZZLE &&
                QuestManager.instance.currentQuest.quest.gameObject.activeInHierarchy &&
                !QuestManager.instance.currentQuest.questState)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        SceneManager.LoadScene(sceneName);
    }
}
