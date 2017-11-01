using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButton : MonoBehaviour {

    public string nextScene;
    private Button startButton;

	// Use this for initialization
	void Start () {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startButtonClicked);

        Scene Scene00 = SceneManager.GetActiveScene();
        Debug.Log("Build Index: " + Scene00.buildIndex);

        Scene Scene01 = SceneManager.GetSceneByName("scene01");
        if(Scene01.IsValid())
            Debug.Log("Build Index 01: " + Scene01.buildIndex);

        Scene Scene02 = SceneManager.GetSceneByName("scene02");
        if (Scene02.IsValid())
            Debug.Log("Build Index 02: " + Scene02.buildIndex);
    }


    void startButtonClicked()
    {
        //  Load next scene
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
    }
}
