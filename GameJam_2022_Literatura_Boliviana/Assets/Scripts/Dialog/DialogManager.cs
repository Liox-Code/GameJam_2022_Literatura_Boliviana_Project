using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    DialogInputActions dialogController;

    public dialogLinesScript[] dialogLines;
    public class dialogLinesScript {
        public DialogTypes.ActorType actor ;
        public string dialogLine;
    }

    public DialogTypes.DialogType currentDialog;
    Dictionary<DialogTypes.DialogType, dialogLinesScript[]> diologScript = new Dictionary<DialogTypes.DialogType, dialogLinesScript[]>()
    {
        {DialogTypes.DialogType.DIALOG_1, new dialogLinesScript[9]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Se�orita?" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Este� disculpe�  quer�a saber si conoce qui�n habitaba o usaba aquel edificio." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Hace tiempo que vivo yo solo. �Por qu�? La licencia y los catastros est�n en orden�" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Represento a Hidrotratamientos LN Iven. Quer�amos constatar el estado de su propiedad para comprarla." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Mi empresa desea usar este terreno para un nuevo modelo de planta generadora de agua, basado en el tipo de contaminaci�n particulada de esta parte de la ciudad." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "La secci�n de presupuestos me ha autorizado a ofrecer 400 mil latinos por el terreno." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Ya ni saben qu� comprar� �No dec�a que por la casa?  " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Consideraremos que el terreno le pertenece. No hay habitantes en al menos tres hect�reas cuadradas." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "S�, s�, los muy desgraciados vienen a quitarnos nuestras tierras para cambiarlas y luego revend�rnoslas m�s caras. Voy a hacer que fije el precio por la oportunidad de rechazar su sucio dinero. Venga, le mostrar�." }
        }},
        {DialogTypes.DialogType.DIALOG_2, new dialogLinesScript[10]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Y creo que quer�an quedarse un buen tiempo. Mire nom�s que instalaron hasta una capilla aqu�. �Qu� le parece? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Pintoresca. Creo que estas im�genes podr�an valuar m�s el costo de la propiedad, se�or... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Amarillo. " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Amarillo... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "As�, sin apellido. Ah� en la polic�a me censaron por el n�mero de serie de mis partes y ahora mi nombre oficial es un mont�n de n�meros. Hay que ser un infeliz para hacerle eso a alguien. " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Se�or Amarillo, �podemos ver otros espacios de su propiedad? Necesito por lo menos un setenta por ciento antes de dar un estimado. Luego, si el precio le parece bien, recomiendo..." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Vamos a ver mi sala de estudio, casi todo lo dem�s son escombros, he  estado tratando de ordenar, pero no hay mucha diferencia un poco de desorden o mucho." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Yo lo sigo. " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Un espacio con un cristal intacto. Esto es mucho mejor de lo que esperaba. �Qu� clase de estudio realiza en este lugar, se�or? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Religioso. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego d�game cu�nto puede pagar y, no s�, le avisar� cuando florezca el chu�o.  " }
        }},
        {DialogTypes.DialogType.DIALOG_3, new dialogLinesScript[15]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Qu� tal? �Cu�nto crees que vale este vertedero?" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Vale un cielo de noche" },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Qu�? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eso digo yo. �Qu� sab�as t� de Claudio?" },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Claudio? ��C�mo sabes t�?!" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Me contrataron para encontrar a sus hijos. No me dir�s que est�n bajo esa mesa que tienes ah�, �no? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Yo no s� nada! T� no eres de ninguna empresa, �no?, ahora mismo vas dejar mi casa..." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Aunque a la gente no le parece, usualmente soy muy profesional. �Sabes qu� significa eso? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Y... Yo... no he hecho nada... Claudio fue el que quiso cambiar los par�metros." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Yo estaba... estoy por llegar a una conclusi�n... hasta he cambiado mi centro neuronal para ver mejor lo que... lo que tenemos que ver... �l es el que ha insistido en usar el ritual de los ni�os perdidos... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "D�jame terminar, cabr�n. Soy una profesional porque soy capaz de justificar la cantidad de dinero que pido a mis contratantes." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eficiencia es la palabra. Y hay cosas que son tan ineficientes que me horrorizan si me contratas a m� misma. Un interrogatorio con un robot demente, por ejemplo. O un cad�ver por ac� y all�, nunca es algo bueno." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Por favor... yo no s� d�nde se ha ido... pero s� m�s o menos lo que quer�a... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Y claro que sabes, s�lo que no voy a perder tiempo sac�ndole la informaci�n. Ahora, respira profundo, Amarillo" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "As� que clonaste tus propios �rganos. Y vibran� " },
        }},
        {DialogTypes.DialogType.DIALOG_4, new dialogLinesScript[1]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "..... ..... ...... ......" }
        }},
    };

    [System.Serializable]
    public class actorDialogVoice
    {
        public DialogTypes.ActorType actor;
        public AudioClip actorVoice;
    }

    [SerializeField] private actorDialogVoice[] actorsVoiceList; 

    private int currentDialogLine = 1;
    
    [SerializeField] private GameObject dialogBackground;
    [SerializeField] private GameObject[] dialogActorsImages;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] private GameObject talkButton;

    [SerializeField] private GameObject messageBackground;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] private GameObject initBackground;
    [SerializeField] private GameObject creditsBackground;

    private AudioSource audioSource;
    [SerializeField] private float typingTime;
    [SerializeField] private float charsToPlaySound;
    public bool dialogActive;

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
    }

    private void Start()
    {
        dialogController = new DialogInputActions();
        dialogController.Dialog.NextDialog.started += ctx => NextDialog();
        dialogController.Dialog.PreviousDialog.started += ctx => PreviousDialog();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (initBackground.activeInHierarchy)
            {
                Debug.Log("hide init");
                initBackground.SetActive(false);
            }
        }
    }

    public void ShowDialog()
    {
        dialogController.Enable();
        dialogActive = true;
        dialogBackground.SetActive(true);
        dialogLines = diologScript[currentDialog];
        currentDialogLine = 0;
        SetDialogText(currentDialogLine);

        PlayerController.instance.isTalking = true;
        Talk(false);
    }

    public void CloseDialog()
    {
        dialogController.Disable();
        dialogActive = false;
        dialogBackground.SetActive(false);
        currentDialogLine = 0;

        PlayerController.instance.isTalking = false;
        Talk(true);
    }

    private void NextDialog()
    {
        currentDialogLine++;
        SetDialogText(currentDialogLine);
    }

    private void PreviousDialog()
    {
        currentDialogLine--;
        if (currentDialogLine < 0)
        {
            currentDialogLine = 0;
        }
        SetDialogText(currentDialogLine);
    }

    private void SetDialogText (int currentDialogLine)
    {
        messageBackground.SetActive(false);
        StopAllCoroutines();
        if (currentDialog == DialogTypes.DialogType.DIALOG_2)
        {
            PlayerController.instance.GetComponent<AudioSource>().clip = PlayerController.instance.audioClip;
            PlayerController.instance.GetComponent<AudioSource>().Play();
        }
        if (currentDialogLine >= 0 && currentDialogLine < dialogLines.Length)
        {
            DialogTypes.ActorType talkingActor = dialogLines[currentDialogLine].actor;

            foreach (GameObject dialogActor in dialogActorsImages)
            {
                if (talkingActor == dialogActor.GetComponent<DialogTypes>().actorType)
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

            if (QuestManager.instance.currentQuest.quest != null && QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_0_INIT)
            {
                QuestManager.instance.QuestCompleted();
            }
            if (QuestManager.instance != null)
            {
                QuestManager.instance.QuestStarted();
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

    public void ToogleMessage()
    {
        StopCoroutine(CloseMessage());
        messageBackground.SetActive(!messageBackground.activeInHierarchy);
    }

    public void ShowMessage(string messageText)
    {
        messageBackground.SetActive(true);
        message.text = messageText;
        StartCoroutine(CloseMessage());
    }

    IEnumerator CloseMessage()
    {
        yield return new WaitForSeconds(3);
        messageBackground.SetActive(false);
    }

    public IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(1);
        creditsBackground.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Desert");
    }

    public void Talk(bool wantTalk)
    {
        talkButton.SetActive(wantTalk);
    }

}
