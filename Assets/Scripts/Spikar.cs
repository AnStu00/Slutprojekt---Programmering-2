using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikar : MonoBehaviour {

    private SpelarKontroll spelare;

    private void Start()
    {
        spelare = GameObject.FindGameObjectWithTag("Player").GetComponent<SpelarKontroll>();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            spelare.Skada(1);

            StartCoroutine(spelare.OntAnim(0.2f, 5, spelare.transform.position));
        }
    }
}
