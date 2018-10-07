﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerVote : MonoBehaviour , IDashable{

    public int smash;
    public ListEffet consequence;
    public float rayon;
    public int angle;

    // Use this for initialization
    void Start () {
        smash = 0;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pop()
    {
        transform.position = calculXYZ();
        gameObject.SetActive(true);
    }

    private Vector3 calculXYZ()
    {
        Vector3 res = Vector3.zero;

        res.y = transform.position.y;
        res.x = rayon * Mathf.Cos(angle);
        res.z = rayon * Mathf.Sin(angle);

        return res;
    }

    public void subirDash(GameObject dasher)
    {
        Debug.Log("dd");
        smash++;
    }

}
