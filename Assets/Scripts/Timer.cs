using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    [HideInInspector]
    public float targetTime;
    [HideInInspector]
    public float currentTime = 0.0f;

	// Update is called once per frame
	void Update () {
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
