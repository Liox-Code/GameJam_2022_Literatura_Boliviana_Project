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

    public string currentDialog;
    Dictionary<string, dialogLinesScript[]> diologScript = new Dictionary<string, dialogLinesScript[]>()
    {
        {"Dialog_0", new dialogLinesScript[9]{
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Se�orita?" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Este� disculpe�  quer�a saber si conoce qui�n habitaba o usaba aquel edificio." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Hace tiempo que vivo yo solo. �Por qu�? La licencia y los catastros est�n en orden�" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Represento a Hidrotratamientos LN Iven. Quer�amos constatar el estado de su propiedad para comprarla." },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Mi empresa desea usar este terreno para un nuevo modelo de planta generadora de agua, basado en el tipo de contaminaci�n particulada de esta parte de la ciudad." },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "La secci�n de presupuestos me ha autorizado a ofrecer 400 mil latinos por el terreno." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Ya ni saben qu� comprar� �No dec�a que por la casa?  " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Consideraremos que el terreno le pertenece. No hay habitantes en al menos tres hect�reas cuadradas." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "S�, s�, los muy hijos de puta vienen a quitarnos nuestras tierras para cambiarlas y luego revend�rnoslas m�s caras. Voy a hacer que fije el precio por la oportunidad de rechazar su dinero de mierda. Venga, le mostrar�." }
        }},
        {"Dialog_1", new dialogLinesScript[10]{
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Y creo que quer�an quedarse un buen tiempo. Mire nom�s que instalaron hasta una capilla aqu�. �Qu� le parece? " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Pintoresca. Creo que estas im�genes podr�an valuar m�s el costo de la propiedad, se�or... " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Amarillo. " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Amarillo... " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "As�, sin apellido. Ah� en la polic�a me censaron por el n�mero de serie de mis partes y ahora mi nombre oficial es un mont�n de n�meros. Hay que ser hijo de puta para hacerle eso a alguien. " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Se�or Amarillo, �podemos ver otros espacios de su propiedad? Necesito por lo menos un setenta por ciento antes de dar un estimado. Luego, si el precio le parece bien, recomiendo..." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Vamos a ver mi sala de estudio, casi todo lo dem�s son escombros, he  estado tratando de ordenar, pero no hay mucha diferencia un poco de desorden o mucho." },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Yo lo sigo. " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Un espacio con un cristal intacto. Esto es mucho mejor de lo que esperaba. �Qu� clase de estudio realiza en este lugar, se�or? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Religioso. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego d�game cu�nto puede pagar y, no s�, la mandar� a la mierda.  " }
        }},
        {"Dialog_2", new dialogLinesScript[15]{
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Qu� tal? �Cu�nto crees que vale este mont�n de mierda?" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Vale un cielo de noche" },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Qu�? " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Eso digo yo. �Qu� sab�as t� de Claudio?" },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Claudio? ��C�mo sabes t�?!" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Me contrataron para encontrar a sus hijos. No me dir�s que est�n bajo esa mesa que tienes ah�, �no? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "�Yo no s� nada! T� no eres de ninguna empresa, �no?, ahora mismo vas dejar mi casa..." },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Aunque a la gente no le parece, usualmente soy muy profesional. �Sabes qu� significa eso? " },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Y... Yo... no he hecho nada... Claudio fue el que quiso cambiar los par�metros." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Yo estaba... estoy por llegar a una conclusi�n... hasta he cambiado mi centro neuronal para ver mejor lo que... lo que tenemos que ver... �l es el que ha insistido en usar el ritual de los ni�os perdidos... " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "D�jame terminar, cabr�n. Soy una profesional porque soy capaz de justificar la cantidad de dinero que pido a mis contratantes." },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Eficiencia es la palabra. Y hay cosas que son tan ineficientes que me horrorizan si me contratas a m� misma. Un interrogatorio con un robot demente, por ejemplo. O un cad�ver por ac� y all�, nunca es algo bueno." },
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "Por favor... yo no s� d�nde se ha ido... pero s� m�s o menos lo que quer�a... " },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "Y claro que sabes, s�lo que no voy a perder tiempo sac�ndole la informaci�n. Ahora, respira profundo, Amarillo" },
            new dialogLinesScript { actor = DialogActors.ActorType.CARMILA, dialogLine = "As� que clonaste tus propios �rganos. Y vibran� " },
        }},
        {"Dialog_3", new dialogLinesScript[1]{
            new dialogLinesScript { actor = DialogActors.ActorType.AMARILLO, dialogLine = "..... ..... ...... ......" }
        }},
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

    [SerializeField] private GameObject messageBakground;
    [SerializeField] TextMeshProUGUI message;

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

        currentDialog = "Dialog_0";
        ShowMessage("Habla con el robot Amarillo.");
    }

    public void ShowDialog()
    {
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
        currentDialogLine--;
        if (currentDialogLine < 0)
        {
            currentDialogLine = 0;
        }
        setDialogText(currentDialogLine);
    }

    private void setDialogText (int currentDialogLine)
    {
        messageBakground.SetActive(false);
        StopAllCoroutines();
        if (currentDialog == "Dialog_2")
        {
            PlayerController.instance.GetComponent<AudioSource>().clip = PlayerController.instance.audioClip;
            PlayerController.instance.GetComponent<AudioSource>().Play();
        }
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

            foreach (QuestManager.QuestObject questObject in QuestManager.instance.questObject)
            {
                if (questObject.quest == null)
                {
                    continue;
                }

                //Debug.Log(!questObject.quest.gameObject.activeInHierarchy);
                //Debug.Log(!questObject.questState);

                if (currentDialog == questObject.questDialog
                    && !questObject.quest.gameObject.activeInHierarchy
                    && !questObject.questState)
                {
                    questObject.quest.gameObject.SetActive(true);
                    questObject.quest.StartQuest();
                }
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
        messageBakground.SetActive(!messageBakground.activeInHierarchy);
    }

    public void ShowMessage(string messageText)
    {
        messageBakground.SetActive(true);
        message.text = messageText;
        StartCoroutine(CloseMessage());
    }

    IEnumerator CloseMessage()
    {
        yield return new WaitForSeconds(3);
        messageBakground.SetActive(false);
    }

}
