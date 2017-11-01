using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HookableObject {
	protected PlayerHelicopter playerHelicopter;

	public override void Initialize()
	{
		base.Initialize();
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		playerHelicopter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelicopter>();
		Debug.Log (go.ToString ());

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}


	public override void GotHooked(Hook hook)
	{
		DeActivate();
		base.GotHooked(hook);
	}

	public override void UnHooked()
	{
		base.UnHooked();
	}
}
