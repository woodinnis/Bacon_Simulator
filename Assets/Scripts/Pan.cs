using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Pan : MonoBehaviour
{

    public float xOffset;
    public float yOffset;
    public Vector3 DefaultV3;

    private Text text;

    // Use this for initialization
    void Start()
    {

        //text = FindObjectOfType<Text>();

        DefaultV3 = Vector3.zero;

        DebugCurrentPanOffset();
        //text.text = "I'm a pan";
    }

    // Verify the mouse is over the pan
    void OnMouseOver()
    {
        CheckMousePositionAndMovePan();

        GameController gc = FindObjectOfType<GameController>();
        float offsetTest = gc.panOffsetCheck;

        Bacon[] bacon;
        bacon = FindObjectsOfType<Bacon>();

        Vector3 V3 = transform.position;

        // Check pan position and set offsets
        xOffset = DefaultV3.x + V3.x;
        yOffset = DefaultV3.y + V3.y;

        if (xOffset >= offsetTest || yOffset >= offsetTest)
        {
            foreach(Bacon b in bacon)
            {
                b.timer.targetTime -= gc.panShakeTimeReduction;
                Debug.Log("Time for " + b + " reduced to " + b.timer.targetTime);
            }
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