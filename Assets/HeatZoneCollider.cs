using UnityEngine;
using System.Collections;

public class HeatZoneCollider : MonoBehaviour {

    public float radius = 0.0f;
    public float heatMultiplier= 0.0f;

	// Use this for initialization
	void Start () {
        GetComponent<CircleCollider2D>().radius = radius;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
