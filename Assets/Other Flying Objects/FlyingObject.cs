using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class FlyingObject : MonoBehaviour {
	protected bool isHooked = false;
	Rigidbody2D myRigidBody;

	public virtual void Initialize()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		UnHooked();
	}

	public virtual void GotHooked()
	{
		isHooked = true;
		myRigidBody.WakeUp();
		myRigidBody.simulated = true;
	}

	public virtual void UnHooked()
	{
		isHooked = false;
		myRigidBody.Sleep();
		myRigidBody.simulated = false;
	}

	public Vector3 V2toV3(Vector2 v2)
	{
		return new Vector3(v2.x, v2.y, 0);
	}
}
