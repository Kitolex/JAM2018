using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class VoteManager : MonoBehaviour {

    public List<Vote> listVote;
    public Vote voteActuel;
    public GameObject buzzer;
    public GameObject cylinder;
    private Random rnd;

   // private Dictionary<string, int> countSmash;
   // private Dictionary<string, List<ListEffet>> consequence;
    public List<BuzzerVote> listBuzzer;

    private const string intro = "Passons maintenant aux votes.";

    // Use this for initialization
    void Start () {
        rnd = new Random();
        listBuzzer = new List<BuzzerVote>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void destroyBuzzer()
    {
        foreach (BuzzerVote b in listBuzzer)
        {
            Destroy(b.gameObject);
        }
        listBuzzer = new List<BuzzerVote>();
    }

    public void startVote()
    {

        Debug.Log("startVote");
        voteActuel = searchVote();
        string enonce = intro+ voteActuel.enonce;
        //TODO afficher enoncer
        //TODO afficher prop

            for (int i = 0; i < voteActuel.nomProposition.Count; i++)
            {
                GameObject go = Instantiate(buzzer);
                go.name = voteActuel.nomProposition[i] + "";
                go.AddComponent<BuzzerVote>();
                go.GetComponent<BuzzerVote>().consequence = voteActuel.listEffects[i];
                go.GetComponent<BuzzerVote>().rayon = calculRayonPopBuzzer((cylinder.transform.localScale.z) / 2);//TODO ALLER CHERCHER taille cercle

                int angle = rnd.Next(0, 360);
                Debug.Log(angle);
                go.GetComponent<BuzzerVote>().angle = angle;
                listBuzzer.Add(go.GetComponent<BuzzerVote>());
            }
            foreach (BuzzerVote b in listBuzzer)
            {
                b.pop();
            }

    }

    public float calculRayonPopBuzzer(float rayon)
    {
        double value = rnd.NextDouble();
        float newRayon = (float) ((rayon *0.85) * value); //TODO rajouter taille buzzer
        return newRayon;
        
    }



    public ListEffet getEffect()
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

        return buzzerWin.consequence;


    }



    private Vote searchVote()
    {
        int value = rnd.Next(0, listVote.Count);
        return listVote[value];
    }
}
