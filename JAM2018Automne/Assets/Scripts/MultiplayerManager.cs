using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour {

    public int numberPlayer;
    public int[] Scores;
    public GameObject[] playerList;
    public Vector3[] SpawnPoints;

    public void InstanciatePlayer(int numPlayer)
    {
        GameObject instanciated;

        //si le joueur n'existe pas 
        if (numPlayer > numberPlayer)
            return;

        if (playerList[numPlayer])
        {
            instanciated = Instantiate(playerList[numPlayer]);
            instanciated.transform.position = SpawnPoints[numPlayer];
        }           
    }
}
