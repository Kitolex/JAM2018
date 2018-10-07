using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGelerSol : Effect
{
    public override void Begin() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

            pb.solGlace = true;
		}
        changeDecorsMaterials("LowPoly_Ice1");
    }

    public override void Display() {
        
    }

    public override void End() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

			pb.solGlace = false;
		}
        changeDecorsMaterials("LowPoly_Rock");
    }

    private void changeDecorsMaterials(string materialName)
    {
        Material iceMaterial = getMaterial(materialName);
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

    private void changeMapMaterials(string materialName)
    {
        Material iceGroundMaterial = getMaterial(materialName);
        GameObject decors = GameObject.FindGameObjectWithTag("mutableMaterials");
        for (int i = 0; i < decors.transform.childCount; i++)
        {
            Renderer r = decors.transform.GetChild(i).GetComponent<Renderer>();
            if (r)
            {
                r.sharedMaterial = iceGroundMaterial;
            }
        }
    }

    private Material getMaterial(string materialName)
    {
        GameObject[] materials = GameObject.FindGameObjectsWithTag("material");
        Material material = null;
        foreach (GameObject go in materials)
        {
            if (go.GetComponent<Renderer>().material.name.Equals(materialName))
            {
                material = go.GetComponent<Renderer>().material;
            }
        }
        if (!material)
        {
            Debug.Log("Ground material not found");
        }
        return material;
    }
    
    
}
