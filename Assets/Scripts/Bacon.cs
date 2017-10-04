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
    public enum BaconState { baconRaw, baconFlipped, baconCooked, baconBurned };
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
    //public Timer timer;

    void Awake()
    {
        // Set starting state of the bacon;
        baconState = BaconState.baconRaw;

        // Check for a timer component attached to the current object
        //if((timer == null) && (GetComponent<Timer>() != null)){
        //    timer = GetComponent<Timer>();
        //}
        //else
        //{
        //    Debug.Log("No Timer Attached");
        //}
    }

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = baconSprites[rawIndex];

        // Set timer based on slice time
        //timer.targetTime = cookedTime;

        // Find and set the Pan
        pan = FindObjectOfType<Pan>();

        // Find a text field in the current scene
        BaconDebugText = FindObjectOfType<Text>();

        // Retrieve the Heat Zones from the pan
        for (int i = 0; i < pan.GetComponent<Pan>().HeatZoneCollider_HeatZones.Length; i++)
        {
            heatZoneColliders[i] = pan.GetComponent<Pan>().HeatZoneCollider_HeatZones[i];
        }

        gc = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {

        //Track the pan position and follow it
        ///*
        //    Vector3 v3 = panTransform.position;
        //    v3.y = v3.y + yOffset;
        //    v3.z = -1;
        //    transform.position = v3;
        //*/

        //Update the displayed time
        //float currentTime = timer.currentTime;

        //Update the sprite based on the current timer

        // If Bacon has reached a cooked state, but not been flipped
        //if (currentTime > cookedTime && baconState == BaconState.baconRaw)
        //{
        //    sprite.sprite = baconSprites[cookedIndex];
        //}
        //If Bacon has reached a cooked state, and been flipped
        //else if (currentTime > cookedTime && baconState == BaconState.baconFlipped)
        //{
        //    baconState = BaconState.baconCooked;
        //    sprite.sprite = baconSprites[cookedIndex];
        //}

        //If bacon has burned
        //if (currentTime >= burnedTime)
        //{
        //    baconState = BaconState.baconBurned;
        //    sprite.sprite = baconSprites[burnedIndex];
        //}
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

        // Check for collision with heatzones
        if (heatZoneColliders[2].GetComponent<CircleCollider2D>().OverlapPoint(mousePos))
        {
            Debug.Log("Heat Zone " + heatZoneColliders[2].heatMultiplier.ToString());
        }
        else if (heatZoneColliders[1].GetComponent<CircleCollider2D>().OverlapPoint(mousePos))
        {
            Debug.Log("Heat Zone " + heatZoneColliders[1].heatMultiplier.ToString());
        }
        else if (heatZoneColliders[0].GetComponent<CircleCollider2D>().OverlapPoint(mousePos))
        {
            Debug.Log("Heat Zone " + heatZoneColliders[0].heatMultiplier.ToString());
        }
    }
}
