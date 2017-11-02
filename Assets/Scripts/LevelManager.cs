using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private string[] GameLevels;

    private Scene CurrentScene;
    int CurrentSceneIndex;
    int NextSceneIndex;

    // Use this for initialization
    void OnEnable()
    {
        // Get the active scene (Should be the start menu)
        CurrentScene = SceneManager.GetActiveScene();

        // Get the active scene index and next scene index
        CurrentSceneIndex = CurrentScene.buildIndex;
        NextSceneIndex = CurrentSceneIndex + 1;

        // Display Current Scene name
        Debug.Log("Current Scene Name: " + GameLevels[CurrentSceneIndex]);

        // If the next scene is valid in the Scene list, display it
        if (NextSceneIndex < GameLevels.Length)
        {
            Debug.Log("Next Scene Name: " + GameLevels[NextSceneIndex]);
        }

    }

    // Load the next level
    public void LoadNextLevel()
    {

        if (NextSceneIndex < GameLevels.Length)
        {
            
            SceneManager.LoadSceneAsync(GameLevels[NextSceneIndex], LoadSceneMode.Single);

        }
        else
        {
            Debug.Log("There is no Scene at this index");

            SceneManager.LoadSceneAsync("scene00",LoadSceneMode.Single);
        }
    }

    // Reload the level
    public void ReloadCurrentLevel()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync(CurrentScene.name, LoadSceneMode.Single);
    }


}
