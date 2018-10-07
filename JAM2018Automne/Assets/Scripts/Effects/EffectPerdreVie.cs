using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPerdreVie : Effect
{
    public override void Begin() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

            pb.stun(0.8f);
			pb.Kill();
            if (pb.IsDead()){
                pb.gameObject.SetActive(false);
            }
		}
    }

    public override void Display() {
        
    }

    public override void End() {
        
    }
}
