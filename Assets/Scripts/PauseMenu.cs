using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    private Image panelImage;

	void Awake()
    {
        panelImage = GetComponent<Image>();
    }

    void Start()
    {
        panelImage.enabled = false;
    }
}
