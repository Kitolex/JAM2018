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
            Renderer[] renderers = decors.transform.GetChild(i).GetComponents<Renderer>();
            if (renderers.Length > 0)
            {
                foreach (Renderer r in renderers)
                { 
                    for (int j=0; j<r.materials.Length; j++)
                    {
                        Debug.Log("actual material : " + r.sharedMaterials[j].name);
                        Debug.Log("ice material : " + iceMaterial.name);
                        r.materials[j] = iceMaterial;
                        r.sharedMaterials[j] = iceMaterial;
                    }
                }
            }
        }
    }

    private void changeMapMaterials(string materialName)
    {
        
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
