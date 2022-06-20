using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private Rigidbody2D npcRigidbody2D;
    private BoxCollider2D npcBoxCollider2D;

    [SerializeField] private bool isWalking;
    public bool isTalking;

    [SerializeField] private float walkTime = 1.5f;
    [SerializeField] private float walkCounter;

    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private float waitCounter;

    class walkingDirection
    {
        public Vector2 Direction { get; set; }
        public bool IsPosible { get; set; }
    }
    private Dictionary<string, walkingDirection> direction = new Dictionary<string, walkingDirection>()
    {
        { "Top", new walkingDirection {Direction = new Vector2(0,1), IsPosible = true}},
        { "Right", new walkingDirection {Direction = new Vector2(1,0), IsPosible = true}},
        { "Bottom", new walkingDirection {Direction = new Vector2(0,-1), IsPosible = true}},
        { "Left", new walkingDirection {Direction = new Vector2(-1,0), IsPosible = true}}
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
        //if (!DialogManager.instance.dialogActive)
        //{
        //    isTalking = false;
        //}
        //if (!isTalking)
        //{
        //    StopWalking();
        //    return;
        //}
        //if (isWalking)
        //{
        //    Debug.Log("Walking");
        //    if (npcWalkingZone != null)
        //    {
        //        //Debug.Log($"{npcBoxCollider2D.bounds.min.x} <= {npcWalkingZone.bounds.min.x} =={(npcBoxCollider2D.bounds.min.x <= npcWalkingZone.bounds.min.x)}");
        //        //Debug.Log($"{npcBoxCollider2D.bounds.max.x} >= {npcWalkingZone.bounds.max.x} =={(npcBoxCollider2D.bounds.max.x >= npcWalkingZone.bounds.max.x)}");
        //        //Debug.Log($"{npcBoxCollider2D.bounds.min.y} <= {npcWalkingZone.bounds.min.y} =={(npcBoxCollider2D.bounds.min.y <= npcWalkingZone.bounds.min.y)}");
        //        //Debug.Log($"{npcBoxCollider2D.bounds.max.y} >= {npcWalkingZone.bounds.max.y} =={(npcBoxCollider2D.bounds.max.y >= npcWalkingZone.bounds.max.y)}");
        //        if (
        //            npcBoxCollider2D.bounds.min.x <= npcWalkingZone.bounds.min.x ||
        //            npcBoxCollider2D.bounds.max.x >= npcWalkingZone.bounds.max.x ||
        //            npcBoxCollider2D.bounds.min.y <= npcWalkingZone.bounds.min.y ||
        //            npcBoxCollider2D.bounds.max.y >= npcWalkingZone.bounds.max.y
        //            )
        //        {
        //            npcRigidbody2D.velocity = Vector2.zero;
        //            return;
        //        }
        //    }

        //    npcRigidbody2D.velocity = walkingDirection[currentDirection] * speed;

        //    walkCounter -= Time.deltaTime;
        //    if (walkCounter < 0)
        //    {
        //        StopWalking();
        //    }
        //    Debug.Log("FinishWWalk");
        //}
        //else
        //{
        //    npcRigidbody2D.velocity = Vector2.zero;

        //    waitCounter -= Time.deltaTime;
        //    if (waitCounter < 0)
        //    {
        //        StartWalking();
        //    }
        //}
        //StartWalking();
    }

    private void StartWalking()
    {
        isWalking = true;
        //currentDirection = Random.Range(0, walkingDirection.Length);

        //currentDirection = direction.TryGetValue("Top", out lol );
        walkCounter = walkTime;
    }

    private void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
    }
}
