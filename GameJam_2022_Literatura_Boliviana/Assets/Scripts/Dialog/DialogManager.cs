using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    DialogInputActions dialogController;

    public dialogLinesScript[] dialogLines;
    public class dialogLinesScript {
        public DialogActors.ActorType actor ;
        public string dialogLine;
    }

    private string currentDialog;
    Dictionary<string, dialogLinesScript[]> diologScript = new Dictionary<string, dialogLinesScript[]>()
    {
        {"Dialog_1", new dialogLinesScript[2]{
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Un espacio con un cristal intacto. Esto es mucho mejor de lo que esperaba. �Qu� clase de estudio realiza en este lugar, se�or? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Religioso. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego d�game cu�nto puede pagar y, no s�, la mandar� a la mierda.  " }
        }},
        {"Dialog_2", new dialogLinesScript[13]{
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Qu� tal? �Cu�nto crees que vale este mont�n de mierda?" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Vale un cielo de noche" },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Qu�? " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Eso digo yo. �Qu� sab�as t� de Claudio?" },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Claudio? ��C�mo sabes t�?!" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Me contrataron para encontrar a sus hijos. No me dir�s que est�n bajo esa mesa que tienes ah�, �no? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Yo no s� nada! T� no eres de ninguna empresa, �no?, ahora mismo vas dejar mi casa..." },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Aunque a la gente no le parece, usualmente soy muy profesional. �Sabes qu� significa eso? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Y... Yo... no he hecho nada... Claudio fue el que quiso cambiar los par�metros. Yo estaba... estoy por llegar a una conclusi�n... hasta he cambiado mi centro neuronal para ver mejor lo que... lo que tenemos que ver... �l es el que ha insistido en usar el ritual de los ni�os perdidos... " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "D�jame terminar, cabr�n. Soy una profesional porque soy capaz de justificar la cantidad de dinero que pido a mis contratantes. Eficiencia es la palabra. Y hay cosas que son tan ineficientes que me horrorizan si me contratas a m� misma. Un interrogatorio con un robot demente, por ejemplo. O un cad�ver por ac� y all�, nunca es algo bueno." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Por favor... yo no s� d�nde se ha ido... pero s� m�s o menos lo que quer�a... " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Y claro que sabes, s�lo que no voy a perder tiempo sac�ndole la informaci�n. Ahora, respira profundo, Amarillo" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "As� que clonaste tus propios �rganos. Y vibran� " },
        }}
    };

    [System.Serializable]
    public class actorDialogVoice
    {
        public DialogActors.ActorType actor;
        public AudioClip actorVoice;
    }

    [SerializeField] private actorDialogVoice[] actorsVoiceList; 

    private int currentDialogLine = 1;
    
    [SerializeField] private GameObject dialogBackground;
    [SerializeField] private GameObject[] dialogActorsImages;
    [SerializeField] TextMeshProUGUI dialogText;
    private AudioSource audioSource;
    [SerializeField] private float typingTime;
    [SerializeField] private float charsToPlaySound;
    public bool dialogActive;

    private void Awake()
    {
        Debug.Log(instance);
        if (instance != null)
        {
            Destroy(this.transform.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this.transform.gameObject);
    }

    private void Start()
    {
        dialogController = new DialogInputActions();
        dialogController.Dialog.NextDialog.started += ctx => NextDialog();
        dialogController.Dialog.PreviousDialog.started += ctx => PreviousDialog();

        audioSource = GetComponent<AudioSource>();

        currentDialog = "Dialog_1"; 
    }

    public void ShowDialog()
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
        StopAllCoroutines();
        if (currentDialogLine >= 0 && currentDialogLine < dialogLines.Length)
        {
            DialogActors.ActorType talkingActor = dialogLines[currentDialogLine].actor;

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

            foreach (actorDialogVoice actorVoice in actorsVoiceList)
            {
                if (actorVoice.actor == talkingActor)
                {
                    audioSource.clip = actorVoice.actorVoice;
                }
            }

            //dialogText.text = dialogLines[currentDialogLine].dialogLine;
            StartCoroutine(ShowLine(dialogLines[currentDialogLine].dialogLine));
        }
        if (currentDialogLine >= dialogLines.Length)
        {
            CloseDialog();
            if (QuestManager.instance.questObject[0] == null)
            {
                Debug.Log("No existe la mision en posicion 0");
            };
            if (currentDialog == "Dialog_1" && !QuestManager.instance.questObject[0].quest.gameObject.activeInHierarchy && !QuestManager.instance.questObject[0].questState)
            {
                QuestManager.instance.questObject[0].quest.gameObject.SetActive(true);
                QuestManager.instance.questObject[0].quest.StartQuest();
            }
        }
    }

    private IEnumerator ShowLine(string dialogLine)
    {
        dialogText.text = string.Empty;
        int charIndex = 0;

        foreach (char ch in dialogLine)
        {
            dialogText.text += ch;

            if (charIndex % charsToPlaySound == 0)
            {
                audioSource.Play();
            }

            charIndex++;

            yield return new WaitForSeconds(typingTime);
        }
    }

}
