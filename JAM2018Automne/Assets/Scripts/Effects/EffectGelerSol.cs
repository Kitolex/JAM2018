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
        changeMapMaterials("LowPoly_Ice1");
        
    }

    public override void Display() {
        
    }

    public override void End() {
        foreach(PersonnageBehaviour pb in this.multiplayerManager.GetAllPersonnages()){

			pb.solGlace = false;
		}
        changeDecorsMaterials("LowPoly_Rock");
        changeMapMaterials("LowPoly_grass1");
    }




    private void changeDecorsMaterials(string materialName)
    {
        Material iceMaterial = getMaterial(materialName);
        GameObject decors = GameObject.FindGameObjectWithTag("mutableMaterials");
        for (int i = 0; i < decors.transform.childCount; i++)
        {
            Renderer[] renderers = decors.transform.GetChild(i).GetComponents<Renderer>();
            if (renderers.Length > 0)
            {
                foreach (Renderer r in renderers)
                {
                    Material[] mats = r.materials;
                    Material[] sharedMats = r.sharedMaterials;
                    for (int j = 0; j < mats.Length; j++)
                    {
                        mats[j] = iceMaterial;
                        sharedMats[j] = iceMaterial;
                    }
                    r.materials = mats;
                    r.sharedMaterials = sharedMats;
                }
            }
        }
    }

    private void changeMapMaterials(string materialName)
    {
        Material iceMaterial = getMaterial(materialName);
        GameObject[] cerles = GameObject.FindGameObjectsWithTag("cercle");
        foreach (GameObject go in cerles)
        {
            go.GetComponent<Renderer>().material = iceMaterial;
            go.GetComponent<Renderer>().sharedMaterial = iceMaterial;
        }

    }

    private Material getMaterial(string materialName)
    {
        GameObject[] materialsObject = GameObject.FindGameObjectsWithTag("material");
        Material material = null;
        foreach (GameObject go in materialsObject)
        {
            if (go.GetComponent<Renderer>().sharedMaterial.name.Equals(materialName))
            { 
                material = go.GetComponent<Renderer>().sharedMaterial;
            }
        }
        if (!material)
        {
            return null;
        }
        return material;
    }
    
    
}
