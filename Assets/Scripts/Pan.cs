using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Pan : MonoBehaviour {

    
    // Use this for initialization
	void Start () {
        Text text = FindObjectOfType<Text>();

        text.text = "I'm a pan";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
