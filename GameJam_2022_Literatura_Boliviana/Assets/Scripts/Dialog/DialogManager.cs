using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    DialogInputActions dialogController;

    public string[] dialogLines;
    public int currentDialogLine;
    [SerializeField] private GameObject dialogBackground;
    [SerializeField] TextMeshProUGUI dialogText;
    public bool dialogActive;

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
        dialogController = new DialogInputActions();
        dialogController.Dialog.NextDialog.started += ctx => NextDialog();
        dialogController.Dialog.PreviousDialog.started += ctx => PreviousDialog();
    }

    private void OnDisable()
    {
        dialogController.Disable();
    }

    public void ShowDialog(string[] dialogsLines)
    {
        dialogController.Enable();
        dialogActive = true;
        dialogBackground.SetActive(true);
        dialogLines = dialogsLines;
        currentDialogLine = 0;
        setDialogText(currentDialogLine);

        PlayerController.instance.isTalking = true;
    }

    public void CloseDialog()
    {
        dialogController.Disable();
        dialogActive = false;
        dialogBackground.SetActive(false);
        currentDialogLine = 0;

        PlayerController.instance.isTalking = false;
    }

    private void NextDialog()
    {
        currentDialogLine++;
        setDialogText(currentDialogLine);
    }

    private void PreviousDialog()
    {
        //Debug.Log("PreviousDialog");
        currentDialogLine--;
        if (currentDialogLine < 0)
        {
            currentDialogLine = 0;
        }
        setDialogText(currentDialogLine);
    }

    private void setDialogText (int currentDialogLine)
    {
        if (currentDialogLine >= 0 && currentDialogLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentDialogLine];
        }
        if (currentDialogLine >= dialogLines.Length)
        {
            CloseDialog();
        }
    }
}
