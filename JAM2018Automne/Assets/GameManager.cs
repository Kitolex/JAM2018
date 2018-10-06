using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float timerChrono;
    public float timerVote;
    private float time;
    private List<Effect> listEffectEnCours;
    private EtatGame etat;

    // Use this for initialization
    void Start () {
        etat = EtatGame.bataille;
        time = Time.time;
        listEffectEnCours = new List<Effect>();
    }
	
	// Update is called once per frame
	void Update () {
        if (etat.Equals(EtatGame.bataille))
        {
            foreach (Effect e in listEffectEnCours)
            {
                e.Display();
            }           

            if (timerChrono <= (Time.time - time))
            {
                time = Time.time;
                StartVote();
            }
        }
        if (etat.Equals(EtatGame.vote))
        {
            if (timerChrono <= (Time.time - time))
            {
                time = Time.time;
                StopVote();
            }
        }


    }

    private void StopVote()
    {
        Debug.Log("Fin Vote");
        etat = EtatGame.bataille;
    }

    private void StartVote()
    {
        Debug.Log("Debut Vote");
        etat = EtatGame.vote;
    }

    public void AddEffect()
    {

    }

    
}

public enum EtatGame
{
    preparation,
    vote,
    bataille,
}
