using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float timerChrono;
    public float timerVote;
    public GameObject StartBuzzer;
    private float time;
    private List<Effect> listEffectEnCours;
    private EtatGame etat;
    private MultiplayerManager multiplayerManager;

    public Vote v;

    // Use this for initialization
    void Start () {

        multiplayerManager = GetComponent<MultiplayerManager>();
        if (!multiplayerManager)
            Debug.LogWarning("Pas de MultiplayerManager sur le game object");
        /*
        for(int i=0;i<v.nomProposition.Count; i++)
        {
            Debug.Log(v.nomProposition[i]);
            Debug.Log(v.listEffects[i].effects.Count);
            for (int j = 0; j < v.listEffects[i].effects.Count; j++)
            {

                Debug.Log(v.listEffects[i].effects[j].ToString());

            }
        }
        */
        EnterPreparation();
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

        // On vérifie le nombre de joueurs encore en vie si on est pas en préparation
        if (!etat.Equals(EtatGame.preparation))
        {
            multiplayerManager.CheckMultiplayerEnding();
        }

    }


    // Met le jeu en état de préparation et place un buzzer au centre de la zone de jeu
    public void EnterPreparation()
    {
        // Instanciation du Start Buzzer
        Instantiate(StartBuzzer);
        StartBuzzer.transform.position = new Vector3(0,1.1f,0);
        StartBuzzer.GetComponent<BuzzerStart>().multiplayerManager = multiplayerManager;

        multiplayerManager.InitializePlayers();
        etat = EtatGame.preparation;
    }


    // Sort de l'état de préparation
    public void StartRound()
    {
        // Suppression du buzzer
        Destroy(StartBuzzer);
       
        multiplayerManager.InitializePlayers();
        etat = EtatGame.bataille;
    }

    // Appelé lorsqu'il ne reste plus qu'un joueur en vie. Affiche le vainqueur et remet le jeu en état de préparation
    public void EndRound(string winner)
    {
        Debug.Log(winner);  // A AFFICHER IN GAME
        EnterPreparation();
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
