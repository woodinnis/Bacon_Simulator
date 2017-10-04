using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bacon : MonoBehaviour {

    public Sprite[] baconSprites;
    public int rawIndex = 0;
    public int cookedIndex;
    public int burnedIndex;


    // Enumerator for bacon raw, flipped, cooked, and burned states
    [HideInInspector]
    public enum BaconState {
        baconRaw,           // Raw state
        //baconSide01Cooked,  // First side cooked
        //baconFlipped,       // Flipped
        //baconSide02Cooked,  // Second side cooked
        baconCooked,        // Fully cooked
        baconBurned         // Burned
    };
    [HideInInspector]
    public BaconState baconState;

    // Times for cooking and burning bacon
    public float cookedTime;
    public float flippedTimeReduction;
    public float burnedTime;

    public Text BaconDebugText;

    private GameController gc;
    public Pan pan;
    public HeatZoneCollider[] heatZoneColliders;

    private SpriteRenderer sprite;
    //private Transform panTransform;

    //[HideInInspector]
    public Timer timer;

    void Awake()
    {
        // Set starting state of the bacon;
        baconState = BaconState.baconRaw;

        // Locate and set a GameController
        gc = FindObjectOfType<GameController>();

        // Check for a timer component attached to the current object
        if ((timer == null) && (GetComponent<Timer>() != null))
        {
            // Set the timer
            timer = GetComponent<Timer>();
        }
        else
        {
            Debug.Log("No Timer Attached");
        }
    }

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = baconSprites[rawIndex];

        // Set timer based on slice time and pause
        timer.targetTime = cookedTime;
        timer.isPaused = true;

        // Find and set the Pan
        pan = FindObjectOfType<Pan>();

        // Find a text field in the current scene
        BaconDebugText = FindObjectOfType<Text>();

        // Retrieve the Heat Zones from the pan
        for (int i = 0; i < pan.GetComponent<Pan>().HeatZoneCollider_HeatZones.Length; i++)
        {
            heatZoneColliders[i] = pan.GetComponent<Pan>().HeatZoneCollider_HeatZones[i];
        }
    }
	
	// Update is called once per frame
	void Update () {

        // Update the displayed time
        float currentTime = timer.currentTime;
//        Debug.Log("Current Time: " + currentTime.ToString());

        // Perform check for collision with HeatZoneColliders
        // Verify mouse is not being held down
        if ((CheckHeatZoneCollision(transform.position)) && (!Input.GetMouseButton(0)))
        {
            // Unpause the timer
            timer.isPaused = false;

            // Check the current bacon state
            CheckBaconState();
        }
        // Pause the timer when no collision is detected
        else
            timer.isPaused = true;
        #region

        //Track the pan position and follow it
        //
        //    Vector3 v3 = panTransform.position;
        //    v3.y = v3.y + yOffset;
        //    v3.z = -1;
        //    transform.position = v3;
        //
        #endregion
    }


    /// <summary>
    /// 
    /// Check bacon states against current time, and when a new cook time milestone is reached advance
    /// to the next state and sprite
    /// 
    /// The function of flipping pieces has been removed until a convenient method to distinguish single clicks from onDrag
    /// can be identified. For now cooking will be a "raw -> cooked -> burnt" process
    /// 
    /// </summary>
    private void CheckBaconState()
    {
        if (baconState == BaconState.baconRaw && timer.currentTime < cookedTime)
        {
//            Debug.Log("Bacon - Raw");
            baconState = BaconState.baconRaw;
            sprite.sprite = baconSprites[rawIndex];
        }
        else if (baconState == BaconState.baconRaw && timer.currentTime >= cookedTime)
        {
//            Debug.Log("Bacon - Cooked");
            baconState = BaconState.baconCooked;
            sprite.sprite = baconSprites[cookedIndex];
        }
        else if (baconState == BaconState.baconCooked && timer.currentTime >= burnedTime)
        {
//            Debug.Log("Bacon - Burned");
            baconState = BaconState.baconBurned;
            sprite.sprite = baconSprites[burnedIndex];
        }
    }

    void OnMouseOver()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //float currentTime = timer.currentTime;
            
        //    float yOffset = transform.position.y;
        //    int yOffsetIndex = 0;
            
        //    // If the player clicks a cooked piece of bacon, flip it over and cook the other side
        //    // Take 5 seconds off the timer
        //    if ((baconState == BaconState.baconRaw) && (currentTime > cookedTime) 
        //        && sprite.sprite == baconSprites[cookedIndex])
        //    {
        //        baconState = BaconState.baconFlipped;
        //        sprite.sprite = baconSprites[rawIndex];
        //        timer.currentTime -= flippedTimeReduction;
        //    }
        //    if((baconState == BaconState.baconCooked) && (currentTime > cookedTime))
        //    {
        //        // Increase score
        //        gc.score++;
        //        gc.baconCount--;

        //        //killBacon(yOffset, yOffsetIndex);

        //    }
        //    // If bacon burns
        //    if (baconState == BaconState.baconBurned)
        //    {
        //        // Check fail count, increase fail count and destroy goBacon
        //        if (gc.failCount < gc.maxFails)
        //        {
        //            gc.failCount++;
        //            gc.baconCount--;

        //            //killBacon(yOffset, yOffsetIndex);
        //        }
        //    }
        //}
    }

    //void killBacon(float yOffset, int yOffsetIndex)
    //{

    //    // Identify and count the BaconSpawners in the scene
    //    BaconSpawner[] baconSpawners;
    //    baconSpawners = FindObjectsOfType<BaconSpawner>();
    //    int baconSpawnersCount = baconSpawners.Length;

    //    // Identity the BaconSpawner alligned with this Bacon piece and set its occupied boolean to false
    //    for(int i = 0; i < baconSpawnersCount; i++)
    //    {
    //        if (baconSpawners[i].transform.position == this.transform.position)
    //            baconSpawners[i].occupiedSpawnPoint = false;
    //    }       

    //    // Destroy the strip
    //    Destroy(gameObject);
    //}

    /// <summary>
    /// 
    /// Move bacon from the spawn panel (Next Piece) to the pan or from the pan to the finished panel (Cooked Pieces)
    /// Should also be able to move pieces between points inside the pan
    /// 
    /// Must register a Vector2 mouse point in worldspace, and a release within appropriate zones. Should pull the heat multiplier from the 
    /// Vector2 of any HeatZone in which it is released
    /// 
    /// </summary>


    void OnMouseDrag()
    {

        // Obtain mouse position and correct for camera view
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Have bacon follow the mouse while it is dragged
        transform.position = mousePos;
    }

    void OnMouseUp()
    {
        // Acquire mouse position and correct for camera view
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Perform check for collision with HeatZoneColliders
        if (CheckHeatZoneCollision(mousePos))
            Debug.Log("Collision Registered");
        else
            Debug.Log("No Collision Registered");
    }

    /// <summary>
    /// Perform a check for a collision between the mouse position and one of the HeatZoneColliders.
    /// </summary>
    /// <param name="testPos"></param>
    /// <returns bool "bool_Collision"></returns>
    private bool CheckHeatZoneCollision(Vector2 testPos)
    {
        // Boolean test for HeatZone Collision
        bool bool_Collision = false;

        // Check for collision with heatzones
        if (heatZoneColliders[2].GetComponent<CircleCollider2D>().OverlapPoint(testPos))
        {
//            Debug.Log("Heat Zone " + heatZoneColliders[2].heatMultiplier.ToString());
            bool_Collision = true;
        }
        else if (heatZoneColliders[1].GetComponent<CircleCollider2D>().OverlapPoint(testPos))
        {
//            Debug.Log("Heat Zone " + heatZoneColliders[1].heatMultiplier.ToString());
            bool_Collision = true;
        }
        else if (heatZoneColliders[0].GetComponent<CircleCollider2D>().OverlapPoint(testPos))
        {
//            Debug.Log("Heat Zone " + heatZoneColliders[0].heatMultiplier.ToString());
            bool_Collision = true;
        }

        // Return the results of the collision check
        return bool_Collision;
    }
}
