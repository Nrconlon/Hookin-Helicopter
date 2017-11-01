using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemy {
	public Vector2 flyDirection;
	public float missileSpeed = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (flyDirection != Vector2.zero && !isHooked && isActivated)
		{
			transform.position = transform.position + (Vector3)(flyDirection * missileSpeed * Time.deltaTime);
		}

		if(isActivated)
		{
			transform.rotation = new Quaternion(flyDirection.x, flyDirection.y, 0, 0);
			float angle = Vector2.Angle(Vector2.zero, flyDirection);

			transform.rotation = Quaternion.FromToRotation(Vector3.left, flyDirection);
		}	

	}

	public override void DeActivate()
	{
		base.DeActivate();
		myRigidBody.gravityScale = 0;
	}

}
