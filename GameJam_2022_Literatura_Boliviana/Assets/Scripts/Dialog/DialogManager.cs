using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    DialogInputActions dialogController;

    public dialogLinesScript[] dialogLines;
    public class dialogLinesScript {
        public DialogActors.ActorType actor ;
        public string dialogLine;
    }
    Dictionary<string, dialogLinesScript[]> diologScript = new Dictionary<string, dialogLinesScript[]>()
    {
        {"Dialog_1", new dialogLinesScript[2]{
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Un espacio con un cristal intacto. Esto es mucho mejor de lo que esperaba. ¿Qué clase de estudio realiza en este lugar, señor? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Religioso. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego dígame cuánto puede pagar y, no sé, la mandaré a la mierda.  " }
        }}
    };

    private int currentDialogLine = 1;
    
    [SerializeField] private GameObject dialogBackground;
    [SerializeField] private GameObject[] dialogActorsImages;
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

    public void ShowDialog(string currentDialog)
    {;
        dialogController.Enable();
        dialogActive = true;
        dialogBackground.SetActive(true);
        dialogLines = diologScript[currentDialog];
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
            DialogActors.ActorType talkingActor = dialogLines[currentDialogLine].actor;;
            foreach (GameObject dialogActor in dialogActorsImages)
            {
                if (talkingActor == dialogActor.GetComponent<DialogActors>().actorType)
                {
                    dialogActor.SetActive(true);
                }
                else
                {
                    dialogActor.SetActive(false);
                }
            }
            dialogText.text = dialogLines[currentDialogLine].dialogLine;
        }
        if (currentDialogLine >= dialogLines.Length)
        {
            CloseDialog();
        }
    }
}
