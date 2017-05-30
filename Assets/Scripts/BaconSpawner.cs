using UnityEngine;
using System.Collections;

public class BaconSpawner : MonoBehaviour {

    public Bacon[] baconTypes;
    public int nextBaconPieceCount = 0;
    public Bacon[] nextBaconPiece;
    public int nextBaconCountdown = 0;

    private bool beginRespawn = false;
    //private float respawnOffset = 0.0f;
    private Vector3 respawnOffset = Vector3.zero;
    private float timerCount = 0.0f;

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
            //foreach (float f in gc.yOffsets)
            //    MakinBacon(f);
            foreach(SpawnPoint sP in gc.baconOffsetArray)
            {
                MakinBacon(sP.position);
            }
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
                MakinBacon(respawnOffset);
                
                timerCount = 0;
            }
        }
    }

    public void respawnBacon(Vector3 yOffset)//float yOffset)
    {
        Debug.Log("yOffset " + yOffset);
        beginRespawn = true;
        respawnOffset = yOffset;
    }

    // Spawn a new strip of bacon
    private void MakinBacon(Vector3 position)//float yOffset)
    {
        //Pan p = FindObjectOfType<Pan>();
        //Vector3 v3 = p.transform.position;

        //v3.y = v3.y + yOffset;
        
        // Instantiate the piece of bacon currently in the last index of Next Piece array
        int lastElement = nextBaconPiece.Length - 1;
        Instantiate(nextBaconPiece[lastElement], position, Quaternion.identity);

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
