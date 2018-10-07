using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour {

    protected MultiplayerManager multiplayerManager;
    protected EffectManager effectManager;

    public Effect() {
        this.multiplayerManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<MultiplayerManager>();
        this.effectManager = GameObject.FindGameObjectWithTag("EffectManager").GetComponent<EffectManager>();
    }

    public abstract void Begin();

    public abstract void Display();

    public abstract void End();


}

public enum Effects
{
    NONE,
    GELER_SOL,
    CHALEUR_INTENSE,
    INVERSER_COMMANDES,
    TOURNER_COMMANDES,
    EJECTIONS_RENFORCEES,
    DIRECTION_DASH_ALEATOIRE,
    PILLARS
}
