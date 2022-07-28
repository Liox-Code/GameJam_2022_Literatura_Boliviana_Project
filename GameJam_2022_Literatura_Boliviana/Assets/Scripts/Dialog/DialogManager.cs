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
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Se�orita?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Este� disculpe�  quer�a saber si conoce qui�n habitaba o usaba aquel edificio."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Hace tiempo que vivo yo solo. �Por qu�? La licencia y los catastros est�n en orden�"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Represento a Hidrotratamientos LN Iven. Quer�amos constatar el estado de su propiedad para comprarla."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Mi empresa desea usar este terreno para un nuevo modelo de planta generadora de agua, basado en el tipo de contaminaci�n particulada de esta parte de la ciudad."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "La secci�n de presupuestos me ha autorizado a ofrecer 400 mil latinos por el terreno."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Ya ni saben qu� comprar� �No dec�a que por la casa?  "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "No hay habitantes en al menos tres hect�reas cuadradas, as� que consideraremos que el terreno le pertenece�"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "S�, s�, los muy desgraciados vienen a quitarnos nuestras tierras para luego revend�rnoslas m�s caras. Voy a hacer que fije el precio por la oportunidad de rechazar su sucio dinero. Venga, le mostrar�."}
        }},
        {DialogTypes.DialogType.DIALOG_2, new dialogLinesScript[10]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Hace mucho instalaron hasta una capilla aqu�, �qu� le parece? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Pintoresca. Creo que estas im�genes podr�an valuar m�s el costo de la propiedad, se�or... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Amarillo. "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Amarillo... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "As�, sin apellido. Ah� en la polic�a me censaron por el n�mero de serie de mis partes y ahora mi nombre oficial es un mont�n de n�meros. Hay que ser un infeliz para hacerle eso a alguien."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Se�or Amarillo, �me permite revisar los espacios? Luego, si el precio le parece bien, recomiendo..."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Esto es mi sala de estudio, casi todo lo dem�s son escombros, he estado tratando de limpiar, pero ya casi no veo la diferencia entre un poco de desorden o mucho."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Bueno, cualquiera, �no? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Ah, tiene un cristal intacto, mucho mejor de lo que esper�bamos. �Qu� clase de estudio realiza en este lugar, se�or? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Teol�gico. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego d�game cu�nto ofrecen para que la mande a volar." }
        }},
        {DialogTypes.DialogType.DIALOG_3, new dialogLinesScript[14]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Qu� tal? �Cu�nto vale este vertedero?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Lo que un cielo de noche"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Qu�? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eso digo yo. �Qu� sab�as t� de Claudio?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Claudio? ��C�mo sabes t�?!"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Me contrataron para encontrar a sus hijos."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "�Yo no s� nada! T� no eres de ninguna empresa, ahora mismo vas dejar mi casa..."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Aunque a la gente no le parece, usualmente soy muy profesional. �Sabes qu� significa eso? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Y... Yo... no he hecho nada... Claudio fue el que quiso cambiar los par�metros."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Yo estaba... estoy por llegar a una conclusi�n... hasta he cambiado mi centro neuronal para ver mejor lo que... lo que tenemos que ver... �l es el que ha insistido en usar el ritual de los ni�os perdidos... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "D�jame terminar. Soy una profesional porque soy capaz de justificar la cantidad de dinero que pido a mis contratantes."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eficiencia es la palabra. Y hay cosas que son tan ineficientes que me horrorizan. Un interrogatorio a un robot demente, por ejemplo."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Por favor... yo no s� d�nde se ha ido... pero s� m�s o menos lo que quer�a... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Y claro que sabes, s�lo que no voy a perder tiempo pregunt�ndote. Ahora, respira profundo, Amarillo"}
        }},
        {DialogTypes.DialogType.DIALOG_4, new dialogLinesScript[3]{
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "As� que� clonaste tus propios �rganos. �Qu� m�quina esperabas fabricar?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "..... ..... ...... ......" },
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "..... ..... ...... ...... ...... ...... ...... " }
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
    public bool dialogActive;
    private bool showAllText;
    private IEnumerator showLine;

    [SerializeField] private GameObject messageBackground;
    [SerializeField] TextMeshProUGUI message;

    private AudioSource audioSource;
    [SerializeField] private float typingTime;
    [SerializeField] private float charsToPlaySound;

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

    private void OnEnable()
    {
        dialogController = new DialogInputActions();
        dialogController.Dialog.NextDialog.started += ctx => NextDialog();
        dialogController.Dialog.PreviousDialog.started += ctx => PreviousDialog();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        if (dialogController != null)
        {
            if (dialogController.Dialog.enabled) dialogController.Dialog.Disable();
        }
    }

    private void Update()
    {
        if (GameManager.instance.currentScene == "MusicPuzzle" || GameManager.instance.currentScene == "Init" || GameManager.instance.currentScene == "Credits" || GameManager.instance.currentScene == null)
        {
            messageBackground.SetActive(false);
            Talk(false);
        }
        else
        {
            messageBackground.SetActive(true);
        }

        //if (Keyboard.current.anyKey.wasPressedThisFrame)
        //{
        //    if (initBackground.activeInHierarchy)
        //    {
        //        initBackground.SetActive(false);
        //    }
        //}
    }

    public void ShowDialog()
    {
        if (!dialogActive)
        {
            dialogController.Dialog.Enable();
            dialogActive = true;
            dialogBackground.SetActive(true);
            dialogLines = diologScript[currentDialog];
            currentDialogLine = 0;
            SetDialogText(currentDialogLine);

            PlayerController.instance.isTalking = true;
            Talk(false);
        }
    }

    public void CloseDialog()
    {
        if (dialogActive)
        {
            dialogController.Dialog.Disable();
            dialogActive = false;
            dialogBackground.SetActive(false);
            currentDialogLine = 0;

            PlayerController.instance.isTalking = false;
            //Talk(true);
        }
    }

    private void NextDialog()
    {
        if (!showAllText)
        {
            showAllText = true;
            dialogText.text = dialogLines[currentDialogLine].dialogLine;
            StopCoroutine(showLine);
            return;
        }
        showAllText = false;
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
        StopAllCoroutines();
        if (currentDialog == DialogTypes.DialogType.DIALOG_3 && PlayerController.instance.GetComponent<AudioSource>().clip != PlayerController.instance.audioClip)
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
            showLine = ShowLine(dialogLines[currentDialogLine].dialogLine);
            StartCoroutine(showLine);
        }
        if (currentDialogLine >= dialogLines.Length)
        {
            CloseDialog();
            if (QuestManager.instance != null)
            {
                if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_MUSIC_PUZZLE)
                {
                    GameManager.instance.changeScene("MusicPuzzle");
                }

                if (QuestManager.instance.currentQuest.quest.questId == QuestType.QuestId.QUEST_INIT)
                {
                    QuestManager.instance.QuestCompleted();
                }
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
        //StopCoroutine(CloseMessage());
        messageBackground.SetActive(!messageBackground.activeInHierarchy);
    }

    public void ShowMessage(string messageText)
    {
        messageBackground.SetActive(true);
        message.text = messageText;
        //StartCoroutine(CloseMessage());
    }

    //IEnumerator CloseMessage()
    //{
    //    yield return new WaitForSeconds(3);
    //    messageBackground.SetActive(false);
    //}

    public void Talk(bool wantTalk)
    {
        talkButton.SetActive(wantTalk);
    }

}
