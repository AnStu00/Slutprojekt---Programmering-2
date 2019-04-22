using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpelarKontroll : MonoBehaviour
{
    //Tog lite hjälp från internet här eftersom jag ville ha ett nartuligt sätt som spelaren  agerar på.

    [SerializeField] private float hoppaForce = 400f;                          // Kraften som blir när spelaren hoppas
    [Range(0, 1)] [SerializeField] private float duckaSpeed = .36f;          // Duckhastigheten i procent
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // För att göra hastigheten mer smooth
    [SerializeField] private bool AirControl = false;                         // Om man kan röra på sig när spelaren hoppar, la till denna för man inte alltid ska kunna röra sig när man hoppar.
    [SerializeField] private LayerMask WhatIsGround;                          // Vad marken befinner sig i förhållande till spelaren, så att man ska kunna hoppa.
    [SerializeField] private Transform GroundCheck;                           // En check som kollar om spelaren är på marken
    [SerializeField] private Transform CeilingCheck;                          // Kollar om det finns något ovanför
    [SerializeField] private Collider2D duckaDisableCollider;                // Collidern på toppen av spelaren kommer stängas av när man duckar så man kan tränga sig igenom trånga utrymmen.

    //Liv systemet

    public int Liv;
    public int MaxLiv = 4;

    const float GroundedRadius = .2f; // Radiusen av cirkelkollidern för att se om man ärt på marken.
    public bool Grounded;            // Kollar om spelaren är på marken
    const float CeilingRadius = .2f; // Kollar om spelaren kan stå upp eller om det finns något ovanför
    private Rigidbody2D Rigidbody2D;
    private bool FacingRight = true;  // Kollar vilket håll spelaren kollar
    private Vector3 Velocity = Vector3.zero;


    private void Start()
    {
        Liv = MaxLiv; //För man ska alltid starta med fullt liv
    }

    private void Update()
    {
        if(Liv > MaxLiv)
        {
            Liv = MaxLiv;
        }
        if(Liv <= 0)
        {
            Dö();
        }
    }
    [Header("Events")]
    [Space]


    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnduckaEvent;
    private bool wasduckaing = false;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnduckaEvent == null)
            OnduckaEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = Grounded;
        Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool ducka, bool hoppa)
    {
        // Om spelaren duckar så kollar denna om spelaren kan stå upp
        if (!ducka)
        {
            // Spelaren kommer fortsätta ducka om det finns något ovanför
            if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, WhatIsGround))
            {
                ducka = true;
            }
        }

        if (Grounded || AirControl)
        {

            // Om man duckar
            if (ducka)
            {
                if (!wasduckaing)
                {
                    wasduckaing = true;
                    OnduckaEvent.Invoke(true);
                }

                // Gör spelaren långsammare när han duckar
                move *= duckaSpeed;

                // Tar bort den översta collidern när han duckar
                if (duckaDisableCollider != null)
                    duckaDisableCollider.enabled = false;
            }
            else
            {
                // Sätter tillbaks collidern när han slutar duckar.
                if (duckaDisableCollider != null)
                    duckaDisableCollider.enabled = true;

                if (wasduckaing)
                {
                    wasduckaing = false;
                    OnduckaEvent.Invoke(false);
                }
            }

            // Flyttar spelaren
            Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
            // Gör det smoooooth!
            Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            // Om spelaren är åt höger men spelaren byter håll, då ska spelaren byta håll
            if (move > 0 && !FacingRight)
            {
                //Byter håll på spelaren
                Flip();
            }
            // Motsatsen till ovan
            else if (move < 0 && FacingRight)
            {
                Flip();
            }
        }
        //Om spelaren kan hoppa
        if (Grounded && hoppa)
        {
            // Lägger en kraft uppåt på spelaren
            Grounded = false;
            Rigidbody2D.AddForce(new Vector2(0f, hoppaForce));
        }
    }


    private void Flip()
    {
        // Byter sida
        FacingRight = !FacingRight;

        // multiplicerar spelaren x värden med -1 för att byta håll.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Dö()
    {
        //Omstart
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Skada(int skada)
    {
        Liv -= skada;
        gameObject.GetComponent <Animation>().Play("Spelare_Skada");
    }

    public IEnumerator OntAnim(float OntTid, float OntKraft, Vector3 OntDir)
    {
        float timer = 0;

        while(OntTid > timer)
        {
            timer += Time.deltaTime;
            Rigidbody2D.AddForce(new Vector3(OntDir.x * -75, OntDir.y * OntKraft, transform.position.z));

        }
        yield return 0; //Stoppa OntAnim
    }
}