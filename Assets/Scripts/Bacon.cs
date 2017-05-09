using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bacon : MonoBehaviour {

    public Sprite[] baconSprites;
    public int rawIndex = 0;
    public int cookedIndex;

    private SpriteRenderer sprite;
    private Transform panTransform;
    private float yOffset;

    void Awake()
    {
        yOffset = transform.position.y;
    }
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = baconSprites[rawIndex];

        panTransform = FindObjectOfType<Pan>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v3 = panTransform.position;
        v3.y = v3.y + yOffset;
        transform.position = v3;

	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sprite.sprite = baconSprites[cookedIndex];
        }
    }
}
