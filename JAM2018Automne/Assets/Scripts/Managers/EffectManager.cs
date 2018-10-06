using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    private List<Effect> listEffectEnCours;

    // Use this for initialization
    void Start () {
        listEffectEnCours = new List<Effect>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createliste(List<ListEffet> listEffets)
    {
        foreach(ListEffet e in listEffets)
        {
            addList(e);
        }
    }

    private void addList(ListEffet e)
    {
        //TODO
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
    }
    public void BeginEffects()
    {
        foreach (Effect e in listEffectEnCours)
        {
            e.Begin();
        }
    }
}
