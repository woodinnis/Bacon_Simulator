using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {


    public float LevelTime;
    private float currentTime;

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

        Debug.Log("Current Time: " + currentTime);
    }

    public bool timerEnded()
    {
        // If the timer has reached its target time
        if (currentTime <= targetTime)
        {
            // Reset time timer and return true
            currentTime = 0.0f;
            return true;
        }
        else
            return false;
    }
}
