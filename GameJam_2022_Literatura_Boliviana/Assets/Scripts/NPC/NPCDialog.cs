using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    NPCInputActions npcController;
    private string currentDialog = "Dialog_1";

    private bool playerOnZone;

    private void Start()
    {
        npcController = new NPCInputActions();
        npcController.Enable();
        npcController.NPC.OpenDialog.started += ctx => TalkNPC();
    }

    private void OnDisable()
    {
        npcController.Disable();
    }

    private void TalkNPC()
    {
        if (playerOnZone)
        {
            DialogManager.instance.ShowDialog(currentDialog);
            if (gameObject.GetComponentInParent<NPCMovement>() != null)
            {
                gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnZone = false;
        }
    }
}
