using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int score = 0;
    public Text scoreField;

    public GameObject bacon;
    public Transform pan;

    public float yOffset1;
    public float yOffset2;

	// Use this for initialization
	void Start () {
        
        pan = FindObjectOfType<Pan>().transform;

        // If no bacon exists in the scene, place bacon
        if (!FindObjectOfType<Bacon>())
        {
            Vector3 v3 = pan.position;
            v3.y = v3.y + yOffset1;
            Instantiate(bacon, v3, Quaternion.identity);

            v3 = pan.transform.position;
            v3.y = v3.y + yOffset2;
            Instantiate(bacon, v3, Quaternion.identity);

            Debug.Log("Bacon");
        }
    }
	
	// Update is called once per frame
	void Update () {
        scoreField.text = score.ToString();
       
    }
}
