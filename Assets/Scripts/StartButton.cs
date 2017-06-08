using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour {

    private Button startButton;

	// Use this for initialization
	void Start () {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startButtonClicked);
	}

    void startButtonClicked()
    {
        Debug.Log("I did a thing");
    }
}
