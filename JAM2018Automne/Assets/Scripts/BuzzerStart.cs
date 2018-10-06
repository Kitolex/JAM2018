using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerStart : MonoBehaviour {

    [HideInInspector]
    public MultiplayerManager multiplayerManager;

    private List<PersonnageBehaviour> listPlayer;   // Liste des joueurs ayant appuyé sur le buzzer

	void Start () {
        listPlayer = new List<PersonnageBehaviour>();

    }
}
