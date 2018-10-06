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

    public Vote v;

    // Use this for initialization
    void Start () {

        for(int i=0;i<v.nomProposition.Count; i++)
        {
            Debug.Log(v.nomProposition[i]);
            Debug.Log(v.listEffects[i].effects.Count);
            for (int j = 0; j < v.listEffects[i].effects.Count; j++)
            {

                Debug.Log(v.listEffects[i].effects[j].ToString());

            }
        }

        etat = EtatGame.bataille;
        time = Time.time;
        listEffectEnCours = new List<Effect>();
    }
	
	// Update is called once per frame
	void Update () {
        if (etat.Equals(EtatGame.bataille))
        {
            DisplayEffects();
            if (timerChrono <= (Time.time - time))
            {
                time = Time.time;
                etat = EtatGame.vote;
                StartVote();
            }
        }
        if (etat.Equals(EtatGame.vote))
        {
            if (timerVote <= (Time.time - time))
            {
                time = Time.time;
                EndEffects();               
                StopVote();
                BeginEffects();
                etat = EtatGame.bataille;
            }
        }


    }

    private void DisplayEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.Display();
        }
    }
    private void EndEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.End();
        }
    }
    private void BeginEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.Begin();
        }
    }

    private void StopVote()
    {
        Debug.Log("Fin Vote");
    }

    private void StartVote()
    {
        Debug.Log("Debut Vote");
        
    }
    
}

public enum EtatGame
{
    preparation,
    vote,
    bataille,
}
