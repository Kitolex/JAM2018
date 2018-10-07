using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public List<Effect> listEffectEnCours;

    [Header("Effects Prefabs")]

    public Transform[] pillars;
    public Transform[] walls;
    public Transform bumper;

    // Use this for initialization
    void Start () {
        listEffectEnCours = new List<Effect>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createliste(ListEffet listEffets)
    {
        foreach(Effects e in listEffets.effects)
        {

                addList(e);

        }
    }

    private void addList(Effects e)
    {
        switch (e)
        {
            case Effects.CHALEUR_INTENSE:
                listEffectEnCours.Add(new EffectChaleurIntense());
                break;
            case Effects.GELER_SOL:
                listEffectEnCours.Add(new EffectGelerSol());
                break;
            case Effects.INVERSER_COMMANDES:
                listEffectEnCours.Add(new EffectInverserCommandes());
                break;
            case Effects.TOURNER_COMMANDES:
                listEffectEnCours.Add(new EffectTournerCommandes());
                break;
            case Effects.EJECTIONS_RENFORCEES:
                listEffectEnCours.Add(new EffectEjectionsRenforcees());
                break;
            case Effects.DIRECTION_DASH_ALEATOIRE:
                listEffectEnCours.Add(new EffectDirectionDashAleatoire());
                break;

        }
    }

    public void DisplayEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.Display();
        }
    }
    public void EndEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.End();
        }
        listEffectEnCours = new List<Effect>();

    }
    public void BeginEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.Begin();
        }
    }
}
