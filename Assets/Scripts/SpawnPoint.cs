using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    private Bacon[] BaconInScene;

    void Update()
    {
        BaconInScene = FindObjectsOfType<Bacon>();

        // Perform collision check for bacon transform
        foreach (Bacon bacon in BaconInScene)
        {
            if (transform.position.Equals(bacon.transform.position))
            {
                //Debug.Log("I'm full here! " + bacon.transform.position);
                Debug.Log("I'm here! " + bacon.transform.position);
            }
        }
    }
    public float offset
    { get; set; }
    public bool occupied
    { get; set; }
    public Vector3 position
    { get; set; }
}
