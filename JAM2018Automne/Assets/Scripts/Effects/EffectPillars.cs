using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPillars : Effect
{

    private int nbObjectToAdd = 4;
    private GameObject[] objects;

    public override void Begin() {

        objects = new GameObject[nbObjectToAdd];

        int nbPrefabs = effectManager.pillars.Length;

        for(int i=0; i<nbObjectToAdd; i++){
            Transform obj = effectManager.pillars[Random.Range(0, nbPrefabs)];
            objects[i] = Instantiate(obj, Vector3.zero, obj.rotation).gameObject;
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
