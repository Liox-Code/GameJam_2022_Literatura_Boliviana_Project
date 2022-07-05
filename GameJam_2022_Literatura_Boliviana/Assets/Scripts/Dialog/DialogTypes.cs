using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTypes : MonoBehaviour
{
    public enum ActorType
    {
        CARMILA,
        AMARILLO
    }

    public ActorType actorType;
    public enum DialogType
    {
        DIALOG_1,
        DIALOG_2,
        DIALOG_3,
        DIALOG_4
    }

    public DialogType dialogType;
}
