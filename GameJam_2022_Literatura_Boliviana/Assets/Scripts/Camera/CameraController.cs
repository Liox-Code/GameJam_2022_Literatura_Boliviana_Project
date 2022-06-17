using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField]
    public GameObject followTarget;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float cameraSpeed = 4.0f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        if(followTarget != null)
        {
            Vector3 followTransform = followTarget.transform.position; 
            targetPosition = new Vector3(followTransform.x, followTransform.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }
}
