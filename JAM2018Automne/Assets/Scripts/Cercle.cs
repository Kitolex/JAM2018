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
        GetComponent<Animator>().SetTrigger("drop");
        GameObject.FindGameObjectWithTag("ZoneJeu").GetComponent<Animator>().SetTrigger("drop");
    }
    public void repop()
    {
        GetComponent<Animator>().SetTrigger("reset");
        GameObject.FindGameObjectWithTag("ZoneJeu").GetComponent<Animator>().SetTrigger("reset");
    }
    public float getDiametre()
    {
        return gameObject.transform.localScale.z;
    }

}
