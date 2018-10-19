using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnStart : MonoBehaviour {

	public Transform poussiereImpactSol;
    public float Height = 10;
    public float FallSpeed = 5;
	private bool effectDone;

	// Use this for initialization
	void Start () {
		effectDone = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > Height)
            transform.position -= Vector3.down * FallSpeed * Time.deltaTime;
        else
        {
            if (!effectDone)
            {
                effectDone = true;

                Vector3 posi = this.transform.position;
                posi.y = 0.4f;
                Instantiate(poussiereImpactSol, posi, poussiereImpactSol.rotation);
            }
        }

    }
}
