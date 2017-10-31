using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HookableObject {
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


	public override void GotHooked(Rigidbody2D hook)
	{
		DeActivate();
		base.GotHooked(hook);
	}

	public override void UnHooked()
	{
		base.UnHooked();
	}
}
