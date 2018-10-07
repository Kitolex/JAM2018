using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerVote : MonoBehaviour , IDashable{
    public AudioClip buzzerSound;

    public int smash;
    public string nomProposition;
    public ListEffet consequence;
    public float rayon;
    public int angle;

    private Animator anim;

    // Use this for initialization
    void Start () {
        smash = 0;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pop()
    {
        transform.position = calculXYZ();
        gameObject.SetActive(true);
    }

    private Vector3 calculXYZ()
    {
        Vector3 res = Vector3.zero;

        res.y = transform.position.y;
        res.x = rayon * Mathf.Cos(angle);
        res.z = rayon * Mathf.Sin(angle);

        return res;
    }

    public void subirDash(GameObject dasher)
    {
        AudioManager.Instance.PlaySound(buzzerSound, Vector3.zero);

        PersonnageBehaviour personnage = dasher.GetComponent<PersonnageBehaviour>();

        Vector3 impact = (personnage.transform.position - this.transform.position);

        impact.y = 0.0f;

        impact = impact.normalized * personnage.dashImpactForce;

        personnage.rb.velocity = Vector3.zero;
        personnage.rb.ResetInertiaTensor();

        personnage.rb.AddForce(impact, ForceMode.Impulse);

        anim.SetTrigger("Push");
        smash++;
    }

}
