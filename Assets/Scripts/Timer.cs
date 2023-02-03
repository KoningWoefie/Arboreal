using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float seconds = 0;

    private bool started = false;
    private bool paused = false;
    private bool stopped = false;

    // Update is called once per frame
    public void StartTimer()
    {
        started = true;
        paused = false;
        stopped = false;
    }

    public void PauseTimer()
    {
        paused = true;
    }

    public void StopTimer()
    {
        stopped = true;
        started = false;
        paused = false;
    }

    public float Seconds()
    {
        return seconds;
    }

    void Update()
    {
        if(started && !paused && !stopped)
        {
            seconds += Time.deltaTime;
        }
        if(stopped)
        {
            seconds = 0;
        }
    }
}
