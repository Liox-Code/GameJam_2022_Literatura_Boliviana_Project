using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatController : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;

    private void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    private void Update()
    {
        if (hasStarted)
        {
            Debug.Log("Beat");
        }
    }

    private void OnMove(InputValue movementValue)
    {
        if (!hasStarted)
        {
           hasStarted = true;
        }
    }
}
