using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButton : MonoBehaviour {

    public int nextSceneBuildIndex;
    private Button startButton;

	// Use this for initialization
	void Start () {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startButtonClicked);
	}

    void startButtonClicked()
    {
        //  Load indexed scene
        SceneManager.LoadSceneAsync(nextSceneBuildIndex, LoadSceneMode.Single);
    }
}
