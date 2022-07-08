using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector2 facingDirection = Vector2.zero;

    [SerializeField] private string placeName;

    private void Start()
    {

        if (!PlayerController.instance.nextPlaceName.Equals(placeName))
        {
            return;
        }

        PlayerController.instance.transform.position = this.transform.position;
        CameraController.instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, CameraController.instance.transform.position.z);

        PlayerController.instance.lastMovement = facingDirection;
    }
}
