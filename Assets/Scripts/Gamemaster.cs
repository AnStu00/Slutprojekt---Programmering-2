using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemaster : MonoBehaviour {
    //Hittils bara ett super simpelt pengarsystem
    public int pengar;

    public Text pengarText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pengarText.text = ("Pengar: " + pengar);
		
	}
}
