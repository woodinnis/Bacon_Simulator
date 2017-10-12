using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    [HideInInspector]
    public float targetTime;
    [HideInInspector]
    public float currentTime = 0.0f;
    [HideInInspector]
    public bool isPaused = false;


	// Update is called once per frame
	void Update () {
        if(!isPaused)
            currentTime += Time.deltaTime;
	}

    public bool timerEnded()
    {
        // If the timer has reached its target time
        if (currentTime >= targetTime)
        {
            // Reset time timer and return true
            currentTime = 0.0f;
            return true;
        }
        else
            return false;
    }
}
