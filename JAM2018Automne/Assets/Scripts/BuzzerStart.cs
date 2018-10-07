using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerStart : MonoBehaviour, IDashable {

    [HideInInspector]
    public MultiplayerManager multiplayerManager;
    [HideInInspector]
    public GameManager gameManager;
    public AudioClip BuzzerSound;

    private Animator anim;

    private List<PersonnageBehaviour> listPlayer;   // Liste des joueurs ayant appuyé sur le buzzer

    public void subirDash(GameObject dasher)
    {
        PersonnageBehaviour personnage = dasher.GetComponent<PersonnageBehaviour>();

        Vector3 impact = (personnage.transform.position - this.transform.position);

        impact.y = 0.0f;
        
        impact = impact.normalized * personnage.dashImpactForce;

        personnage.rb.velocity = Vector3.zero;
        personnage.rb.ResetInertiaTensor();

        personnage.rb.AddForce(impact, ForceMode.Impulse);

        anim.SetTrigger("Push");

        if (BuzzerSound)
            AudioManager.Instance.PlaySound(BuzzerSound, Vector3.zero);

        if (listPlayer.Contains(personnage))
            return;

        listPlayer.Add(dasher.GetComponent<PersonnageBehaviour>());
        if (listPlayer.Count == multiplayerManager.GetAllPersonnages().Count && multiplayerManager.GetAllPersonnages().Count >= 2)
        {
            StartCoroutine(WaitForRoundStart());           
        }

        
    }

    public IEnumerator WaitForRoundStart()
    {
        yield return new WaitForSeconds(1);
        gameManager.StartRound();
    }

    void Start () {
        listPlayer = new List<PersonnageBehaviour>();
        anim = GetComponent<Animator>();
    }
}
