using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {


    public float LevelTime;
    [HideInInspector]
    public float currentTime;

    private float targetTime = 0.0f;

    [HideInInspector]
    public bool isPaused;

    void Start()
    {
        // Start the current time at the Level Time for this timer
        currentTime = LevelTime;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
            currentTime -= Time.deltaTime;
    }

    public bool timerEnded()
    {
        // If the timer has reached its target time
        if (currentTime <= targetTime)
        {
            // Reset time timer and return true
            isPaused = true;
            return true;
        }
        else
            return false;
    }
}
