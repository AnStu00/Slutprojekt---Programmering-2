using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meny_Knappar : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject LevelSelectPanel;
    // Use this for initialization
    void Start()
    {
        MenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }
    public void ShowLevelPanel()
    {
        MenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }
}
