using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GemMovement : MonoBehaviour, IClick
{
    [SerializeField] private float initialVelocity = 4f;
    [Range(1.1f,6)][SerializeField] private float minVelocity = 2;
    [Range(6, 10)] [SerializeField] private float maxVelocity = 6;

    [SerializeField] private Vector2 currentVelocity;

    private float velocityMultiplier = 1.1f;
    private float velocityDivider = 1.1f;

    private Rigidbody2D rbGem;

    private void Awake()
    {
        GemGenerator.OnUpdateCurrentGemType += activateCircle;
    }

    private void Start()
    {
        rbGem = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void OnDisable()
    {
        GemGenerator.OnUpdateCurrentGemType -= activateCircle;
    }

    private void Update()
    {
        currentVelocity = rbGem.velocity;
    }

    private void Launch()
    {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;

        rbGem.velocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    public void onClickAction()
    {
        GemGenerator.OnUpdateCurrentGemType -= activateCircle;
        Destroy(gameObject);
    }

    private void activateCircle()
    {
        bool isActive = (GemGenerator.instance.activeGemType.gemTypes == gameObject.GetComponent<GemType>().gemTypes);
        gameObject.transform.Find("Circle").gameObject.SetActive(isActive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(rbGem.velocity.magnitude);
        if (collision.gameObject.CompareTag("Wall"))
        {
            rbGem.velocity = ClampMagnitudeMaxMin(rbGem.velocity / velocityDivider, maxVelocity, minVelocity);
        }
        if (collision.gameObject.CompareTag("Gem"))
        {

            rbGem.velocity = ClampMagnitudeMaxMin(rbGem.velocity * velocityMultiplier, maxVelocity, minVelocity);
        }
    }

    public static Vector3 ClampMagnitudeMaxMin(Vector3 v, float max, float min)
    {
        //El valor minimo debe ser 1.1 o el valor se ira al 0
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
}
