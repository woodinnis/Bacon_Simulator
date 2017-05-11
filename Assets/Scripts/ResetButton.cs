using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour {

    private GameController gc;
    private Button btn;

	// Use this for initialization
	void Start () {
        gc = FindObjectOfType<GameController>();
        btn = GetComponent<Button>();

        btn.onClick.AddListener(ButtonHasBeenClicked);
	}
	
	void ButtonHasBeenClicked()
    {
        gc.ResetGameState();
    }
}
