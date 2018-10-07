using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cercle : MonoBehaviour {


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void tomber()
    {
        Debug.Log(gameObject.name + " TOMBE");
        //TODO : lancer anim
    }
    public void repop()
    {
        //TODO : repop
    }
    public float getDiametre()
    {
        return gameObject.transform.localScale.z;
    }

}
