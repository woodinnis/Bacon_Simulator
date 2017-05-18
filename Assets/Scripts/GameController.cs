using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int maxFails = 0;
    public int maxStrips = 0;

    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public int failCount = 0;

    public Text scoreField;
    public Text failField;
    public Text winLoseField;

    public GameObject[] bacon;
    [HideInInspector]
    public int baconCount;

    public Button resetButton;
    public Button quitButton;

    public float[] yOffsets;

    // These values are for a feature that is not yet implemented

    [HideInInspector]
    public float panOffsetCheck = 0.0f;
    [HideInInspector]
    public float panShakeTimeReduction = 0.0f;

    // The above values are for a feature that is not yet implemented

    void Awake()
    {
        SetResetButtonState(false);
    }

    // Use this for initialization
    void Start () {

        // If no bacon exists in the scene, place bacon
        if (!FindObjectOfType<Bacon>())
        {
            foreach (float f in yOffsets)
                MakinBacon(f);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        scoreField.text = score.ToString();
        failField.text = failCount.ToString();

        if (!FindObjectOfType<Bacon>())
        {
            foreach(float f in yOffsets)
                MakinBacon(f);
        }

        if (failCount >= maxFails)
        {
            winLoseField.text = "You Lose!";
            SetResetButtonState(true);
        }
        CheckBaconState();
        quitButton.onClick.AddListener(QuitGame);
    }


    private void CheckBaconState()
    {
        // Check for a mouse click
        if (Input.GetMouseButton(0))
        {
            // Get mouse position and convert to usable coordinates
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            float newBaconLocation = 0.0f;

            // If less than the max number of strips are present in the scene spawn a new strip
            if (baconCount < maxStrips)
            {
                newBaconLocation = mousePos.y;
                MakinBacon(newBaconLocation);
            }
        }
    }

    // Spawn a new strip of bacon
    private void MakinBacon(float yOffset)
    {
        Pan p = FindObjectOfType<Pan>();
        Vector3 v3 = p.transform.position;

        v3.y = v3.y + yOffset;
        Instantiate(bacon[GetRandomInt()], v3, Quaternion.identity);
        baconCount++;
    }

    // Random integer generation from system clock
    private int GetRandomInt()
    {
        int index = 0;
        Random.InitState(System.DateTime.Now.Millisecond);
        index = Random.Range(0, bacon.Length);
        return index;
    }

    // Enable or disable Reset button
    void SetResetButtonState(bool buttonState)
    {
        if (buttonState)
        {
            resetButton.enabled = true;
            resetButton.image.enabled = true;
            resetButton.GetComponentInChildren<Text>().enabled = true;
        }
        else
        {
            resetButton.enabled = false;
            resetButton.image.enabled = false;
            resetButton.GetComponentInChildren<Text>().enabled = false;
        }
    }

    // Reset the game when reset button is pressed
    public void ResetGameState()
    {
        
        // Destroy all instances of bacon
        foreach (Bacon b in FindObjectsOfType<Bacon>())
        {
            Destroy(b.gameObject);
        }
        
        // Reset score and fails
        score = 0;
        failCount = 0;

        SetResetButtonState(false);
    }


    void QuitGame()
    {
        Application.Quit();
    }
   
    
}
