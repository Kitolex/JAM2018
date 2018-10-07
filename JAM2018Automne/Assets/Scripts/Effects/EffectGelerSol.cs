using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGelerSol : Effect
{
    public override void Begin() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

            pb.solGlace = true;
		}
        changeDecorsMaterials(); // TODO : Pas appellée
    }

    public override void Display() {
        
    }

    public override void End() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

			pb.solGlace = false;
		}
    }

    private void changeDecorsMaterials()
    {
        GameObject[] materials = GameObject.FindGameObjectsWithTag("material");
        Material iceMaterial = null;
        foreach (GameObject go in materials)
        {
            if (go.GetComponent<Renderer>().material.name.Equals("LowPoly_Ice1"))
            {
                iceMaterial = go.GetComponent<Renderer>().material;
            }
        }
        if (!iceMaterial)
        {
            return;
        }
        GameObject decors = GameObject.FindGameObjectWithTag("mutableMaterials");
        for( int i=0; i< decors.transform.childCount; i++)
        {
            Renderer r = decors.transform.GetChild(i).GetComponent<Renderer>();
            if (r)
            {
                r.sharedMaterial = iceMaterial;
            }
        }
    }
    
    private void changeMapMaterials()
    {

    }
}
