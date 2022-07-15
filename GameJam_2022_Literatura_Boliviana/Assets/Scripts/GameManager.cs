using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    PlayerInputAction playerInputAction;

    private bool isInit;
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionSpeed = 2f;
    [SerializeField] private Image restartCircleImage;
    [SerializeField] private float restartCircleDurationSec = 1f;
    private bool isChanging;

    [HideInInspector] public string currentScene;

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

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.UI.Restart.Enable();
        playerInputAction.UI.Restart.started += _ => OnRestartStart();
        playerInputAction.UI.Restart.performed += _ => OnRestart();
        playerInputAction.UI.Restart.canceled += _ => OnRestartCancel();
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && !Keyboard.current.rKey.wasPressedThisFrame && isInit)
        {
            changeScene("Desert");
        }
    }

    private void HidePlayer(Scene oldScene, Scene newScene)
    {
        currentScene = newScene.name;

        StopAllCoroutines();
        isChanging = false;
        StartCoroutine(transitionOpen());
        if (newScene.name == "Init")
        {
            isInit = true;
        }
        else
        {
            isInit = false;
        }

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
    }

    private void OnRestartStart()
    {
        if (isChanging || isInit)
        {
            return;
        }
        restartCircleImage.gameObject.SetActive(true);
        restartCircleImage.material.SetFloat("_Percentage", 1);
        StartCoroutine(RestartLoading());
    }

    private void OnRestartCancel()
    {
        restartCircleImage.gameObject.SetActive(false);
        restartCircleImage.material.SetFloat("_Percentage", 1);
    }

    private void OnRestart()
    {
        restartCircleImage.gameObject.SetActive(false);
        restartCircleImage.material.SetFloat("_Percentage", 1);
        if (isChanging || isInit)
        {
            return;
        }
        StopAllCoroutines();
        DestroyGameobjects();
        changeScene("Init");
    }

    IEnumerator RestartLoading()
    {
        bool isTransitionOver = false;
        restartCircleImage.material.SetFloat("_Percentage", 1);
        while (!isTransitionOver)
        {
            if (restartCircleImage.material.GetFloat("_Percentage") != 0)
            {
                restartCircleImage.material.SetFloat("_Percentage", Mathf.MoveTowards(restartCircleImage.material.GetFloat("_Percentage"), 0f, restartCircleDurationSec * Time.deltaTime));
            }
            else
            {
                isTransitionOver = true;
            }
            yield return null;
        }
    }

    private void DestroyGameobjects()
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
    }

    public void changeScene(string sceneName)
    {
        if (!isChanging)
        {
            isChanging = true;
            StopAllCoroutines();
            if (PlayerController.instance)
            {
                PlayerController.instance.isTalking = true;
            }
            StartCoroutine(transitionClose(sceneName));
        }
    }


    [ContextMenu("transitionOpen")]
    IEnumerator transitionOpen()
    {
        bool isTransitionOver = false;
        transitionImage.material.SetFloat("_Cutoff", -0.1f);
        while (!isTransitionOver)
        {
            if (transitionImage.material.GetFloat("_Cutoff") != 1.1f)
            {
                transitionImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(transitionImage.material.GetFloat("_Cutoff"), 1.1f, transitionSpeed * Time.deltaTime));
            }
            else
            {
                isTransitionOver = true;
            }
            yield return null;
        }
        if (PlayerController.instance)
        {
            PlayerController.instance.isTalking = false;
        }
    }

    [ContextMenu("transitionClose")]
    IEnumerator transitionClose(string sceneName)
    {
        bool isTransitionOver = false;
        transitionImage.material.SetFloat("_Cutoff", 1.1f);
        while (!isTransitionOver)
        {
            if (transitionImage.material.GetFloat("_Cutoff") != -0.1f - transitionImage.material.GetFloat("_EdgeSmoothing"))
            {
                transitionImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(transitionImage.material.GetFloat("_Cutoff"), -0.1f - transitionImage.material.GetFloat("_EdgeSmoothing"), transitionSpeed * Time.deltaTime));
            }
            else
            {
                isTransitionOver = true;
            }
            yield return null;
        }
        isChanging = false;

        if (sceneName == "MusicPuzzle")
        {
            if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_MUSIC_PUZZLE &&
                QuestManager.instance.currentQuest.quest.gameObject.activeInHierarchy &&
                !QuestManager.instance.currentQuest.questState)
            {
                SceneManager.LoadSceneAsync(sceneName);
            }
        }
        SceneManager.LoadSceneAsync(sceneName);
    }

    public IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(1);
        DestroyGameobjects();
        changeScene("Credits");
    }
}
