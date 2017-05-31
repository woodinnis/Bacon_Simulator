using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public Button pauseButton;
    public PauseMenu pauseMenu;

	// Use this for initialization
	void Start () {

        pauseButton.onClick.AddListener(pauseButtonClicked);
        pauseMenu.enabled = false;
	}

    void pauseButtonClicked()
    {
        Debug.Log("You clicked me!");
    }
}
