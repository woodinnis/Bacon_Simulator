using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float targetTime;
    

	// Update is called once per frame
	void Update () {
        targetTime -= Time.deltaTime;
        
        if(targetTime <= 0.0f)
        {
            timerEnded();
        }
	}

    bool timerEnded()
    {
        return true;
    }
}
