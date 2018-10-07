using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TVManager : MonoBehaviour {

    public static TVManager tvDisplay;
    public Text textDisplay;
    private GameManager gameManager;

	// Use this for initialization
	void Awake () {
        gameManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<GameManager>();
        tvDisplay = this;
    }

    public void DisplayPreparation()
    {
        textDisplay.text = "Unforgettable \n Random \n Survival \n Show";
    }

    public void DisplayTimer(float maxTimer, float timer)
    {
        textDisplay.text = (Mathf.RoundToInt(maxTimer) - Mathf.RoundToInt(timer)).ToString();
    }

    public void DisplayWinner(string winner)
    {
        textDisplay.text = "Bravo " + winner;
    }

    public void DisplayVoteResult(string response)
    {
        textDisplay.text = response;
    }

}
