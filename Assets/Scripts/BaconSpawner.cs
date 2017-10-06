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
            Debug.Log("Next Bacon Piece " + i);
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

                resetBaconTimer();
            }
        }
    }

    /// <summary>
    /// Reset the respawn timer
    /// </summary>
    public void resetBaconTimer()
    {
        timerCount = 0.0f;
    }

    /// <summary>
    /// Reset the spawn position, and mark the respawn functionality as true
    /// </summary>
    /// <param name="position"></param>
    public void respawnBacon(Vector3 position)
    {
        beginRespawn = true;
        spawnPosition = position;
    }

    /// <summary>
    /// Generate a random piece of bacon to fill the correct array position
    /// </summary>
    /// <param name="i"></param>
    private void GenerateBacon(int i)
    {
        nextBaconPiece[i] = baconTypes[GetRandomInt()];
    }

    /// <summary>
    /// Return a random integer generated from the system clock
    /// </summary>
    /// <returns></returns>
    private int GetRandomInt()
    {
        int index = 0;
        Random.InitState(System.DateTime.Now.Millisecond);
        index = Random.Range(0, baconTypes.Length);
        return index;
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
        //ShiftBaconArrayUp();
        //GenerateBacon(0);
    }


    //// Shift all Next Piece array elements up by 1 index
    //void ShiftBaconArrayUp()
    //{
    //    int size = nextBaconPiece.Length - 1;

    //    for (int i = size; i > 0; i--)
    //    {
    //        nextBaconPiece[i] = nextBaconPiece[i - 1];
    //    }
    //}


}
