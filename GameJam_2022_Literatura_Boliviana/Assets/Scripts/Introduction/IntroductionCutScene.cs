using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionCutScene : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] Sprite[] introductionImages;

    PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Interact.Enable();
        playerInputAction.Player.Interact.started += _ => NextImage();
    }

    private void OnDisable()
    {
        playerInputAction.Player.Interact.started -= _ => NextImage();
        playerInputAction.Player.Interact.Disable();
    }

    void Start()
    {
        image.sprite = introductionImages[0];
    }

    private void NextImage()
    {
        int currentQuestIndex = System.Array.FindIndex(introductionImages, introImage => introImage.Equals(image.sprite)); 
        if (currentQuestIndex + 1 < introductionImages.Length)
        {
            image.sprite = introductionImages[currentQuestIndex + 1];
        }
        if (currentQuestIndex + 1 == introductionImages.Length)
        {
            GameManager.instance.changeScene("Desert");
        }
    }
}
