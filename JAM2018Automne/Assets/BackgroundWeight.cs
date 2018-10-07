using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundWeight : MonoBehaviour {

    public float animWeight = 0.5f;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim.SetLayerWeight(10, animWeight);
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetLayerWeight(anim.GetLayerIndex("BG"), animWeight);
    }
}
