using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 4.0F;
    public string nextPlaceName;
    private bool isWalking = false;
    public bool isTalking = false;
    public Vector2 lastMovement = Vector2.zero;

    private float movementX;
    private float movementY;
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";
    private string lastHorizontal = "LastHorizontal";
    private string lastVertical = "LastVertical";
    private string walking = "Walking";

    private Animator animator;
    private Rigidbody2D playerRigidbody;

    public static PlayerController instance;

    private void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();
        movementX = movement.x;
        movementY = movement.y;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.transform.gameObject);

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isTalking)
        {
            playerRigidbody.velocity = Vector2.zero;
            animator.SetBool(walking, false);
            return;
        };

        isWalking = false;
        if (movementX > 0.5 || movementX < -0.5)
        {
            //this.transform.Translate(movementX * speed * Time.deltaTime, 0, 0);
            playerRigidbody.velocity = new Vector2(movementX * speed, 0);
            lastMovement = new Vector2(movementX,0);
            isWalking = true;
        }
        if (movementY > 0.5 || movementY < -0.5)
        {
            //this.transform.Translate(0, movementY * speed * Time.deltaTime, 0);
            playerRigidbody.velocity = new Vector2(0, movementY * speed );
            lastMovement = new Vector2(0, movementY);
            isWalking = true;
        }

        if (!isWalking)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        animator.SetFloat(horizontal, movementX);
        animator.SetFloat(vertical, movementY);
        animator.SetFloat(lastHorizontal, lastMovement.x);
        animator.SetFloat(lastVertical, lastMovement.y);
        animator.SetBool(walking, isWalking);
    }
}
