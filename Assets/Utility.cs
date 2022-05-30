using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{

}

[System.Serializable]
public class Timer
{
    public float duration;

    public float CurrentTime { get; set; } = 0;
    public bool Enabled { get; private set; } = false;

    public Timer()
    {
        
    }

    public void Start(float time)
    {
        duration = time;
        Enabled = true;
    }

    public void Pause()
    {
        Enabled = false;
    }

    public void Reset(bool disable = true)
    {
        CurrentTime = 0;
        if (disable == true)
        {
            Enabled = false;
        }
    }

    public bool Done() // Must be called at some point to actually increment timer!
    {
        if(CurrentTime < duration)
        {
            CurrentTime += Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

}
[System.Serializable]
public class Stopwatch
{

    public float elapsedTime { get; private set; } = 0;
    public bool Enabled { get; private set; } = false;

    public Stopwatch()
    {

    }

    public void Start()
    {
        Enabled = true;

    }

    public void Stop()
    {
        Enabled = false;
        elapsedTime = 0;
    }

    public void Pause()
    {
        Enabled = false;
        
    }

    public float Count()
    {
        if (Enabled)
        {
            elapsedTime += Time.deltaTime;
            return elapsedTime;
        }
        else
        {
            return elapsedTime;
        }
        
    }
}
