﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour {

    protected MultiplayerManager multiplayerManager;

    void Awake() {
        this.multiplayerManager = GameObject.FindGameObjectWithTag("MultiplayerManager").GetComponent<MultiplayerManager>();
    }

    public abstract void Begin();

    public abstract void Display();

    public abstract void End();


}

public enum Effects
{
    GELER_SOL,
    CHALEUR_INTENSE,
    INVERSER_COMMANDES,
    TOURNER_COMMANDES
}
