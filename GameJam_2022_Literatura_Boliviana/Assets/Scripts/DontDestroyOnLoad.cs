using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerController.instance != null)
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(this.transform.gameObject);
    }
}
