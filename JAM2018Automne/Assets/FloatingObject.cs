using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour {

    public float FloatingDistance = 2;
    public float FloatingSpeed = 1;


    private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = initialPosition + Vector3.up * Mathf.Sin(Time.time * FloatingSpeed) * FloatingDistance;
	}
}
