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
    private MapManager mapManager;

    // private Dictionary<string, int> countSmash;
    // private Dictionary<string, List<ListEffet>> consequence;
    public List<BuzzerVote> listBuzzer;

    private const string intro = "Passons maintenant aux votes.";

    // Use this for initialization
    void Start () {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
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
                go.name = "BuzzerVote" + i;
                createBuzzer(go,i);
                
                
            }
            foreach (BuzzerVote b in listBuzzer)
            {
                b.pop();
            }

    }

    public void createBuzzer(GameObject go,int i)
    {
        go.AddComponent<BuzzerVote>();
        go.GetComponent<BuzzerVote>().consequence = voteActuel.listEffects[i];
        go.GetComponent<BuzzerVote>().rayon = calculRayonPopBuzzer(mapManager.getDiametreMap() / 2);//TODO ALLER CHERCHER taille cercle
        int angle = rnd.Next(0, 360);
        Debug.Log(angle);
        go.GetComponent<BuzzerVote>().angle = angle;

        if (checkRandom(go))
        {
            listBuzzer.Add(go.GetComponent<BuzzerVote>());
        }
        else
        {
            createBuzzer(buzzer, i);
        }
    }

    public bool checkRandom(GameObject go)
    {
        foreach (BuzzerVote b in listBuzzer)
        {
            if (go.GetComponent<BuzzerVote>().rayon < (b.rayon*1.15) &&
                go.GetComponent<BuzzerVote>().rayon > (b.rayon * 0.85) &&
                go.GetComponent<BuzzerVote>().angle < (b.angle * 1.15) &&
                go.GetComponent<BuzzerVote>().angle > (b.angle * 0.85)
                )
            {
                return false;
            }
            
        }

         return true;
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
        if (buzzerWin==null)
        {
            return new ListEffet();
        }
        
        return buzzerWin.consequence;


    }



    private Vote searchVote()
    {
        int value = rnd.Next(0, listVote.Count);
        return listVote[value];
    }
}
