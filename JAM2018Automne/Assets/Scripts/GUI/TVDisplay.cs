using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TVDisplay : MonoBehaviour {

    public Text tvDisplay;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<GameManager>();
        tvDisplay = GetComponent<Text>() as Text;
    }

    // Update is called once per frame
    void Update() {

        if (gameManager.etat.Equals(EtatGame.preparation))
        {
            tvDisplay.text = "Unforgettable \n Random \n Survival \n Show";
        }
        if (gameManager.etat.Equals(EtatGame.bataille))
        {
            tvDisplay.text = Mathf.RoundToInt(gameManager.timerChrono - gameManager.time).ToString();
        }
        if (gameManager.etat.Equals(EtatGame.vote))
        {
            tvDisplay.text = Mathf.RoundToInt(gameManager.timerVote - gameManager.time).ToString();
        }

        // TODO : enlever commentaire + else, modifier nom EtatGame si nécessaire
     // if (gameManager.etat.Equals(EtatGame.fin))
        else
        {
            tvDisplay.text = "Bravo "+ gameManager.winner;
        }
    }
}
