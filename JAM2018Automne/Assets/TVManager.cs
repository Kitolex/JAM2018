using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TVManager : MonoBehaviour {

    public Text timerDisplay;
    public Text oneTimeText;

    private int phaseTime;
    private int voteTime;
    private GameManager gameManager;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameeManager").GetComponent<GameManager>();
        oneTimeText.text = "Unforgettable \n Random \n Survival \n Show";
    }

    // Update is called once per frame
    void ForceUpdateCanvases() {        

        if (gameManager.etat.Equals(EtatGame.preparation))
        {
            timerDisplay.GetComponent<Text>().enabled = false;
            oneTimeText.text = "Unforgettable \n Random \n Survival \n Show";
        }
        //if (gameManager.etat.Equals(EtatGame.bataille))
        if (EtatGame.bataille.Equals(EtatGame.bataille))
        {
            oneTimeText.text = "";
            timerDisplay.text = Mathf.RoundToInt(gameManager.timerChrono - gameManager.time).ToString();
        }
        if (gameManager.etat.Equals(EtatGame.vote))
        {
            oneTimeText.text = "";
            timerDisplay.text = Mathf.RoundToInt(gameManager.timerVote - gameManager.time).ToString();
        }

        // TODO : enlever commentaire + else, modifier nom EtatGame si nécessaire
     // if (gameManager.etat.Equals(EtatGame.fin))
        else
        {
            timerDisplay.GetComponent<Text>().enabled = false;
            oneTimeText.text = "Bravo "+ gameManager.winner;
        }
		
	}
}
