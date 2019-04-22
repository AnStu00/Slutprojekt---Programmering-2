using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bakgrund : MonoBehaviour {
    //För att bakgrunden ska se nartuligt ut när den följer spelaren, underlättar också för mig som slipper lägga ut många bakgrunder
    [SerializeField]
    private Transform centerBakgrund;
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (transform.position.x >= centerBakgrund.position.x + 10.94f)
        centerBakgrund.position = new Vector2(centerBakgrund.position.x, transform.position.x + 10.94f);

        else if (transform.position.y <= centerBakgrund.position.x - 10.94f)
            centerBakgrund.position = new Vector2(centerBakgrund.position.x, transform.position.x - 10.94f);
	}
}
