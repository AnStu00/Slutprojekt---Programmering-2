using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausMeny : MonoBehaviour {

    public GameObject PausUI;

    private bool pausat = false;

	// Use this for initialization
	void Start ()
    {
        PausUI.SetActive(false);    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Pausa"))
        {
            pausat = !pausat;
        }

        if (pausat)
        {
            PausUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!pausat)
        {
            PausUI.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void Forsätt()
    {
        pausat = false;
    }

    public void StartaOm()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void HuvudMeny()
    {
        //Application.LoadLevel[0]; Har ingen huvudmeny ännu så kan inte ladda den scenen.
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void Avsluta()
    {
        Application.Quit();
    }
}
