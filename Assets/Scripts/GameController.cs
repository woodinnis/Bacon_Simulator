using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int score = 0;
    public int failCount = 0;
    public int maxFails = 0;
    public int maxStrips = 0;

    public Text scoreField;
    public Text failField;
    public Text winLoseField;

    public GameObject bacon;
    public Transform panTransform;

    public float yOffset1;
    public float yOffset2;

    // Use this for initialization
    void Start () {
        
        panTransform = FindObjectOfType<Pan>().transform;

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
            winLoseField.text = "You Lose!";
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
