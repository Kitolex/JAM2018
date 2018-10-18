using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnStart : MonoBehaviour {

	public Transform poussiereImpactSol;
	private bool effectDone;

	// Use this for initialization
	void Start () {
		effectDone = false;
		this.GetComponent<Rigidbody>().velocity = Vector3.up * -60.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Rigidbody>().velocity.y >= 0)
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }

	void OnCollisionEnter(Collision collision) {
		
		if(!effectDone){
			effectDone = true;

			Vector3 posi = this.transform.position;
			posi.y = 0.4f;
			Instantiate(poussiereImpactSol, posi, poussiereImpactSol.rotation);
		}
		
		
	}
}
