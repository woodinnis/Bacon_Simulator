using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int score = 0;
    public int failCount = 0;
    public int maxFails = 0;
    public int maxStrips = 0;
    public float panOffsetCheck = 0.0f;
    public float panShakeTimeReduction = 0.0f;

    public Text scoreField;
    public Text failField;
    public Text winLoseField;

    public GameObject bacon;

    // Pan values;
    public Transform panTransform;
    private Vector3 defaultPanPosition;

    public Button resetButton;

    public float yOffset1;
    public float yOffset2;

    void Awake()
    {
        SetResetButtonState(false);
    }

    // Use this for initialization
    void Start () {
        
        panTransform = FindObjectOfType<Pan>().transform;
        defaultPanPosition = panTransform.position;

        // If no bacon exists in the scene, place bacon
        if (!FindObjectOfType<Bacon>())
        {
            MakinBacon();
        }
    }

    // Update is called once per frame
    void Update () {
        scoreField.text = score.ToString();
        failField.text = failCount.ToString();

        if (!FindObjectOfType<Bacon>())
        {
            MakinBacon();
        }

        if (failCount >= maxFails)
        {
            winLoseField.text = "You Lose!";
            SetResetButtonState(true);
        }
    }

    // Place two pieces of bacon in the pan
    private void MakinBacon()
    {
        Vector3 v3 = panTransform.position;
        v3.y = v3.y + yOffset1;
        Instantiate(bacon, v3, Quaternion.identity);

        v3 = panTransform.transform.position;
        v3.y = v3.y + yOffset2;
        Instantiate(bacon, v3, Quaternion.identity);

        Debug.Log("Bacon");
    }

    void SetResetButtonState(bool buttonState)
    {
        if (buttonState)
        {
            resetButton.enabled = true;
            resetButton.image.enabled = true;
            resetButton.GetComponentInChildren<Text>().enabled = true;
        }
        else
        {
            resetButton.enabled = false;
            resetButton.image.enabled = false;
            resetButton.GetComponentInChildren<Text>().enabled = false;
        }
    }

    public void ResetGameState()
    {
        
        foreach (Bacon b in FindObjectsOfType<Bacon>())
        {
            Debug.Log("Poof!");
            Destroy(b.gameObject);
        }

        Pan pan = FindObjectOfType<Pan>();
        pan.transform.position = defaultPanPosition;

        score = 0;
        failCount = 0;
        MakinBacon();
        SetResetButtonState(false);
    }

    // This cannot check for a MouseOver of the pan, because the pan is a separate object
    //void OnMouseOver()
    //{
    //    // Test for a mouse click
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // Test for a maximum number of bacon strips in the scene
    //        if (FindObjectsOfType<Bacon>().Length < maxStrips)
    //        {
    //            Debug.Log("No more bacon");
    //            Vector3 test1 = pan.position;
    //            test1.y = test1.y + yOffset1;

    //            Vector3 test2 = pan.position;
    //            test2.y = test2.y + yOffset2;

    //            if (!FindObjectOfType<Bacon>())
    //            {
    //                Vector3 v3 = pan.position;
    //                v3.y = v3.y + yOffset1;
    //                Instantiate(bacon, v3, Quaternion.identity);
    //            }
    //            else if (bacon.transform.position == (test1))
    //            {
    //                Instantiate(bacon, test1, Quaternion.identity);
    //            }
    //            else if (bacon.transform.position == (test2))
    //            {
    //                Instantiate(bacon, test2, Quaternion.identity);
    //            }
    //        }
    //    }
    //}
}
