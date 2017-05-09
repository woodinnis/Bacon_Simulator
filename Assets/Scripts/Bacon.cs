using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bacon : MonoBehaviour {

    public Sprite[] baconSprites;
    public int rawIndex = 0;
    public int cookedIndex;

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = baconSprites[rawIndex];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sprite.sprite = baconSprites[cookedIndex];
        }
    }
}
