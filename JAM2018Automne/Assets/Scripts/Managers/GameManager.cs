using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {

    public float timerChrono;
    public float timerVote;
    public GameObject StartBuzzer;

    private GameObject buzzerInstance;
    public float time;
   
    public EtatGame etat;
    public string winner;
    private MultiplayerManager multiplayerManager;
    private VoteManager voteManager;
    private EffectManager effectManager;
    private MapManager mapManager;

    private int multiCHance;//pas touche
    public int chanceInit;
    public int augmentationChance;

    private bool activeDescente;
    public float whenDescente;

    private Random rnd;

    private bool firstPrepa;


    // Use this for initialization
    void Start () {
        firstPrepa = true;
        activeDescente = false;
        multiplayerManager = GetComponent<MultiplayerManager>();
        if (!multiplayerManager)
            Debug.LogWarning("Pas de MultiplayerManager sur le game object");
        
        voteManager = GameObject.FindGameObjectWithTag("VoteManager").GetComponent<VoteManager>();
        effectManager = GameObject.FindGameObjectWithTag("EffectManager").GetComponent<EffectManager>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        rnd = new Random();
        EnterPreparation();
        



    }
	
	// Update is called once per frame
	void Update () {
        // On vérifie le nombre de joueurs encore en vie si on est pas en préparation
        if (!etat.Equals(EtatGame.preparation) && !etat.Equals(EtatGame.postBataille))
        {
            multiplayerManager.CheckMultiplayerEnding();
        }

        if (etat.Equals(EtatGame.bataille))
        {
            if (activeDescente)
            {
                
                if (whenDescente <= (Time.time - time))
                {
                    mapManager.activeDescente();
                    if (multiCHance < 2)
                    {
                        multiCHance = 0;
                    }
                    else
                    {
                        multiCHance -= 2;
                    }                    
                    activeDescente = false;
                }
            }
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
                multiCHance++;
                calculChanceMap();
            }
        }
    }

    public void calculChanceMap()
    {
        int chance = rnd.Next(0, 101);
        if (chance< (chanceInit+(augmentationChance*multiCHance)))
        {
            activeDescente = true;
            whenDescente = rnd.Next(0,(int)(timerChrono*0.8));
        }
    }


    // Met le jeu en état de préparation et place un buzzer au centre de la zone de jeu
    public void EnterPreparation()
    {
        
        // Instanciation du Start Buzzer
        buzzerInstance = Instantiate(StartBuzzer);
        buzzerInstance.transform.position = new Vector3(0, 3.0f, 0);
        buzzerInstance.GetComponent<BuzzerStart>().multiplayerManager = multiplayerManager;
        buzzerInstance.GetComponent<BuzzerStart>().gameManager = this;
        multiplayerManager.InitializePlayers();
        etat = EtatGame.preparation;
        multiCHance = 0;
        if (firstPrepa)
        {
            //TODO : dialogue init
        }
        else
        {
            mapManager.InitMap();
        }

        firstPrepa = false;
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
        this.winner = winner;       
        effectManager.EndEffects();
        StartCoroutine(PostBataille());
    }

    public IEnumerator PostBataille()
    {
        etat = EtatGame.postBataille;
        yield return new WaitForSeconds(5);
        EnterPreparation();
    }

    private void StopVote()
    {
        Debug.Log("STOPVOTE");
        ListEffet listEffect = voteManager.getEffect();
        if (listEffect.effects != null)
        {
            afficheReponse(listEffect.reponse);
            effectManager.createliste(listEffect);
        }
        else
        {
            mapManager.activeDescente();
        }

        voteManager.destroyBuzzer();
    }

    private void afficheReponse(string reponse)
    {
        Debug.Log(reponse);
    }
}

public enum EtatGame
{
    preparation,
    vote,
    bataille,
    postBataille
}
