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

    private int angle;
    private float rayon;

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
        if (mapManager.nb<=1)
        {
            BuzzerVote b = new BuzzerVote();
            b.consequence = voteActuel.listEffects[rnd.Next(0, voteActuel.nomProposition.Count)];
            listBuzzer.Add(b);
        }
        else
        {
            for (int i = 0; i < voteActuel.nomProposition.Count; i++)
            {
                GameObject go = Instantiate(buzzer);
                go.name = "BuzzerVote" + i;
                createBuzzer();
                go.AddComponent<BuzzerVote>();

        PresentateurManager.PManager.setStatementText(enonce);
          
            PresentateurManager.PManager.setPropText(voteActuel.nomProposition[i], i);
          
            go.GetComponent<BuzzerVote>().nomProposition = voteActuel.nomProposition[i];

                go.GetComponent<BuzzerVote>().consequence = voteActuel.listEffects[i];
                go.GetComponent<BuzzerVote>().angle = angle;
                go.GetComponent<BuzzerVote>().rayon = rayon;
                listBuzzer.Add(go.GetComponent<BuzzerVote>());

            }
            foreach (BuzzerVote b in listBuzzer)
            {
                b.pop();
            }
        }
           

    }

    public void createBuzzer()
    {
        rayon = calculRayonPopBuzzer(mapManager.getDiametreMap() / 2);
        angle = rnd.Next(0, 360);
        

        if (!checkRandom(angle, rayon))
        {
            createBuzzer();
        }
    }

    public bool checkRandom(int angle,float rayon)
    {
        foreach (BuzzerVote b in listBuzzer)
        {
            if (rayon < (b.rayon*1.15) &&
                rayon > (b.rayon * 0.85) &&
                angle < (b.angle * 1.15) &&
                angle > (b.angle * 0.85)
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
        else
        {
            TVManager.tvDisplay.DisplayVoteResult(buzzerWin.nomProposition);
        }


        
        return buzzerWin.consequence;
    }



    private Vote searchVote()
    {
        int value = rnd.Next(0, listVote.Count);
        return listVote[value];
    }
}
