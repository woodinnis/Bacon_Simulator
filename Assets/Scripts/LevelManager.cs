using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private string[] GameLevels;

    private Scene CurrentScene;
    int CurrentSceneIndex;

    // Use this for initialization
    void OnEnable()
    {
        // Get the active scene (Should be the start menu)
        CurrentScene = SceneManager.GetActiveScene();

        // Get the active scene index (Should be 0)
        CurrentSceneIndex = CurrentScene.buildIndex;

        // Display Current Scene name
        Debug.Log("Current Scene Name: " + GameLevels[CurrentSceneIndex]);
        Debug.Log("Next Scene Name: " + GameLevels[CurrentSceneIndex + 1]);

    }

    // Load the next level
    public void LoadNextLevel()
    {

        int NextSceneIndex = CurrentSceneIndex + 1;

        if (NextSceneIndex <= GameLevels.Length)
        {
            SceneManager.LoadSceneAsync(GameLevels[NextSceneIndex], LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("There is no Scene at this index");
        }
    }

}
