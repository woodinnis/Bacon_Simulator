using UnityEngine;
using System.Collections;

public class BaconSpawner : MonoBehaviour {

    public Bacon[] baconTypes;
    public int nextBaconPieceCount = 0;
    public Bacon[] nextBaconPiece;
    //public int nextBaconCountdown = 0;

    //public bool occupiedSpawnPoint = false;

    //private bool beginRespawn = false;
    //private Vector3 spawnPosition;
    //private float timerCount = 0.0f;

    void Start ()
    {
        // Fill the bacon array at startup
        FillBaconArray();
        
        //// Set the spawn position to the current transform of the BaconSpawner
        //spawnPosition = transform.position;

        //// Fill the Next Piece array
        //for (int i = 0; i < nextBaconPieceCount; i++)
        //{
        //    Debug.Log("Next Bacon Piece " + i);
        //    GenerateBacon(i);
        //}
    }

    void Update()
    {
        //if (beginRespawn)
        //{

        //    timerCount += Time.deltaTime;

        //    if (timerCount > nextBaconCountdown)
        //    {
        //        beginRespawn = false;
        //        //MakinBacon(spawnPosition);

        //        resetBaconTimer();
        //    }
        //}
    }

    /// <summary>
    /// Reset the respawn timer
    /// </summary>
    //public void resetBaconTimer()
    //{
    //    timerCount = 0.0f;
    //}

    /// <summary>
    /// Reset the spawn position, and mark the respawn functionality as true
    /// </summary>
    /// <param name="position"></param>
    //public void respawnBacon(Vector3 position)
    //{
    //    beginRespawn = true;
    //    spawnPosition = position;
    //}

    /// <summary>
    /// Generate a random piece of bacon to fill the correct array position
    /// </summary>
    /// <param name="i"></param>
    Bacon GenerateBacon(int i)
    {
        int lastElement = nextBaconPiece.Length - 1;
        Bacon newBacon = Instantiate(nextBaconPiece[lastElement]);

        return newBacon;
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

    // Fill the next bacon piece array
    void FillBaconArray()
    {

        // Fill the Next Piece array
        for (int i = 0; i < nextBaconPieceCount; i++)
        {
            // If the array position to be filled is not the zeroth position
            if (i > 0)
            {
                // If a position needs to be filled begin filling it
                if (!nextBaconPiece[i])
                {
                    // Generate new pieces until no matched between i and i-1 exist
                    do
                    {
                        nextBaconPiece[i] = baconTypes[GetRandomInt()];
                    }
                    while (nextBaconPiece[i] == nextBaconPiece[i - 1]);
                }
            }
            // Fill the array position [0] with a new piece of bacon
            else
            {
                nextBaconPiece[i] = baconTypes[GetRandomInt()];
            }
        }
    }
}
