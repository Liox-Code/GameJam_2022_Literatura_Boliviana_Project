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
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Señorita?" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Este… disculpe…  quería saber si conoce quién habitaba o usaba aquel edificio." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Hace tiempo que vivo yo solo. ¿Por qué? La licencia y los catastros están en orden…" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Represento a Hidrotratamientos LN Iven. Queríamos constatar el estado de su propiedad para comprarla." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Mi empresa desea usar este terreno para un nuevo modelo de planta generadora de agua, basado en el tipo de contaminación particulada de esta parte de la ciudad." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "La sección de presupuestos me ha autorizado a ofrecer 400 mil latinos por el terreno." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Ya ni saben qué comprar… ¿No decía que por la casa?  " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Consideraremos que el terreno le pertenece. No hay habitantes en al menos tres hectáreas cuadradas." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Sí, sí, los muy desgraciados vienen a quitarnos nuestras tierras para cambiarlas y luego revendérnoslas más caras. Voy a hacer que fije el precio por la oportunidad de rechazar su sucio dinero. Venga, le mostraré." }
        }},
        {DialogTypes.DialogType.DIALOG_2, new dialogLinesScript[10]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Y creo que querían quedarse un buen tiempo. Mire nomás que instalaron hasta una capilla aquí. ¿Qué le parece? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Pintoresca. Creo que estas imágenes podrían valuar más el costo de la propiedad, señor... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Amarillo. " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Amarillo... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Así, sin apellido. Ahí en la policía me censaron por el número de serie de mis partes y ahora mi nombre oficial es un montón de números. Hay que ser un infeliz para hacerle eso a alguien. " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Señor Amarillo, ¿podemos ver otros espacios de su propiedad? Necesito por lo menos un setenta por ciento antes de dar un estimado. Luego, si el precio le parece bien, recomiendo..." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Vamos a ver mi sala de estudio, casi todo lo demás son escombros, he  estado tratando de ordenar, pero no hay mucha diferencia un poco de desorden o mucho." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Yo lo sigo. " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Un espacio con un cristal intacto. Esto es mucho mejor de lo que esperaba. ¿Qué clase de estudio realiza en este lugar, señor? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Religioso. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego dígame cuánto puede pagar y, no sé, le avisaré cuando florezca el chuño.  " }
        }},
        {DialogTypes.DialogType.DIALOG_3, new dialogLinesScript[15]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Qué tal? ¿Cuánto crees que vale este vertedero?" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Vale un cielo de noche" },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Qué? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eso digo yo. ¿Qué sabías tú de Claudio?" },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Claudio? ¡¿Cómo sabes tú?!" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Me contrataron para encontrar a sus hijos. No me dirás que están bajo esa mesa que tienes ahí, ¿no? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¡Yo no sé nada! Tú no eres de ninguna empresa, ¿no?, ahora mismo vas dejar mi casa..." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Aunque a la gente no le parece, usualmente soy muy profesional. ¿Sabes qué significa eso? " },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Y... Yo... no he hecho nada... Claudio fue el que quiso cambiar los parámetros." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Yo estaba... estoy por llegar a una conclusión... hasta he cambiado mi centro neuronal para ver mejor lo que... lo que tenemos que ver... él es el que ha insistido en usar el ritual de los niños perdidos... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Déjame terminar, cabrón. Soy una profesional porque soy capaz de justificar la cantidad de dinero que pido a mis contratantes." },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eficiencia es la palabra. Y hay cosas que son tan ineficientes que me horrorizan si me contratas a mí misma. Un interrogatorio con un robot demente, por ejemplo. O un cadáver por acá y allá, nunca es algo bueno." },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Por favor... yo no sé dónde se ha ido... pero sé más o menos lo que quería... " },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Y claro que sabes, sólo que no voy a perder tiempo sacándole la información. Ahora, respira profundo, Amarillo" },
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Así que clonaste tus propios órganos. Y vibran… " },
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
