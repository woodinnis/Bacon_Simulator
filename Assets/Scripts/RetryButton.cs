using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour {

    private Button RetryBtn;
    private LevelManager LevelManager;

	// Use this for initialization
	void Start () {

        LevelManager = FindObjectOfType<LevelManager>();
        RetryBtn = GetComponent<Button>();

        #region // Check for a mouse press on the Retry Button and reload the level
        if (RetryBtn.isActiveAndEnabled)
        {
            RetryBtn.onClick.AddListener(LevelManager.ReloadCurrentLevel);
        }
        #endregion
    }
}
