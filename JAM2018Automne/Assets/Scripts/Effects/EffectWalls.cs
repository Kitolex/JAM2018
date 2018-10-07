using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectWalls : Effect
{

    private int nbObjectToAdd = 2;
    private GameObject[] objects;

    public override void Begin() {

        objects = new GameObject[nbObjectToAdd];

        int nbPrefabs = effectManager.walls.Length;

        float maxRayon = mapManager.getDiametreMap() * 0.5f - 1.0f;

        Vector3 posi = Vector3.up * 40.0f;

        for(int i=0; i<nbObjectToAdd; i++){
            Transform obj = effectManager.walls[Random.Range(0, nbPrefabs)];
            objects[i] = Instantiate(obj, posi + Vector3.right * Random.Range(0.0f, maxRayon), obj.rotation).gameObject;
        }
    }

    public override void Display() {
        
    }

    public override void End() {
        for(int i=0; i<nbObjectToAdd; i++) {
            Destroy(objects[i]);
        }
    }
}
