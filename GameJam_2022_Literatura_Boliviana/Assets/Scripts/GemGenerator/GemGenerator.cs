using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GemGenerator : MonoBehaviour
{
    [Range(1, 21)][SerializeField] private int gemPerRow = 1;
    [Range(1, 7)] [SerializeField] private int gemPerColumn = 1;

    private BoxCollider2D gemGeneratorBoxCollider;
    [SerializeField] private GameObject gem;
    private SpriteRenderer gemSpriteRenderer;
    [SerializeField] private Sprite[] gemColors;

    private void Start()
    {
        gemGeneratorBoxCollider = GetComponent<BoxCollider2D>();
        gemSpriteRenderer = gem.GetComponent<SpriteRenderer>();

        int gemCuantity = gemPerRow * gemPerColumn;
        for (int column = 0; column < gemPerColumn; column++)
        {
            for (int row = 0; row < gemPerRow; row++)
            {
                float xPosition = this.transform.position.x + (row * gemSpriteRenderer.bounds.size.x) + (row * 0.2f);
                float yPosition = this.transform.position.y + ((column * gemSpriteRenderer.bounds.size.y) + (column * 0.2f)) * -1;
                Vector2 gemPosition = new Vector2(xPosition, yPosition);
                GameObject createdGem = Instantiate(gem);
                createdGem.transform.position = gemPosition;

                Sprite SelectedColorGem = gemColors[Random.Range(0, gemColors.Length)];
                createdGem.GetComponent<SpriteRenderer>().sprite = SelectedColorGem;
            }
        }
    }
}
