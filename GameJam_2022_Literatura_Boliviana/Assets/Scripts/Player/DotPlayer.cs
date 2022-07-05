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

    private void Start()
    {
        dotPlayerController = new DotPlayerController();
        dotPlayerController.DotPlayer_1.Movement.Enable();
        dotPlayerController.DotPlayer_2.Movement.Enable();

        rbDotPlayer = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        dotPlayerController.DotPlayer_1.Movement.Disable();
        dotPlayerController.DotPlayer_2.Movement.Disable();
    }

    private void Update()
    {
        if (DotPlayerTypes.dotPlayer.Player_1 == dotPlayerType)
        {
            direction = dotPlayerController.DotPlayer_1.Movement.ReadValue<Vector2>();
        }
        if (DotPlayerTypes.dotPlayer.Player_2 == dotPlayerType)
        {
            direction = dotPlayerController.DotPlayer_2.Movement.ReadValue<Vector2>();
        }
        rbDotPlayer.velocity = direction * speed;
    }
}
