using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int maxFails = 0;
    public int maxStrips = 0;

    //[HideInInspector]
    public int score = 0;
    [HideInInspector]
    public int failCount = 0;
    [SerializeField]
    private Timer NextPieceTimer;
    public float TimeUntilNextPiece = 0.0f;

    [SerializeField]
    private LevelTimer GameLevelTimer;
    public float LevelTime;
    

    #region // Variables for bacon and spawning bacon
    [HideInInspector]
    public int baconCount;
    [SerializeField]
    private BaconSpawner baconSpawner;
    //[SerializeField]
    //private NextPiece nextPieceBox;
    public SpawnPoint[] SpawnPoints;
    [SerializeField]
    private Vector2[] SpawnPointVectors;
    #endregion

    #region // UI variables
    // Score, win, fail
    public Text scoreField;
    public Text failField;
    public Text winLoseField;

    public Text LevelTimerText;

    // Buttons
    public Button resetButton;
    public Button quitButton;

    public GameObject finishedPieces;
    [SerializeField]
    private BoxCollider2D finishedPiecesCollider;
    #endregion

    #region // These values are for a feature that is not yet implemented
    [HideInInspector]
    public float panOffsetCheck = 0.0f;
    [HideInInspector]
    public float panShakeTimeReduction = 0.0f;
    #endregion

    void Awake()
    {
        //  Set standard time scale
        Time.timeScale = 1;


        // Set Level Timer
        GameLevelTimer.LevelTime = LevelTime;
        GameLevelTimer.isPaused = true;
    }

    // Use this for initialization
    void Start()
    {

        // Fill array with spawn point locations
        for(int i = 0; i < SpawnPoints.Length; i++)
        {
            SpawnPointVectors[i] = SpawnPoints[i].transform.position;
        }

        // Spawn bacon at all points
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            RespawnBacon(i);
            //SpawnPoints[i].occupied = true;
        }

        // Find collider on the finished pieces field
        finishedPiecesCollider = finishedPieces.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // Find all bacons currently in the scene
        Bacon[] baconsInScene = FindObjectsOfType<Bacon>();
      
        // Check the finished pieces box
        FinishedPiecesBoxCheck(baconsInScene);

        // Update displayed score
        scoreField.text = score.ToString();

        // Update Level Timer
        if (!GameLevelTimer.timerEnded())
        {
            LevelTimerText.text = Mathf.FloorToInt(GameLevelTimer.currentTime).ToString();// ((int)GameLevelTimer.currentTime).ToString();

            #region // Fill spawn points with new pieces of bacon
            for (int SpawnPointIterator = 0; SpawnPointIterator < SpawnPoints.Length; SpawnPointIterator++)
            {
                // Check if a spawn point is occupied
                if (!SpawnPoints[SpawnPointIterator].occupied)
                {
                    // Spawn a piece of bacon and mark the point as occupied
                    RespawnBacon(SpawnPointIterator);
                    //Debug.Log("Spawn Point " + SpawnPointIterator + " : " + SpawnPoints[SpawnPointIterator].transform.position);
                }
            }
            #endregion
        }
        else
        {
            // Disable all bacon colliders and timers in the scene to prevent further movement and burning
            foreach(Bacon bacon in baconsInScene)
            {
                bacon.GetComponent<BoxCollider2D>().enabled = false;
                bacon.timer.enabled = false;
            }
        }

        // Proof of concept Update code
        #region
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
        //        quitButton.onClick.AddListener(QuitGame);
        #endregion

        // When Mouse Button 0 (LMB) is pressed unpause the Level Timer
        if (Input.GetMouseButtonDown(0))
        {
            GameLevelTimer.isPaused = false;
        }

    }

    // Check for a collision between any piece of bacon and the Finished Pieces box
    private void FinishedPiecesBoxCheck(Bacon[] allBacons)
    {
        // Test for bacon overlapping the finished pieces box
        foreach (Bacon bacon in allBacons)
        {
            if (finishedPiecesCollider.IsTouching(bacon.GetComponent<BoxCollider2D>()))
            {
                // Set a new Vector based on the position of the bacon piece and the tray
                Vector2 moveToPan = new Vector2(finishedPiecesCollider.transform.position.x, bacon.transform.position.y);

                // Move the bacon piece to the tray and disable the collider
                if (!Input.GetMouseButton(0))
                {
                    bacon.transform.position = moveToPan;
                    bacon.GetComponent<BoxCollider2D>().enabled = false;

                    // Add to the score for a successfully cooked piece
                    if (bacon.baconState == Bacon.BaconState.baconCooked)
                    {
                        score++;
                    }
                    // Deduct from the score for a burnt piece
                    else if (bacon.baconState == Bacon.BaconState.baconBurned)
                    {
                        if (score > 0)
                        {
                            score--;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Spawn a new piece of bacon at the spawn point referenced by the SpawnPointIndex variable
    /// Set the SpawnPoint at the SpawnPointIndex to occupied
    /// </summary>
    /// <param name="SpawnPointIndex"></param>
    private void RespawnBacon(int SpawnPointIndex)
    {
        // Start the countdown timer
        if(NextPieceTimer.isPaused)
            NextPieceTimer.isPaused = false;

        // Set the countdown timer's target time
        NextPieceTimer.targetTime = TimeUntilNextPiece;

        // When the timer ends - timerEnded() check
        if (NextPieceTimer.timerEnded())
        {
            // Pause the timer
            NextPieceTimer.isPaused = true;

            // Spawn a new piece and place it at the spawn point
            Bacon nextBaconPiece = baconSpawner.GenerateBacon();
            nextBaconPiece.transform.position = SpawnPointVectors[SpawnPointIndex];

            // Mark this spawn point as occupied
            SpawnPoints[SpawnPointIndex].occupied = true;
        }
    }

    // Check the current number of bacon strips on screen and return a T/F
    private bool CheckTotalBaconCount()
    {
        if (baconCount < maxStrips)
            return true;
        else
            return false;
    }

    // Seriously, do you need this explained?
    void QuitGame()
    {
        Application.Quit();
    }
}   

#region // Old code for reference
    //float NewBaconLocation()
    //Vector3 NewBaconLocation()
    //{
    //    float newBaconLocation = 0.0f;
    //    Vector3 newBaconLocation = Vector3.zero;
    //    int baconSpawnerCount = baconSpawnerArray.Length;

    //    for(int i = 0; i < baconSpawnerCount; i++)
    //    {
    //        if (baconSpawnerArray[i].occupiedSpawnPoint)
    //        {
    //            return baconSpawnerArray[i].transform.position;
    //        }
    //    }
    //    return newBaconLocation;
    //}

    // Enable or disable Reset button
    //void SetResetButtonState(bool buttonState)
    //{
    //    if (buttonState)
    //    {
    //        resetButton.enabled = true;
    //        resetButton.image.enabled = true;
    //        resetButton.GetComponentInChildren<Text>().enabled = true;
    //    }
    //    else
    //    {
    //        resetButton.enabled = false;
    //        resetButton.image.enabled = false;
    //        resetButton.GetComponentInChildren<Text>().enabled = false;
    //    }
    //}

    // Reset the game when reset button is pressed
    //public void ResetGameState()
    //{

    //    // Destroy all instances of bacon
    //    foreach (Bacon b in FindObjectsOfType<Bacon>())
    //    {
    //        Destroy(b.gameObject);
    //    }

    //    //  Reset all Bacon Spawners
    //    int baconSpawnerCount = baconSpawnerArray.Length;
    //    for (int i = 0; i < baconSpawnerCount; i++)
    //    {
    //        if (baconSpawnerArray[i].occupiedSpawnPoint)
    //        {
    //            baconSpawnerArray[i].occupiedSpawnPoint = false;
    //        }

    //        baconSpawnerArray[i].resetBaconTimer();
    //    }

    //    //  Reset strip count, score, and fails
    //    baconCount = 0;
    //    score = 0;
    //    failCount = 0;
    //}
    #endregion
