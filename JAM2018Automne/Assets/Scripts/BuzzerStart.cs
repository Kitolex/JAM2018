using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerStart : MonoBehaviour, IDashable {

    [HideInInspector]
    public MultiplayerManager multiplayerManager;
    [HideInInspector]
    public GameManager gameManager;

    private List<PersonnageBehaviour> listPlayer;   // Liste des joueurs ayant appuyé sur le buzzer

    public void subirDash(GameObject dasher)
    {
        PersonnageBehaviour personnage = dasher.GetComponent<PersonnageBehaviour>();

        if (listPlayer.Contains(personnage))
            return;

        listPlayer.Add(dasher.GetComponent<PersonnageBehaviour>());
        Debug.Log(listPlayer.Count);
        if (listPlayer.Count == multiplayerManager.GetAllPersonnages().Count)
        {
           gameManager.StartRound();
        }
            
    }

    void Start () {
        listPlayer = new List<PersonnageBehaviour>();
    }
}
