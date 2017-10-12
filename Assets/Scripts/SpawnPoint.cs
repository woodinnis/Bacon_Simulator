using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    
    public float offset
    { get; set; }
    public bool occupied
    { get; set; }
    public Vector3 position
    { get; set; }


    // This handles the SpawnPoints and toggling the occupied bool.
    // Multiple instances will still occur if a piece is dragged over an occupied spawn point
    // A code solution has not been fully worked out yet, but this suffices

    // Check for a piece currently present on the sawn point
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bacon")
            // Mark the spawn point as occupied
            occupied = true;
    }

    // Check for a piece leaving the spawn point
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bacon")
        {
            // Mark the spawn point unoccupied
            occupied = false;
        }
    }
}
