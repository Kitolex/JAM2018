using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class VoteManager : MonoBehaviour {

    public List<Vote> listVote;
    public Vote voteActuel;
    public BuzzerVote buzzer;

   // private Dictionary<string, int> countSmash;
   // private Dictionary<string, List<ListEffet>> consequence;
    private List<BuzzerVote> listBuzzer;

    private const string intro = "Passons maintenant aux votes.";

    // Use this for initialization
    void Start () {
        listBuzzer = new List<BuzzerVote>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startVote()
    {
        voteActuel = searchVote();
        string enonce = intro+ voteActuel.enonce;
        //TODO afficher enoncer
        //TODO afficher prop
        for(int i = 0; i <voteActuel.nomProposition.Count;i++)
        {
            BuzzerVote buzzerVoteTemp = Instantiate(buzzer);            
            buzzerVoteTemp.consequence = voteActuel.listEffects;
            buzzerVoteTemp.rayon = calculRayonPopBuzzer(52.0f);//ALLER CHERCHER taille cercle
            Random rnd = new Random();
            buzzerVoteTemp.angle = rnd.Next(0, 360);
            listBuzzer.Add(buzzerVoteTemp);
        }
        foreach (BuzzerVote b in listBuzzer)
        {
            b.pop();
        }


    }

    public float calculRayonPopBuzzer(float rayon)
    {
        Random rnd = new Random();
        double value = rnd.NextDouble();
        float newRayon = (float) ((rayon - 0.0f) * value); //TODO rajouter taille buzzer
        return newRayon;    
    }



    public List<ListEffet> getEffect()
    {
        int max = 0;
        BuzzerVote buzzerWin = null;
        foreach (BuzzerVote b in listBuzzer)
        {
            if (b.smash>max)
            {
                max = b.smash;
                buzzerWin = b;
            }
        }
        if (buzzerWin == null)
        {
            return null;
        }
        return buzzerWin.consequence;

    }


    private Vote searchVote()
    {
        Random rnd = new Random();
        int value = rnd.Next(0, listVote.Count);
        return listVote[value];
    }
}
