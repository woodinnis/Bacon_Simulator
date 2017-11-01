using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private string[] GameLevels;

    private Scene CurrentScene;
    int CurrentSceneIndex;

	// Use this for initialization
	void Start () {

        // Get the active scene (Should be the start menu)
        CurrentScene = SceneManager.GetActiveScene();

        // Get the active scene index (Should be 0)
        CurrentSceneIndex = CurrentScene.buildIndex;

        // Display Current Scene name
        Debug.Log("Current Scene Name: " + GameLevels[CurrentSceneIndex]);
        
	}

    // Load the next level
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(GameLevels[CurrentSceneIndex + 1]);
    }
}
