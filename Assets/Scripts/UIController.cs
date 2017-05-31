using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public Button pauseButton;
    public PauseMenu pauseMenu;
    
	// Use this for initialization
	void Start () {

        //  Disable and deactivate the pause menu
        pauseMenu.enabled = false;
        pauseMenu.gameObject.SetActive(false);

        pauseButton.onClick.AddListener(pauseButtonClicked);
	}

    //  Pause Button functions
    void pauseButtonClicked()
    {
        //  Enable the pause menu
        pauseMenu.enabled = true;
        pauseMenu.gameObject.SetActive(true);
        //  Disable the pause button
        enabled = false;
        pauseButton.gameObject.SetActive(false);
    }
}
