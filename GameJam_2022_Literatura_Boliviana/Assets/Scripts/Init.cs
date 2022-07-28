using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    [SerializeField] Image CCELP;

    private void Start()
    {
        StartCoroutine(ImageHide());
    }

    IEnumerator ImageHide()
    {
        yield return new WaitForSeconds(3);
        CCELP.enabled = false;
    }
}
