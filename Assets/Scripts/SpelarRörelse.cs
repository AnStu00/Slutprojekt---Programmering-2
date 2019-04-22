using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelarRörelse : MonoBehaviour
{
    //Lite ostrukturerat eftersom pengarna ligger även här. Men detta är så spelaren kan röra sig.

    public SpelarKontroll rörelse;

    public float hastighet = 40f;
    Gamemaster gamemaster;

    public bool KanDubellHoppa;
    float HorizontalMove = 0f;
    bool hoppa = false;
    bool ducka = false;
    // Use this for initialization
    void Start()
    {
        gamemaster = GameObject.FindGameObjectWithTag("Gamemaster").GetComponent<Gamemaster>();
    }

    // Update is called once per frame
    void Update()
    {

        HorizontalMove = Input.GetAxisRaw("Horizontal") * hastighet;
        if (Input.GetButtonDown("Jump"))
        {
            if (rörelse.Grounded)
            {
                KanDubellHoppa = true;
                hoppa = true;
            }
            else
            {
                if (KanDubellHoppa)
                {
                    KanDubellHoppa = false;
                    hoppa = true;
                }
            }
        }
        if (Input.GetButtonDown("Ducka"))
        {
            ducka = true;
        }
        else if (Input.GetButtonUp("Ducka"))
        {
            ducka = false;
        }
    }

    void FixedUpdate()
    {
        rörelse.Move(HorizontalMove * Time.fixedDeltaTime, ducka, hoppa);
        hoppa = false;


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pengar"))
        {
            Destroy(col.gameObject);
            gamemaster.pengar += 10;
        }
        if (col.CompareTag("Blåpeng"))
        {
            Destroy(col.gameObject);
            gamemaster.pengar += 1;
        }
    }
}
