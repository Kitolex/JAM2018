using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {

    public float timerChrono;
    public float timerVote;
    private float time;
   
    private EtatGame etat;
    private VoteManager voteManager;
    private EffectManager effectManager;

    public Vote v;

    // Use this for initialization
    void Start () {
        voteManager = GameObject.FindGameObjectWithTag("VoteManager").GetComponent<VoteManager>();
        effectManager = GameObject.FindGameObjectWithTag("EffectManager").GetComponent<EffectManager>();
        etat = EtatGame.bataille;
        time = Time.time;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (etat.Equals(EtatGame.bataille))
        {
            effectManager.DisplayEffects();
            if (timerChrono <= (Time.time - time))
            {
                time = Time.time;
                etat = EtatGame.vote;
                voteManager.startVote();
            }
        }
        if (etat.Equals(EtatGame.vote))
        {
            if (timerVote <= (Time.time - time))
            {
                time = Time.time;
                effectManager.EndEffects();               
                StopVote();
                effectManager.BeginEffects();
                etat = EtatGame.bataille;
            }
        }


    }

    

    private void StopVote()
    {
        List<ListEffet> listEffect = new List<ListEffet>();
        listEffect.AddRange(voteManager.getEffect());
        effectManager.createliste(listEffect);
    }


    
}

public enum EtatGame
{
    preparation,
    vote,
    bataille,
}
