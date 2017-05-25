using UnityEngine;
using System.Collections;

public class BaconSpawner : MonoBehaviour {

    public Bacon[] baconTypes;
    public int nextBaconPieceCount = 0;
    public Bacon[] nextBaconPiece;
    public int nextBaconCountdown = 0;
    public Timer nextBaconTimer;

    void Start ()
    {
        // Fill the Next Piece array
        for (int i = 0; i < nextBaconPieceCount; i++)
        {
            GenerateBacon(i);
        }

        GameController gc = FindObjectOfType<GameController>();

        // If no bacon exists in the scene, place bacon
        if (!FindObjectOfType<Bacon>())
        {
            foreach (float f in gc.yOffsets)
                MakinBacon(f);
        }
    }

    // Spawn a new strip of bacon
    public void MakinBacon(float yOffset)
    {
        Pan p = FindObjectOfType<Pan>();
        Vector3 v3 = p.transform.position;

        v3.y = v3.y + yOffset;

        // Instantiate the piece of bacon currently in the last index of Next Piece array
        int lastElement = nextBaconPiece.Length - 1;
        Instantiate(nextBaconPiece[lastElement], v3, Quaternion.identity);

        // Increase the total count
        GameController gc = FindObjectOfType<GameController>();
        gc.baconCount++;

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

}
