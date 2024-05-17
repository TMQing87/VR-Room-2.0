using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerText;

    private float startTime;
    private bool isTimerRunning;

    void Start()
    {
        isTimerRunning = false;
        startTime = 0f;
        UpdateTimerText(0f);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void StartStopTimer()
    {
        if (isTimerRunning)
        {
            StopTimer();
        }
        else if (startTime != 0f)
        {
            ResetTimer();
        } 
        else
        {
            StartTimer();
        }
    }

    public void ResetTimer()
    {
        StopTimer();
        startTime = 0f;
        UpdateTimerText(0f);
    }

    private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    private void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}