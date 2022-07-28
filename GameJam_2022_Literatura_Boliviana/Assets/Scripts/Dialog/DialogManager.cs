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
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Señorita?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Este… disculpe…  quería saber si conoce quién habitaba o usaba aquel edificio."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Hace tiempo que vivo yo solo. ¿Por qué? La licencia y los catastros están en orden…"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Represento a Hidrotratamientos LN Iven. Queríamos constatar el estado de su propiedad para comprarla."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Mi empresa desea usar este terreno para un nuevo modelo de planta generadora de agua, basado en el tipo de contaminación particulada de esta parte de la ciudad."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "La sección de presupuestos me ha autorizado a ofrecer 400 mil latinos por el terreno."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Ya ni saben qué comprar… ¿No decía que por la casa?  "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "No hay habitantes en al menos tres hectáreas cuadradas, así que consideraremos que el terreno le pertenece…"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Sí, sí, los muy desgraciados vienen a quitarnos nuestras tierras para luego revendérnoslas más caras. Voy a hacer que fije el precio por la oportunidad de rechazar su sucio dinero. Venga, le mostraré."}
        }},
        {DialogTypes.DialogType.DIALOG_2, new dialogLinesScript[10]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Hace mucho instalaron hasta una capilla aquí, ¿qué le parece? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Pintoresca. Creo que estas imágenes podrían valuar más el costo de la propiedad, señor... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Amarillo. "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Amarillo... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Así, sin apellido. Ahí en la policía me censaron por el número de serie de mis partes y ahora mi nombre oficial es un montón de números. Hay que ser un infeliz para hacerle eso a alguien."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Señor Amarillo, ¿me permite revisar los espacios? Luego, si el precio le parece bien, recomiendo..."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Esto es mi sala de estudio, casi todo lo demás son escombros, he estado tratando de limpiar, pero ya casi no veo la diferencia entre un poco de desorden o mucho."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Bueno, cualquiera, ¿no? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Ah, tiene un cristal intacto, mucho mejor de lo que esperábamos. ¿Qué clase de estudio realiza en este lugar, señor? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Teológico. Puede ver lo que quiera, yo tengo que terminar un informe. Le doy un par de minutos. Luego dígame cuánto ofrecen para que la mande a volar." }
        }},
        {DialogTypes.DialogType.DIALOG_3, new dialogLinesScript[14]{
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Qué tal? ¿Cuánto vale este vertedero?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Lo que un cielo de noche"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Qué? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eso digo yo. ¿Qué sabías tú de Claudio?"},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¿Claudio? ¡¿Cómo sabes tú?!"},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Me contrataron para encontrar a sus hijos."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "¡Yo no sé nada! Tú no eres de ninguna empresa, ahora mismo vas dejar mi casa..."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Aunque a la gente no le parece, usualmente soy muy profesional. ¿Sabes qué significa eso? "},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Y... Yo... no he hecho nada... Claudio fue el que quiso cambiar los parámetros."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Yo estaba... estoy por llegar a una conclusión... hasta he cambiado mi centro neuronal para ver mejor lo que... lo que tenemos que ver... él es el que ha insistido en usar el ritual de los niños perdidos... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Déjame terminar. Soy una profesional porque soy capaz de justificar la cantidad de dinero que pido a mis contratantes."},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Eficiencia es la palabra. Y hay cosas que son tan ineficientes que me horrorizan. Un interrogatorio a un robot demente, por ejemplo."},
            new dialogLinesScript { actor = DialogTypes.ActorType.AMARILLO, dialogLine = "Por favor... yo no sé dónde se ha ido... pero sé más o menos lo que quería... "},
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Y claro que sabes, sólo que no voy a perder tiempo preguntándote. Ahora, respira profundo, Amarillo"}
        }},
        {DialogTypes.DialogType.DIALOG_4, new dialogLinesScript[3]{
            new dialogLinesScript { actor = DialogTypes.ActorType.CARMILA, dialogLine = "Así que… clonaste tus propios órganos. ¿Qué máquina esperabas fabricar?"},
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
