using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    //  Public button variables for the pause menu buttons
    public Button RestartButton;
    public Button ResumeButton;
    public Button QuitButton;

    public Button PauseButton;
    //private Image panelImage;
    

	void Awake()
    {
        //panelImage = GetComponent<Image>();

        //  Add listeners for all pause menu buttons
        RestartButton.onClick.AddListener(restartButtonClicked);
        ResumeButton.onClick.AddListener(resumeButtonClicked);
        QuitButton.onClick.AddListener(quitButtonClicked);
    }

    void Start()
    {
        //panelImage.enabled = false;
    }

    //  Functions performed when the corresponding pause menu button is clicked
    //  Restart
    void restartButtonClicked()
    {
        Debug.Log("START AGAIN!");
    }

    // Resume
    void resumeButtonClicked()
    {
        //  Reenable and reactivate the Pause Button
        PauseButton.enabled = true;
        PauseButton.gameObject.SetActive(true);

        //  Disable and deactivate the pause menu
        gameObject.SetActive(false);
        enabled = false;
    }

    //  Quit
    void quitButtonClicked()
    {
        Debug.Log("OW! Stop that!");
    }
}
