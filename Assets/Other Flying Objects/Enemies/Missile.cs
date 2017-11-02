using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemy {
	public Vector2 flyDirection;
	public float missileSpeed = 3f;
	bool launchingMissile = false;
	public Enemy myInstigator;

	float missilePrepareTime = 2f;
	float missileDownPrepareSpeed = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (launchingMissile)
		{
			transform.position = transform.position + Vector3.down * missileDownPrepareSpeed * Time.deltaTime;
		}

		if (flyDirection != Vector2.zero && !isHooked && isActivated)
		{
			transform.position = transform.position + (Vector3)(flyDirection * missileSpeed * Time.deltaTime);
			transform.rotation = Quaternion.FromToRotation(Vector3.left, flyDirection);
		}	

	}

	public override void DeActivate()
	{
		base.DeActivate();
	}

	public void TurnOnLaunching()
	{
		DeActivate();
		launchingMissile = true;
		StartCoroutine(ActivateMissile());
	}


	IEnumerator ActivateMissile()
	{
		yield return new WaitForSeconds(missilePrepareTime);
		launchingMissile = false;
		flyDirection = (playerHelicopter.transform.position - transform.position).normalized;
		Activate();
	}

}
