using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public QuestType.QuestId questId;

    public void StartQuest()
    {
        if (questId == QuestType.QuestId.QUEST_INIT)
        {
            DialogManager.instance.currentDialog = DialogTypes.DialogType.DIALOG_1;
            DialogManager.instance.ShowMessage("Habla con el robot Amarillo.");
        }
        if (questId == QuestType.QuestId.QUEST_INITIAL_CONVERSATION)
        {
            DialogManager.instance.currentDialog = DialogTypes.DialogType.DIALOG_2;
            DialogManager.instance.ShowMessage("Habla con el robot Amarillo dentro de su casa.");
        }
        if (questId == QuestType.QuestId.QUEST_WATCH_THROUGH_WINDOW)
        {
            DialogManager.instance.ShowMessage("Ve a la ventana central en la casa de Amarillo.");
        }
        if (questId == QuestType.QuestId.QUEST_MUSIC_PUZZLE)
        {
            DialogManager.instance.currentDialog = DialogTypes.DialogType.DIALOG_3;
            DialogManager.instance.ShowMessage("Vuelve a hablar con Amarillo.");
        }
        if (questId == QuestType.QuestId.QUEST_FIND_HEARTH)
        {

            DialogManager.instance.currentDialog = DialogTypes.DialogType.DIALOG_4;
            DialogManager.instance.ShowMessage("Encuentra y recoge una bolsa de organos alrededor de la casa de Amarillo.");
        }
    }

    public void CompleteQuest()
    {
        if (questId == QuestType.QuestId.QUEST_WATCH_THROUGH_WINDOW || questId == QuestType.QuestId.QUEST_MUSIC_PUZZLE)
        {
            QuestManager.instance.QuestStarted();
        }
    }

}
