using UnityEngine;
using System.Collections;

public class BaconSpawner : MonoBehaviour {

    public Bacon[] baconTypes;

    /// <summary>
    /// Return a randomly generate piece of Bacon from the predefine BaconTypes array
    /// </summary>
    /// <returns></returns>
    public Bacon GenerateBacon()
    {
        // Generate a new piece of bacon using a random number
        Bacon newBacon = Instantiate(baconTypes[GetRandomInt()]);

        // Return the piece
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

}

#region // Obsolete BaconSpawner code pre-SpawnPoint updates, retained for reference

/// <summary>
/// Reset the respawn timer
/// </summary>
//public void resetBaconTimer()
//{
//    timerCount = 0.0f;
//}
/// <summary>
/// Clear the last element of the Next piece array, and shift all remeining elements up
/// to take its place
/// </summary>
/// <param name="lastElement"></param>
//private void ShiftNextPieceArray(int lastElement)
//{
//    int ArrayLength = nextBaconPiece.Length - 1;

//    Debug.Log("Array Length: " + ArrayLength);

//    Debug.Log("Last Element = " + nextBaconPiece[lastElement]);
//    nextBaconPiece[lastElement] = null;

//    // Shift the elements of the array up
//    for (int x = ArrayLength; x > 0; x--)
//    {
//        Bacon y = nextBaconPiece[x];
//        Debug.Log("Next Bacon Piece " + x + ": " + y);
//        //nextBaconPiece[x + 1] = y;
//    }

//    Debug.Log("Last Element = " + nextBaconPiece[lastElement]);
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

//// Fill the next bacon piece array
//    void FillBaconArray()
//    {
//        // Fill the Next Piece array
//        for (int i = 0; i < nextBaconPieceCount; i++)
//        {
//            // If the array position to be filled is not the zeroth position
//            if (i > 0)
//            {
//                // If a position needs to be filled begin filling it
//                if (!nextBaconPiece[i])
//                {
//                    // Generate new pieces until no matched between i and i-1 exist
//                    do
//                    {
//                        nextBaconPiece[i] = baconTypes[GetRandomInt()];
//                    }
//                    while (nextBaconPiece[i] == nextBaconPiece[i - 1]);
//                }
//            }
//            // Fill the array position [0] with a new piece of bacon
//            else
//            {
//                nextBaconPiece[i] = baconTypes[GetRandomInt()];
//            }
//        }
//    }

#endregion