using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{

    private GameController gc;
    private Button NextLvlButton;
    private LevelManager LevelManager;

    // Use this for initialization
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        NextLvlButton = GetComponent<Button>();

        #region // Check for mouse press on the Next Level Button and load the next level
        if (NextLvlButton.isActiveAndEnabled)
        {
            NextLvlButton.onClick.AddListener(LevelManager.LoadNextLevel);
        }
        #endregion
    }
}
