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

    // Variables for bacon and spawning bacon
    [HideInInspector]
    public int baconCount;
    private BaconSpawner baconSpawner;

    // UI variables
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

        //// Fill the Next Piece array
        //for (int i = 0; i < nextBaconPieceCount; i++)
        //{
        //    GenerateBacon(i);
        //}

        //// If no bacon exists in the scene, place bacon
        //if (!FindObjectOfType<Bacon>())
        //{
        //    foreach (float f in yOffsets)
        //        MakinBacon(f);
        //}

        // Find and assign the timer child object
        //nextBaconTimer = GetComponentInChildren<Timer>();

        baconSpawner = FindObjectOfType<BaconSpawner>();
    }


    // Update is called once per frame
    void Update ()
    {
        scoreField.text = score.ToString();
        failField.text = failCount.ToString();

        /*
        if (!FindObjectOfType<Bacon>())
        {
            foreach(float f in yOffsets)
                MakinBacon(f);
        }
        */

        if (failCount >= maxFails)
        {
            winLoseField.text = "You Lose!";
            SetResetButtonState(true);
        }

        Timer t;

        // Check total bacon pieces in the scene
        if (CheckTotalBaconCount())
        {
            // If a new piece is needed, spawn a timer
            //Debug.Log("Bacon Count: " + baconCount);

            baconSpawner.MakinBacon(NewBaconLocation());
            
            //for (int i = baconCount; i < maxStrips; i++)
            //{
            //    Debug.Log("i = " + i);
            //    //Instantiate(timer, new Vector3(0, 0), Quaternion.identity);
            //    t = GetComponentInChildren<Timer>();
                
            //    t.targetTime = nextBaconCountdown;

            //    Debug.Log("Countdown Time: " + t.targetTime);
            //} 
        }

        //float currentTime = t.currentTime;

        //Debug.Log("Current Time: " + currentTime);

        //if (currentTime >= timer.targetTime)
        //{
            //MakinBacon(2);
        //    Destroy(timer.gameObject);
        //}

        quitButton.onClick.AddListener(QuitGame);
    }

    // Check the current number of bacon strips on screen and return a T/F
    private bool CheckTotalBaconCount()
    {
        if (baconCount < maxStrips)
            return true;
        else
            return false;
    }

    float NewBaconLocation()
    {
        // Check for a mouse click
        if (Input.GetMouseButton(0))
        {
            // Get mouse position and convert to usable coordinates
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            float newBaconLocation = 0.0f;

            return newBaconLocation = mousePos.y;
            //baconSpawner.MakinBacon(newBaconLocation);
        }
        else
            return 0.0f;
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

    // Seriously, do you need this explained?
    void QuitGame()
    {
        Application.Quit();
    }
   
    
}
