using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class VoteManager : MonoBehaviour {

    public List<Vote> listVote;
    public Vote voteActuel;

    private Dictionary<string, int> countSmash;
    private Dictionary<string, List<ListEffet>> consequence;

    private const string intro = "Passons maintenant aux votes.";

    // Use this for initialization
    void Start () {
        countSmash = new Dictionary<string, int>();
        consequence = new Dictionary<string, List<ListEffet>>();

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
            countSmash.Add(voteActuel.nomProposition[i],0);
            consequence.Add(voteActuel.nomProposition[i], voteActuel.listEffects);
        }

    }

    public void smash(string prop)
    {
        countSmash[prop] ++;
    }

    public List<ListEffet> getEffect()
    {
        int max = 0;
        string propMax = "";
        foreach (KeyValuePair<string,int> entry in countSmash)
        {
            if (entry.Value>max)
            {
                max = entry.Value;
                propMax = entry.Key;
            }
        }

        return consequence[propMax];

    }


    private Vote searchVote()
    {
        Random rnd = new Random();
        int value = rnd.Next(0, listVote.Count);
        return listVote[value];
    }
}
