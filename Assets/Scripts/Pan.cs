using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Pan : MonoBehaviour {


    private Text text;
    
    // Use this for initialization
    void Start () {

        text = FindObjectOfType<Text>();

        text.text = "I'm a pan";
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    // Verify the mouse is over the pan
    void OnMouseOver()
    {
        CheckMousePositionAndMovePan();
    }

    // Move the pan with the mouse
    private void CheckMousePositionAndMovePan()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        float yOffset = collider.offset.y;

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            text.text = mousePos.ToString();
            mousePos.z = 0;
            mousePos.y = mousePos.y - yOffset;

            transform.position = mousePos;
        }
        else
            text.text = "I'm a pan";
    }
}
