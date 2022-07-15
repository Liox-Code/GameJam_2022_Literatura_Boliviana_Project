using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private int min, seg;
    private float time;
    private bool isTimeRuning;

    [SerializeField] private TextMeshProUGUI textTimer;

    void Start()
    {
        time = (min * 60) + seg;
        isTimeRuning = true;
    }

    void Update()
    {
        if (GemGenerator.instance != null && GemGenerator.instance.isPuzzleOver)
        {
            isTimeRuning = false;
        }
        
        if (isTimeRuning)
        {
            time -= Time.deltaTime;

            if (time < 1)
            {
                isTimeRuning = false;
                GemGenerator.instance.puzzleFailed = true;
                GemGenerator.instance.isPuzzleOver = true;
            }

            int tempMin = Mathf.FloorToInt(time / 60);
            int tempSeg = Mathf.FloorToInt(time % 60);

            textTimer.text = String.Format("{0:00} : {1:00}", tempMin, tempSeg);
        }
    }
}
