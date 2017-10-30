using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy {
	float movementSpeed = 3f;
	Vector2 targetDirection = Vector2.zero;
	// Use this for initialization
	void Start () {
		base.Initialize();
	}

	private void FixedUpdate()
	{
		if(targetDirection != Vector2.zero && !isHooked && isActivated)
		{
			transform.position = transform.position + V2toV3(targetDirection * movementSpeed * 0.02f);
		}
	}


	// Update is called once per frame
	void Update () {  
		if(!isHooked)
		{
			//make sure this works
			Vector2 playerPosition = playerHelicopter.transform.position;
			Vector2 myPosition = transform.position;
			targetDirection = (playerPosition - myPosition).normalized;
		}
		else
		{
			targetDirection = Vector2.zero;
		}
	}
}
