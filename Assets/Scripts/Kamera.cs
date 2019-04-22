using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

    private Vector2 hastighet;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject spelare;
    // Use this for initialization
    void Start () {
        spelare = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float posX = Mathf.SmoothDamp(transform.position.x, spelare.transform.position.x, ref hastighet.x, smoothTimeX);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

	}
}
