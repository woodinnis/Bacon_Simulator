using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bacon : MonoBehaviour {

    public Sprite[] baconSprites;
    public int rawIndex = 0;
    public int cookedIndex;
    public int burnedIndex;

    public float cookedTime;
    public float burnedTime;

    private SpriteRenderer sprite;
    private Transform panTransform;
    private float yOffset;

    private Timer timer;
    private Text textField;

    void Awake()
    {
        yOffset = transform.position.y;

        // Find a text field in the current scene
        textField = FindObjectOfType<Text>();

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

        textField.text = cookedTime.ToString();

        panTransform = FindObjectOfType<Pan>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v3 = panTransform.position;
        v3.y = v3.y + yOffset;
        transform.position = v3;

        // Update the displayed time
        float currentTime = timer.currentTime;
        textField.text = currentTime.ToString();

        // Update the sprite based on the current timer
        if (currentTime > cookedTime)
        {
            sprite.sprite = baconSprites[cookedIndex];
        }
        if(currentTime >= burnedTime)
        {
            sprite.sprite = baconSprites[burnedIndex];
        }

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sprite.sprite = baconSprites[cookedIndex];
        }
    }
}
