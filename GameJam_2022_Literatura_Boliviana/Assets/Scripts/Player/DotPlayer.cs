using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DotPlayer : MonoBehaviour
{

    public DotPlayerTypes.dotPlayer dotPlayerType;
    public float speed = 1f;

    DotPlayerController dotPlayerController;

    private Vector2 direction;
    private Rigidbody2D rbDotPlayer;

    private void OnEnable()
    {
        dotPlayerController = new DotPlayerController();
        dotPlayerController.DotPlayer_1.Movement.Enable();

        rbDotPlayer = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        dotPlayerController.DotPlayer_1.Movement.Disable();
    }

    private void Update()
    {
        if (DotPlayerTypes.dotPlayer.Player_1 == dotPlayerType)
        {
            direction = dotPlayerController.DotPlayer_1.Movement.ReadValue<Vector2>();
        }
        rbDotPlayer.velocity = direction * speed;
    }
}
