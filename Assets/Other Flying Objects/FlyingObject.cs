using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class FlyingObject : MonoBehaviour {
	bool isHooked = false;
	Rigidbody2D myRigidBody;


	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myRigidBody.Sleep();
	}
	
	void Update () {
		
	}

	public virtual void GotHooked()
	{
		isHooked = true;
		myRigidBody.WakeUp();
	}

	public virtual void UnHooked()
	{
		isHooked = false;
		myRigidBody.Sleep();
	}
}
