using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [System.Serializable]
    public class baconOffset
    {
        public float offset
        { get; set; }
        public bool occupied
        { get; set; }
    }

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
    //[HideInInspector]
    public float[] yOffsets;
    private BaconSpawner baconSpawner;
    public baconOffset[] baconOffsetArray;

    // UI variables
    public Button resetButton;
    public Button quitButton;

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

        // Find and assign the bacon spawner 
        baconSpawner = FindObjectOfType<BaconSpawner>();
        
        // Set default values for Offset class array
        for (int i = 0; i < yOffsets.Length; i++)
        {
            baconOffsetArray[i].offset = yOffsets[i];
            baconOffsetArray[i].occupied = true;
        }
    }


    // Update is called once per frame
    void Update ()
    {
        scoreField.text = score.ToString();
        failField.text = failCount.ToString();

        // Test for failure count
        if (failCount >= maxFails)
        {
            winLoseField.text = "You Lose!";
            SetResetButtonState(true);
        }

        // Check total bacon pieces in the scene
        if (CheckTotalBaconCount())
        {
            baconSpawner.respawnBacon(NewBaconLocation());
        }

        // Check for a button press on the Quit button
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
        
        //Check for a mouse click
        if (Input.GetMouseButton(0))
            {
                // Get mouse position and convert to usable coordinates
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                float newBaconLocation = mousePos.y;

                return newBaconLocation;
            }
            else
                return 0.0f;
        
        // Check all default yOffsets for a piece of bacon
        //Bacon[] checkYoffsets = FindObjectsOfType<Bacon>();
        //bool yOffsetTest = false;
        //float newOffset = 0.0f;
        
        //for(int i = 0; i < yOffsets.Length; i++)
        //foreach(float y in yOffsets)
        //{
        //    foreach(Bacon b in checkYoffsets)
        //    {
        //        if (b.transform.position.y == y)
        //            break;
        //        else
        //        {
        //            newOffset = y;
        //            break;
        //            //Debug.Log("Bacon Offsets: " + yOffsets[i]);
        //        }
        //    }
            //Debug.Log("yOffset: " + y);

            //foreach (Bacon b in checkYoffsets)
            //    //Debug.Log("Bacon Offsets: " + b.transform.position.y);
            //    if (y == b.transform.position.y)
            //        Debug.Log("Missing: " + y);

            //{
            //    if (b.transform.position.y == y)
            //        break;
            //    else
            //        return y;
            //}
            //return 0;
        //

        //return 0f;}
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
