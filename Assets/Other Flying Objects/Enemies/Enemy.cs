using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FlyingObject {
	protected PlayerHelicopter playerHelicopter;

	public override void Initialize()
	{
		playerHelicopter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelicopter>();
		base.Initialize();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
