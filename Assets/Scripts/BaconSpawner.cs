using UnityEngine;
using System.Collections;

public class BaconSpawner : MonoBehaviour {

    public Bacon[] baconTypes;
    public int nextBaconPieceCount = 0;
    public Bacon[] nextBaconPiece;
    public int nextBaconCountdown = 0;

    public bool occupiedSpawnPoint = false;

    private bool beginRespawn = false;
    private Vector3 spawnPosition;
    private float timerCount = 0.0f;

    void Start ()
    {
        // Set the spawn position to the current transform of the BaconSpawner
        spawnPosition = transform.position;

        // Fill the Next Piece array
        for (int i = 0; i < nextBaconPieceCount; i++)
        {
            GenerateBacon(i);
        }
    }

    void Update()
    {
        if (beginRespawn)
        {
            
            timerCount += Time.deltaTime;

            if (timerCount > nextBaconCountdown)
            {
                beginRespawn = false;
                MakinBacon(spawnPosition);
                
                timerCount = 0;
            }
        }
    }

    public void respawnBacon(Vector3 position)
    {
        Debug.Log("Respawn Position " + position);
        beginRespawn = true;
        spawnPosition = position;
    }

    // Spawn a new strip of bacon
    private void MakinBacon(Vector3 position)
    { 
        // Instantiate the piece of bacon currently in the last index of Next Piece array
        int lastElement = nextBaconPiece.Length - 1;
        Instantiate(nextBaconPiece[lastElement], position, Quaternion.identity);

        // Increase the total count
        GameController gc = FindObjectOfType<GameController>();
        gc.baconCount++;
        occupiedSpawnPoint = true;

        // Shift the array up, and generate a new piece in the Next Piece array
        ShiftBaconArrayUp();
        GenerateBacon(0);
    }

    // Generate a random piece of bacon
    private void GenerateBacon(int i)
    {
        nextBaconPiece[i] = baconTypes[GetRandomInt()];
    }

    // Shift all Next Piece array elements up by 1 index
    void ShiftBaconArrayUp()
    {
        int size = nextBaconPiece.Length - 1;

        for (int i = size; i > 0; i--)
        {
            nextBaconPiece[i] = nextBaconPiece[i - 1];
        }
    }

    // Random integer generation from system clock
    private int GetRandomInt()
    {
        int index = 0;
        Random.InitState(System.DateTime.Now.Millisecond);
        index = Random.Range(0, baconTypes.Length);
        return index;
    }

    // Timer function
    //void timer(float targetTime)
    //{
    //    float currentTime = 0.0f;

    //    while(currentTime < targetTime)
    //    {
    //        Debug.Log("Current Time: " + currentTime.ToString());
    //        currentTime += Time.deltaTime;

    //        if (currentTime >= targetTime)
    //        {
    //            timerEnded();
    //        }
    //    }
    //}

    // Perform this function only when the timer has ended

    //bool timerEnded()
    //{
    //    Debug.Log("Timer Ended");
    //    return true;
    //}

}
