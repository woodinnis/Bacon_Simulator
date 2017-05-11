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
        
        if(currentTime >= targetTime)
        {
            timerEnded();
        }
	}

    bool timerEnded()
    {
        return true;
    }
}
