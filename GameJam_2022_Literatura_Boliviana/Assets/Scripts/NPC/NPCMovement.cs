using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private Rigidbody2D npcRigidbody2D;
    private BoxCollider2D npcBoxCollider2D;

    [SerializeField] private bool isWalking;

    [SerializeField] private float walkTime = 1.5f;
    [SerializeField] private float walkCounter;

    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private float waitCounter;

    private Vector2[] walkingDirection =
    {
        new Vector2(1,0),
        new Vector2(-1,0),
        new Vector2(0,1),
        new Vector2(0,-1)
    };

    private int currentDirection;

    public BoxCollider2D npcWalkingZone;

    private void Start()
    {
        npcRigidbody2D = GetComponent<Rigidbody2D>();
        npcBoxCollider2D = GetComponent<BoxCollider2D>();
        walkCounter = walkTime;
        waitCounter = waitTime;
    }

    private void Update()
    {
        if (isWalking)
        {
            if (npcWalkingZone != null)
            {
                //Debug.Log($"{npcBoxCollider2D.bounds.min.x} <= {npcWalkingZone.bounds.min.x} =={(npcBoxCollider2D.bounds.min.x <= npcWalkingZone.bounds.min.x)}");
                //Debug.Log($"{npcBoxCollider2D.bounds.max.x} >= {npcWalkingZone.bounds.max.x} =={(npcBoxCollider2D.bounds.max.x >= npcWalkingZone.bounds.max.x)}");
                //Debug.Log($"{npcBoxCollider2D.bounds.min.y} <= {npcWalkingZone.bounds.min.y} =={(npcBoxCollider2D.bounds.min.y <= npcWalkingZone.bounds.min.y)}");
                //Debug.Log($"{npcBoxCollider2D.bounds.max.y} >= {npcWalkingZone.bounds.max.y} =={(npcBoxCollider2D.bounds.max.y >= npcWalkingZone.bounds.max.y)}");
                if (
                    npcBoxCollider2D.bounds.min.x <= npcWalkingZone.bounds.min.x ||
                    npcBoxCollider2D.bounds.max.x >= npcWalkingZone.bounds.max.x ||
                    npcBoxCollider2D.bounds.min.y <= npcWalkingZone.bounds.min.y ||
                    npcBoxCollider2D.bounds.max.y >= npcWalkingZone.bounds.max.y
                    )
                {
                    npcRigidbody2D.velocity = Vector2.zero;
                    StopWalking();
                }
            }

            npcRigidbody2D.velocity = walkingDirection[currentDirection] * speed;

            walkCounter -= Time.deltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            npcRigidbody2D.velocity = Vector2.zero;

            waitCounter -= Time.deltaTime;
            if (waitCounter < 0)
            {
                StartWalking();
            }
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        currentDirection = Random.Range(0, walkingDirection.Length);
        walkCounter = walkTime;
    }

    private void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
    }
}
