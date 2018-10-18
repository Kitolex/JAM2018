using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {

    public AudioClip BackgroundMusic;
    public AudioClip EndGame1;
    public AudioClip EndGame2;
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

  

    private bool firstPrepa;
    public PresentateurManager pm;


    // Use this for initialization
    void Start () {
        firstPrepa = true;
        pm.gameObject.SetActive(false);
        multiplayerManager = GetComponent<MultiplayerManager>();
        if (!multiplayerManager)
            Debug.LogWarning("Pas de MultiplayerManager sur le game object");
        
        voteManager = GameObject.FindGameObjectWithTag("VoteManager").GetComponent<VoteManager>();
        effectManager = GameObject.FindGameObjectWithTag("EffectManager").GetComponent<EffectManager>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();

        EnterPreparation();
        AudioManager.Instance.PlaySound(BackgroundMusic, Vector3.zero, true);



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

            effectManager.DisplayEffects();
            if (timerChrono <= (Time.time - time))
            {
                time = Time.time;
                etat = EtatGame.vote;
                voteManager.startVote();
            }
            TVManager.tvDisplay.DisplayTimer(timerChrono, (Time.time - time));
        }
        if (etat.Equals(EtatGame.vote))
        {
            pm.gameObject.SetActive(true);
            if (timerVote <= (Time.time - time))
            {
                time = Time.time;
                effectManager.EndEffects();               
                StopVote();
                effectManager.BeginEffects();
                etat = EtatGame.bataille;
                //multiCHance++;
                //calculChanceMap();
            }
            TVManager.tvDisplay.DisplayTimer(timerVote, (Time.time - time));
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

       
        if (firstPrepa)
        {
            //TODO : dialogue init
        }
        else
        {
            mapManager.InitMap();
        }

        firstPrepa = false;


        TVManager.tvDisplay.DisplayPreparation();

    }

    // Sort de l'état de préparation
    public void StartRound()
    {
        multiplayerManager.DestroyAllIcons();

        pm.gameObject.SetActive(false);
        // Suppression du buzzer
        Destroy(buzzerInstance);

        if (firstPrepa)
            AudioManager.Instance.PlaySound(BackgroundMusic, Vector3.zero, true);
        multiplayerManager.InitializePlayers();
        time = Time.time;
        etat = EtatGame.bataille;
    }

    // Appelé lorsqu'il ne reste plus qu'un joueur en vie. Affiche le vainqueur et remet le jeu en état de préparation
    public void EndRound(string winner)
    {

        voteManager.destroyBuzzer();

        AudioManager.Instance.PlaySound(EndGame1, Vector3.zero, true);
        AudioManager.Instance.PlaySound(EndGame2, Vector3.zero, true);

        this.winner = winner;       
        TVManager.tvDisplay.DisplayWinner(winner);
        pm.gameObject.SetActive(false);
        effectManager.EndEffects();
        StartCoroutine(PostBataille());
    }

    public IEnumerator PostBataille()
    {
        etat = EtatGame.postBataille;
        yield return new WaitForSeconds(5);
        pm.gameObject.SetActive(false);
        EnterPreparation();
    }

    private void StopVote()
    {
        PresentateurManager.PManager.hideText();
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
        PresentateurManager.PManager.setStatementText(reponse);
    }
}

public enum EtatGame
{
    preparation,
    vote,
    bataille,
    postBataille
}
