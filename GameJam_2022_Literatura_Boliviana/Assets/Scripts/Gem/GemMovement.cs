using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GemMovement : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 4f;

    [SerializeField] private Vector2 currentVelocity;

    private float velocityMultiplier = 1.1f;
    private float velocityDivider = 1.1f;
    private bool isGemActive;
    private bool reduceCircleSizeEnabled = false;

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
        StopCoroutine(reduceCircleSize());
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

    private void activateCircle()
    {
        isGemActive = (GemGenerator.instance.activeGemType.gemTypes == gameObject.GetComponent<GemType>().gemTypes);
        gameObject.transform.Find("Circle").gameObject.SetActive(isGemActive);
        gameObject.transform.Find("Circle").gameObject.transform.localScale = new Vector3(2, 2, 0);
        if (!reduceCircleSizeEnabled)
        {
            StopCoroutine(reduceCircleSize());
            StartCoroutine(reduceCircleSize());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = rbGem.velocity.magnitude;
        Vector2 direction = rbGem.velocity.normalized;
        if (direction.x >= 0 && direction.x < 0.5)
        {
            direction.x = 0.5f;
        }
        if (direction.x < 0 && direction.x > -0.5)
        {
            direction.x = -0.5f;
        }
        if (direction.y >= 0 && direction.y < 0.5)
        {
            direction.y = 0.5f;
        }
        if (direction.y < 0 && direction.y > -0.5)
        {
            direction.y = -0.5f;
        }
        rbGem.velocity = ClampMagnitudeMaxMin(direction * speed * velocityMultiplier, GemGenerator.instance.maxVelocity, GemGenerator.instance.minVelocity);

        ////Debug.Log(rbGem.velocity.magnitude);
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    //rbGem.velocity = ClampMagnitudeMaxMin(rbGem.velocity / velocityDivider, maxVelocity, minVelocity);
        //}
        //if (collision.gameObject.CompareTag("Gem"))
        //{

        //    //rbGem.velocity = ClampMagnitudeMaxMin(rbGem.velocity * velocityMultiplier, GemGenerator.instance.maxVelocity, GemGenerator.instance.minVelocity);
        //}
        ////If hit a gameobject with a Gem tag
        if (collision.gameObject.CompareTag("DotPlayer"))
        {
            if (isGemActive)
            {
                GemGenerator.OnUpdateCurrentGemType -= activateCircle;
                GemGenerator.OnGemDestroy?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator reduceCircleSize()
    {
        reduceCircleSizeEnabled = true;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);;
            if (gameObject.transform.Find("Circle").gameObject.transform.localScale.x < 0 && gameObject.transform.Find("Circle").gameObject.transform.localScale.y < 0)
            {
                reduceCircleSizeEnabled = false;
                StopCoroutine(reduceCircleSize());
                break;
            }
            else
            {
                gameObject.transform.Find("Circle").gameObject.transform.localScale -= new Vector3(0.05f, 0.05f, 0);
            }
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
