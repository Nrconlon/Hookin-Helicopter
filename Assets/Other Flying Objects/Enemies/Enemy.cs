using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FlyingObject {
	PlayerHelicopter playerHelicopter;
	// Use this for initialization
	void Start () {
		playerHelicopter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelicopter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
