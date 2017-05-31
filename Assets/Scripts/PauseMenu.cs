using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    //  Public button variables for the pause menu buttons
    public Button RestartButton;
    public Button ResumeButton;
    public Button QuitButton;

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
        Debug.Log("Continue your revelries");
    }

    //  Quit
    void quitButtonClicked()
    {
        Debug.Log("OW! Stop that!");
    }
}
