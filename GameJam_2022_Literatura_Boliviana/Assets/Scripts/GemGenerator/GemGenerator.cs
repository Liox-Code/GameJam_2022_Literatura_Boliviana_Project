using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GemGenerator : MonoBehaviour
{
    // Define MouseController of the new Input System on Unity
    private Mouse_Input_Actions mouseController;

    // Define Gems pre Row and per Column
    [Range(1, 21)][SerializeField] private int gemPerRow = 1;
    [Range(1, 7)] [SerializeField] private int gemPerColumn = 1;

    //Define Gem prefab to be instantiated and the SpriteRenderer to get the Size of the sprite
    [SerializeField] private GameObject gem;
    private SpriteRenderer spGem;

    //Define Sprites to be aplied on the instantited Gems
    [SerializeField] private Sprite[] gemColors;

    private void Start()
    {
        // Activate mouse controler and add StartedClickFunction when click started;
        mouseController = new Mouse_Input_Actions();
        mouseController.Enable();
        mouseController.Mouse.Click.started += ctx => StartedClick();

        //Get GameObject of the Sprite to be replace
        spGem = gem.transform.Find("GemSprite").GetComponent<SpriteRenderer>();

        //2 Loops to instantiate Gem per Row and Column
        for (int column = 0; column < gemPerColumn; column++)
        {
            for (int row = 0; row < gemPerRow; row++)
            {
                //Set diferent transform position to be initialized, depending on the row and column
                float xPosition = this.transform.position.x + (row * spGem.bounds.size.x) + (row * 0.2f);
                float yPosition = this.transform.position.y + ((column * spGem.bounds.size.y) + (column * 0.2f)) * -1;
                Vector2 gemPosition = new Vector2(xPosition, yPosition);

                //Instantiate Gem and Set a the new position to avoid collision error when gems are instantiated at the same place
                GameObject createdGem = Instantiate(gem);
                createdGem.transform.position = gemPosition;

                //Set a diferent Gem sprite to change gem color
                Sprite SelectedColorGem = gemColors[Random.Range(0, gemColors.Length)];
                createdGem.transform.Find("GemSprite").GetComponent<SpriteRenderer>().sprite = SelectedColorGem;
            }
        }
    }

    private void StartedClick()
    {
        // Raycast to get mouse position when clicked has started
        Ray ray = CameraController.instance.GetComponent<Camera>().ScreenPointToRay(mouseController.Mouse.Position.ReadValue<Vector2>());
        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null)
        {
            IClick clickOnGem = hits2D.collider.gameObject.GetComponent<IClick>();
            if (clickOnGem != null) clickOnGem.onClickAction();
            Debug.Log($"Hits 2d Collider {hits2D.collider.tag}");
        }
    }
}
