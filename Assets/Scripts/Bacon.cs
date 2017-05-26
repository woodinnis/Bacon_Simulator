using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bacon : MonoBehaviour {

    public Sprite[] baconSprites;
    public int rawIndex = 0;
    public int cookedIndex;
    public int burnedIndex;

    [HideInInspector]
    public enum BaconState { baconRaw, baconFlipped, baconCooked, baconBurned };
    [HideInInspector]
    public BaconState baconState;

    public float cookedTime;
    public float flippedTimeReduction;
    public float burnedTime;

    private GameController gc;
    
    private SpriteRenderer sprite;
    //private Transform panTransform;
    //private float yOffset;

    [HideInInspector]
    public Timer timer;

    void Awake()
    {
        // Set starting state of the bacon;
        baconState = BaconState.baconRaw;

        // Find Y-axis offset to maintain position relative to the pan
        //yOffset = transform.position.y;

        // Find a text field in the current scene
        //textField = FindObjectOfType<Text>();

        // Check for a timer component attached to the current object
        if((timer == null) && (GetComponent<Timer>() != null)){
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

        // Set timer based on slice time
        timer.targetTime = cookedTime;

        //panTransform = FindObjectOfType<Pan>().transform;

        gc = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {

        // Track the pan position and follow it
        /*
            Vector3 v3 = panTransform.position;
            v3.y = v3.y + yOffset;
            v3.z = -1;
            transform.position = v3;
        */

        // Update the displayed time
        float currentTime = timer.currentTime;

        // Update the sprite based on the current timer

        // If Bacon has reached a cooked state, but not been flipped
        if (currentTime > cookedTime && baconState == BaconState.baconRaw)
        {
            sprite.sprite = baconSprites[cookedIndex];
        }
        // If Bacon has reached a cooked state, and been flipped
        else if(currentTime > cookedTime && baconState == BaconState.baconFlipped)
        {
            baconState = BaconState.baconCooked;
            sprite.sprite = baconSprites[cookedIndex];
        }

        // If bacon has burned
        if(currentTime >= burnedTime)
        {
            baconState = BaconState.baconBurned;
            sprite.sprite = baconSprites[burnedIndex];
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float currentTime = timer.currentTime;

            // If the player clicks a cooked piece of bacon, flip it over and cook the other side
            // Take 5 seconds off the timer
            if ((baconState == BaconState.baconRaw) && (currentTime > cookedTime) 
                && sprite.sprite == baconSprites[cookedIndex])
            {
                baconState = BaconState.baconFlipped;
                sprite.sprite = baconSprites[rawIndex];
                timer.currentTime -= flippedTimeReduction;
            }
            if((baconState == BaconState.baconCooked) && (currentTime > cookedTime))
            {
                // Increase score
                gc.score++;
                gc.baconCount--;

                // Remove bacon from pan
                Destroy(gameObject);
            }
            // If bacon burns
            if (baconState == BaconState.baconBurned)
            {
                // Check fail count, increase fail count and destroy goBacon
                if (gc.failCount < gc.maxFails)
                {
                    gc.failCount++;
                    gc.baconCount--;

                    Destroy(gameObject);
                }
            }
        }
    }

    //void OnMouseDrag()
    //{
    //    timer.isPaused = true;

    //    Vector3 v3 = Vector3.zero;
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

    //    v3.x = 0.0f;
    //    v3.y = mousePos.y;
    //    transform.position = v3;
    //}
    //void OnMouseUp()
    //{
    //    if (timer.isPaused)
    //        timer.isPaused = false;
    //}
}
