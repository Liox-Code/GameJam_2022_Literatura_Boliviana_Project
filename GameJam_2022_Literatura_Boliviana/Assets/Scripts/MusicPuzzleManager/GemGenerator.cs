using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class GemGenerator : MonoBehaviour
{
    //AudioSource
    public AudioSource audioSource;

    //Singleton
    public static GemGenerator instance;

    private bool isGameOver;
    [SerializeField] private GameObject PauseMenu;

    // Define Gems Quantity of all types
    [Range(1, 20)] [SerializeField] private int gemQuantity = 1;

    //Define Gem prefab to be instantiated and the SpriteRenderer to get the Size of the sprite
    [SerializeField] private GameObject[] gems;
    private SpriteRenderer spGem;


    [SerializeField] private AudioClip[] gemMelody;

    public class GemTypes
    {
        public GemType gemType;
        public int gemsTypeQuantity;
        public AudioClip gemTypeMelody;
    }

    List<GemTypes> gemTypesList = new List<GemTypes>();
    public List<GemTypes> activeGemTypesList;
    public AudioClip winSong;


    //Define GemTypes
    public GemType activeGemType;

    //Define current Gemtype able to be destroyed
    public int currentActiveGemType;

    public static Action OnUpdateCurrentGemType;
    public static Action OnGemDestroy;

    private IEnumerator activeRandomGemTypes;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //2 Loops to instantiate Gem per Row and Column
        for (int gemType = 0; gemType < gems.Length; gemType++)
        {
            //Get Sprite of the GameObject
            spGem = gems[gemType].transform.Find("GemSprite").GetComponent<SpriteRenderer>();

            //Populate GemTypes
            gemTypesList.Add(new GemTypes { gemType = gems[gemType].GetComponent<GemType>(), gemsTypeQuantity = gemQuantity, gemTypeMelody = gemMelody[gemType] });

            for (int gemIndex = 0; gemIndex < gemQuantity; gemIndex++)
            {
                //Instantiate Gem and Set a the new position to avoid collision error when gems are instantiated at the same place
                GameObject createdGem = Instantiate(gems[gemType]);
                //Set diferent transform position to be initialized, depending on the row and column
                float xPosition = this.transform.position.x + (gemIndex * createdGem.GetComponent<CapsuleCollider2D>().bounds.size.x) + (gemIndex * 0.2f);
                float yPosition = this.transform.position.y + ((gemType * createdGem.GetComponent<CapsuleCollider2D>().bounds.size.y) + (gemType * 0.2f)) * -1;
                Vector2 gemPosition = new Vector2(xPosition, yPosition);
                createdGem.transform.position = gemPosition;;
            }
        }

        activeGemTypesList = gemTypesList;
        activeRandomGemTypes = ActiveRandomGemType();
        StartCoroutine(activeRandomGemTypes);
        DialogManager.instance.ShowMessage("Haz click en los fractales para destruirlos, solo se pueden destruir los fractales activos.");

        OnGemDestroy += GemDestroyed;
    }

    private void OnDisable()
    {
        OnGemDestroy -= GemDestroyed;
    }

    private void Update()
    {
        if (isGameOver && Keyboard.current.anyKey.wasPressedThisFrame && GameManager.instance != null)
        {
            PlayerController.instance.nextPlaceName = "AmarilloHouseWindow";
            GameManager.instance.changeScene("AmarilloHouse");
        }
    }

    IEnumerator ActiveRandomGemType()
    {
        while (true)
        {
            activeGemTypesList = activeGemTypesList.Where(activeGemType => activeGemType.gemsTypeQuantity > 0).ToList<GemTypes>();
            if (activeGemTypesList.Count <= 0)
            {
                //PuzzleCompleted();
                break;
            };
            currentActiveGemType = UnityEngine.Random.Range(0, activeGemTypesList.Count);
            audioSource.clip = activeGemTypesList[currentActiveGemType].gemTypeMelody;
            audioSource.Play();
            activeGemType = activeGemTypesList[currentActiveGemType].gemType;
            OnUpdateCurrentGemType?.Invoke();

            yield return new WaitForSeconds(2f);
        }
    }

    public void GemDestroyed()
    {
        activeGemTypesList[currentActiveGemType].gemsTypeQuantity--;

        if (activeGemTypesList[currentActiveGemType].gemsTypeQuantity <= 0)
        {
            StopCoroutine(activeRandomGemTypes);
            Debug.Log("Stop");
            StartCoroutine(activeRandomGemTypes);
            activeGemTypesList = activeGemTypesList.Where(activeGemType => activeGemType.gemsTypeQuantity > 0).ToList<GemTypes>();
            if (activeGemTypesList.Count <= 0)
            {
                PuzzleCompleted();
            }
        }
    }

    public void PuzzleCompleted()
    {
        audioSource.clip = winSong;
        audioSource.Play();
        StartCoroutine(ShowPauseMenu());
        StopCoroutine(activeRandomGemTypes);

        if (QuestManager.instance == null)
        {
            return;
        }

        if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_MUSIC_PUZZLE)
        {
            QuestManager.instance.QuestCompleted();
        }
    }

    IEnumerator ShowPauseMenu() {
        yield return new WaitForSeconds(2);
        PauseMenu.SetActive(true);
        isGameOver = true;
    }
}
