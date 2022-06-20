using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class GemGenerator : MonoBehaviour
{
    //AudioSource
    public AudioSource audioSource;

    //Singleton
    public static GemGenerator instance;

    // Define MouseController of the new Input System on Unity
    private MusicPuzzle mouseController;

    [SerializeField] private GameObject PauseMenu;

    // Define Gems Quantity of all types
    [Range(1, 20)] [SerializeField] private int gemQuantity = 1;

    //Define Gem prefab to be instantiated and the SpriteRenderer to get the Size of the sprite
    [SerializeField] private GameObject[] gems;
    private SpriteRenderer spGem;


    [SerializeField] private AudioClip[] gemMelody;

    class GemTypes
    {
        public GemType gemType;
        public int gemsTypeQuantity;
        public AudioClip gemTypeMelody;
    }

    List<GemTypes> gemTypesList = new List<GemTypes>();
    List<GemTypes> activeGemTypesList;


    //Define GemTypes
    public GemType activeGemType;

    //Define current Gemtype able to be destroyed
    private int currentActiveGemType;

    public static Action OnUpdateCurrentGemType;

    private void OnDisable()
    {
        mouseController.Disable();
    }

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

        // Activate mouse controler and add StartedClickFunction when click started;
        mouseController = new MusicPuzzle();
        mouseController.Enable();
        mouseController.Mouse.Click.started += ctx => ClickObject();
        mouseController.UI.ToggleMenu.started += ctx => ToggleMenu();

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
        StartCoroutine(activeRandomGemType());
    }

    private void ToggleMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
    }

    private void ClickObject()
    {
        // Raycast to get mouse position when clicked has started
        Ray ray = CameraController.instance.GetComponent<Camera>().ScreenPointToRay(mouseController.Mouse.Position.ReadValue<Vector2>());
        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null)
        {
            //If hit a gameobject with a Gem tag
            if (hits2D.collider.gameObject.CompareTag("Gem"))
            {
                //If click on gem and current gemtype equal to hit gem gemtype
                IClick clickOnGem = hits2D.collider.gameObject.GetComponent<IClick>();
                if (clickOnGem != null && activeGemTypesList[currentActiveGemType].gemType.GetComponent<GemType>().gemTypes == hits2D.collider.gameObject.GetComponent<GemType>().gemTypes) {
                    clickOnGem.onClickAction();
                    activeGemTypesList[currentActiveGemType].gemsTypeQuantity--;
                };
            }
        }
    }

    IEnumerator activeRandomGemType()
    {
        while (true)
        {
            activeGemTypesList = activeGemTypesList.Where(activeGemType => activeGemType.gemsTypeQuantity > 0).ToList<GemTypes>();
            if (activeGemTypesList.Count <= 0)
            {
                audioSource.Stop();
                StopCoroutine(activeRandomGemType());
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
}
