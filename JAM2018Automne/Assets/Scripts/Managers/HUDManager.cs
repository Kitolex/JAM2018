using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public static HUDManager Instance;

    public Image[] player1Hearts;
    public Image[] player2Hearts;
    public Image[] player3Hearts;
    public Image[] player4Hearts;
    public Image[] portraits;

    public Sprite heartEmpty;
    public Sprite heartFull;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePlayerLife(int PlayerId, int value)
    {
        switch (PlayerId)
        {
            case 1: for(int i = 0; i < player1Hearts.Length; i++) { player1Hearts[i].gameObject.SetActive(true); player1Hearts[i].sprite = i<value? heartFull:heartEmpty; } break;
            case 2: for(int i = 0; i < player1Hearts.Length; i++) { player2Hearts[i].gameObject.SetActive(true); player2Hearts[i].sprite = i<value? heartFull:heartEmpty; } break;
            case 3: for(int i = 0; i < player1Hearts.Length; i++) { player3Hearts[i].gameObject.SetActive(true); player3Hearts[i].sprite = i<value? heartFull:heartEmpty; } break;
            case 4: for(int i = 0; i < player1Hearts.Length; i++) { player4Hearts[i].gameObject.SetActive(true); player4Hearts[i].sprite = i<value? heartFull:heartEmpty; } break;           
            default: break;
        }
    }

    public void InitHud(List<PersonnageBehaviour> players)
    {
        foreach(PersonnageBehaviour p in players)
        {
            portraits[p.getPlayerID() - 1].gameObject.SetActive(true);
            portraits[p.getPlayerID() - 1].sprite = p.portrait;
            UpdatePlayerLife(p.getPlayerID(), p.VieActuelle); 
        }
    }
}
