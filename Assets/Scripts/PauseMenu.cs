using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        //  Reset the game
        GameController gc = FindObjectOfType<GameController>();
//        gc.ResetGameState();

        //  Deactivate the pause menu
        resumeButtonClicked();
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

        //  Restart regular time
        Time.timeScale = 1;
    }

    //  Quit
    void quitButtonClicked()
    {
        //  Stop the player while using the editor
        //  This must be disabled when building for the target platform
        //if(Application.isEditor)
        //    UnityEditor.EditorApplication.isPlaying = false;

        // Quit the application
        // Application.Quit();
        //  Return to the main menu
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        
    }
}
