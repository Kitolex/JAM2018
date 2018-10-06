using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour {

    private int numberPlayer;                       // Nombre de joueur actuellement dans la partie
    private List<PersonnageBehaviour> playerList;   // Liste des références vers les script de personnage

    public int[] Scores;                            // Liste indiquant le nombre de manche remportées par chaque joueurs
    public GameObject[] prefabList;                 // Liste des prefab servant à instancier les personnages   
    public GameObject[] SpawnPoints;                // Liste des point de spawn où les personnages seront créés

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
        for (int i = 1; i <= 4; i++)
            if (Input.GetAxisRaw("Player" + i + "Start") != 0)
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

        // Instanciation du personnage si le prefab existe
        if (prefabList[numPlayer - 1])
        {
            instanciated = Instantiate(prefabList[numPlayer - 1]);
            behaviour = instanciated.GetComponent<PersonnageBehaviour>();

            // Gestion de la position de spawn
            if (SpawnPoints[numPlayer - 1])
                instanciated.transform.position = SpawnPoints[numPlayer - 1].transform.position;
            else
            {
                instanciated.transform.position = Vector3.zero;
                Debug.LogWarning("Pas de point de spawn pour le joueur " + numPlayer);
            }          

            // Récupération du script de behaviours
            if (behaviour)
                playerList.Add(behaviour);
            behaviour.setPlayerID("Player" + numPlayer);

            numberPlayer++;
        }
    }

    public void DestroyPlayer(int numPlayer)
    {
        // Si le joueur existe
        foreach (PersonnageBehaviour p in playerList)
            if (p.getPlayerID() == "Player" + numPlayer)
            {
                playerList.Remove(p);
                Destroy(p.gameObject);
                numPlayer--;
                return;
            }

        Debug.LogWarning("Pas de joueur à détruire");
    }

    public void CheckMultiplayerEnding()
    {
        int stillAlive = 0;
        string winnerID = "";
        foreach(PersonnageBehaviour p in playerList)
        {
            if (p.IsAlive())
            {
                stillAlive++;
                winnerID = p.getPlayerID();
            }
        }

        if(stillAlive <= 1)
        {
            // fin de round
        }
    }


    public List<PersonnageBehaviour> GetAllPersonnages()
    {
        return playerList;        
    }
}
