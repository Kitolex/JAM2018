﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {

    public float timerChrono;
    public float timerVote;
    public GameObject StartBuzzer;

    private GameObject buzzerInstance;
    private float time;
   
    public EtatGame etat;
    private MultiplayerManager multiplayerManager;
    private VoteManager voteManager;
    private EffectManager effectManager;

    // Use this for initialization
    void Start () {

        multiplayerManager = GetComponent<MultiplayerManager>();
        if (!multiplayerManager)
            Debug.LogWarning("Pas de MultiplayerManager sur le game object");
        
        voteManager = GameObject.FindGameObjectWithTag("VoteManager").GetComponent<VoteManager>();
        effectManager = GameObject.FindGameObjectWithTag("EffectManager").GetComponent<EffectManager>();
      
        EnterPreparation();
        

    }
	
	// Update is called once per frame
	void Update () {
        // On vérifie le nombre de joueurs encore en vie si on est pas en préparation
        if (!etat.Equals(EtatGame.preparation))
        {
            multiplayerManager.CheckMultiplayerEnding();
        }

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


    // Met le jeu en état de préparation et place un buzzer au centre de la zone de jeu
    public void EnterPreparation()
    {
        // Instanciation du Start Buzzer
        buzzerInstance = Instantiate(StartBuzzer);
        buzzerInstance.transform.position = new Vector3(0, 2.5f, 0);
        buzzerInstance.GetComponent<BuzzerStart>().multiplayerManager = multiplayerManager;
        buzzerInstance.GetComponent<BuzzerStart>().gameManager = this;

        multiplayerManager.InitializePlayers();
        etat = EtatGame.preparation;
    }

    // Sort de l'état de préparation
    public void StartRound()
    {
        // Suppression du buzzer
        Destroy(buzzerInstance);
       
        multiplayerManager.InitializePlayers();
        time = Time.time;
        etat = EtatGame.bataille;
    }

    // Appelé lorsqu'il ne reste plus qu'un joueur en vie. Affiche le vainqueur et remet le jeu en état de préparation
    public void EndRound(string winner)
    {
        Debug.Log(winner);  // A AFFICHER IN GAME
        EnterPreparation();
        effectManager.EndEffects();
    }

    private void StopVote()
    {
        Debug.Log("STOPVOTE");
        ListEffet listEffect = voteManager.getEffect();
        effectManager.createliste(listEffect);
        voteManager.destroyBuzzer();
    }
}

public enum EtatGame
{
    preparation,
    vote,
    bataille,
}
