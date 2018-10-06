﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInverserCommandes : Effect
{
    public override void Begin() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

			pb.commandeInversees = true;
		}
    }

    public override void Display() {
        
    }

    public override void End() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

			pb.commandeInversees = false;
		}
    }
}
