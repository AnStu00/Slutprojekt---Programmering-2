using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Liv : MonoBehaviour {

    public Sprite[] LivSprites;

    public Image LivUI;

    private SpelarKontroll Spelare;

    private void Start()
    {
        Spelare = GameObject.FindGameObjectWithTag("Player").GetComponent<SpelarKontroll>();

    }
    private void Update()
    {
        LivUI.sprite = LivSprites[Spelare.Liv];
    }
}
