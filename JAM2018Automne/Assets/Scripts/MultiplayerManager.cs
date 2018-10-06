using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour {

    public int numberPlayer;
    public int[] Scores;
    public GameObject[] prefabList;
    public List<PersonnageBehaviour> playerList;
    public Vector3[] SpawnPoints;

    public void Start()
    {
        playerList = new List<PersonnageBehaviour>();
        numberPlayer = 0;
    }

    public void Update()
    {
        HandleInput(); 
    }


    /// <summary>
    ///  Vérifie si le bouton start a été appuyé par un des joueurs
    ///  Si le joueur en question n'a pas encore rejoint la partie, instancie un personnage jouable et lui assigne les controlles approprié
    /// </summary>
    public void HandleInput()
    {
        //Récupération des inputs
        for(int i = 1; i <= 4; i++)
            if (Input.GetAxisRaw("Player" + i + "Start") !=0 )
                InstanciatePlayer(i);                    
    }

    public void InstanciatePlayer(int numPlayer)
    {
        GameObject instanciated;
        PersonnageBehaviour behaviour;

        // Si le joueur existe déjà
        foreach (PersonnageBehaviour p in playerList)
            if (p.getPlayerID() == "Player" + numPlayer)
                return;

        //si le numéro est hors range 
        if (numPlayer > 4)
            return;


        if (prefabList[numPlayer-1])
        {
            instanciated = Instantiate(prefabList[numPlayer - 1]);
            instanciated.transform.position = SpawnPoints[numPlayer - 1];
            behaviour = instanciated.GetComponent<PersonnageBehaviour>();
            if (behaviour)
                playerList.Add(behaviour);
            behaviour.setPlayerID("Player" + numPlayer);
        }           
    }
}
