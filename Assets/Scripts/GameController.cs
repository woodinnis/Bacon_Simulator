using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int maxFails = 0;
    public int maxStrips = 0;

    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public int failCount = 0;

    public GameObject finishedPieces;
    [SerializeField]
    private BoxCollider2D finishedPiecesCollider;

    public Text scoreField;
    public Text failField;
    public Text winLoseField;

    // Variables for bacon and spawning bacon
    [HideInInspector]
    public int baconCount;
    [SerializeField]
    private BaconSpawner[] baconSpawner;
    [SerializeField]
    private Bacon[] allBacons;

    // UI variables
    public Button resetButton;
    public Button quitButton;

    // These values are for a feature that is not yet implemented
    #region
    [HideInInspector]
    public float panOffsetCheck = 0.0f;
    [HideInInspector]
    public float panShakeTimeReduction = 0.0f;
    #endregion

    void Awake()
    {
        SetResetButtonState(false);
    }

    // Use this for initialization
    void Start()
    {
        //  Set standard time scale
        Time.timeScale = 1;

        // Find and assign the bacon spawner 
        baconSpawner = FindObjectsOfType<BaconSpawner>();

        // Find collider on the finished pieces field
        finishedPiecesCollider = finishedPieces.GetComponent<BoxCollider2D>();

        // Count the number of spawners in the scene
        int baconSpawnerCount = baconSpawner.Length;

        //  Load up the bacon
        if (!FindObjectOfType<Bacon>())
        {
            for (int i = 0; i < baconSpawnerCount; i++)
            {
                baconSpawner[i].respawnBacon(baconSpawner[i].transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Test for instance of Object.Bacon, and load array
        if (FindObjectOfType<Bacon>())
        {
            allBacons = FindObjectsOfType(typeof(Bacon)) as Bacon[];
        }

        // Test for bacon overlapping the finished pieces box
        foreach (Bacon bacon in allBacons)
        {
            if (finishedPiecesCollider.OverlapPoint(bacon.transform.position))
                Debug.Log("Bacon Positioned At: " + bacon.transform.position);
        }

        // Proof of concept Update code
        #region
        //scoreField.text = score.ToString();
        //failField.text = failCount.ToString();

        //// Test for failure count
        //if (failCount >= maxFails)
        //{
        //    winLoseField.text = "You Lose!";
        //    SetResetButtonState(true);
        //}

        //// Check total bacon pieces in the scene
        //if (CheckTotalBaconCount())
        //{
        //    int baconSpawnerCount = baconSpawner.Length;

        //    for (int i = 0; i < baconSpawnerCount; i++)
        //    {
        //        if (!baconSpawner[i].occupiedSpawnPoint)
        //        {
        //            baconSpawner[i].respawnBacon(baconSpawner[i].transform.position);
        //        }
        //    }
        //}

        // Check for a button press on the Quit button
        quitButton.onClick.AddListener(QuitGame);
        #endregion
    }

    // Check the current number of bacon strips on screen and return a T/F
    private bool CheckTotalBaconCount()
    {
        if (baconCount < maxStrips)
            return true;
        else
            return false;
    }

    //float NewBaconLocation()
    Vector3 NewBaconLocation()
    {
        //float newBaconLocation = 0.0f;
        Vector3 newBaconLocation = Vector3.zero;
        int baconSpawnerCount = baconSpawner.Length;

        for(int i = 0; i < baconSpawnerCount; i++)
        {
            if (baconSpawner[i].occupiedSpawnPoint)
            {
                return baconSpawner[i].transform.position;
            }
        }
        return newBaconLocation;

       
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

        //  Reset all Bacon Spawners
        int baconSpawnerCount = baconSpawner.Length;
        for (int i = 0; i < baconSpawnerCount; i++)
        {
            if (baconSpawner[i].occupiedSpawnPoint)
            {
                baconSpawner[i].occupiedSpawnPoint = false;
            }

            baconSpawner[i].resetBaconTimer();
        }

        //  Reset strip count, score, and fails
        baconCount = 0;
        score = 0;
        failCount = 0;
    }

    // Seriously, do you need this explained?
    void QuitGame()
    {
        Application.Quit();
    }


}