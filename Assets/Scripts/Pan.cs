using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Pan : MonoBehaviour
{

    public float xOffset;
    public float yOffset;
    public Vector3 DefaultV3;
    public CircleCollider2D[] CircleCollider2D_HeatZones;

    public Text text;
    

    // Use this for initialization
    void Start()
    {

        //text = FindObjectOfType<Text>();

        DefaultV3 = Vector3.zero;

        DebugCurrentPanOffset();
    }

    void Update()
    {
        
    }

    void OnMouseOver()
    {

        // Obtain mouse position and correct for camera view
        Vector2 vector2_mousePosition = Input.mousePosition;
        vector2_mousePosition = Camera.main.ScreenToWorldPoint(vector2_mousePosition);

        if (CircleCollider2D_HeatZones[2].OverlapPoint(vector2_mousePosition))
        {
            text.text = "Zone 2";
            //text.text = vector2_mousePosition.ToString();
        }
        else if (CircleCollider2D_HeatZones[1].OverlapPoint(vector2_mousePosition))
        {
            text.text = "Zone 1";
            //text.text = vector2_mousePosition.ToString();
        }
        else if (CircleCollider2D_HeatZones[0].OverlapPoint(vector2_mousePosition))
        {
            text.text = "Zone 0";
            //text.text = vector2_mousePosition.ToString();
        }
        else
        {
            text.text = "No Zone";
        }

    }

    void OnMouseUp()
    {
        DefaultV3 = transform.position;

        xOffset = 0f;
        yOffset = 0f;
        DebugCurrentPanOffset();
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
            //text.text = mousePos.ToString();
            mousePos.z = 0;
            mousePos.y = mousePos.y - yOffset;

            transform.position = mousePos;
        }
        //else
        //text.text = "I'm a pan";
    }
    private void DebugCurrentPanOffset()
    {
        Debug.Log("Default: " + DefaultV3.ToString());
        Debug.Log("X Offset: " + xOffset);
        Debug.Log("Y Offset: " + yOffset);
    }
}