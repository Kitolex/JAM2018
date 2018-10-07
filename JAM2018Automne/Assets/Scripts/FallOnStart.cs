using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().velocity = Vector3.up * -60.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
