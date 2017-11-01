using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class HookableObject : MonoBehaviour {
	protected bool isHooked = false;
	protected Rigidbody2D myRigidBody;
	HingeJoint2D myHingeJoint;
	DistanceJoint2D myDistanceJoint;

	SpriteRenderer mySpriteRenderer;
	protected bool isActivated = true;
	[SerializeField] Sprite activeSprite;
	[SerializeField] Sprite unActiveSprite;

	public virtual void Initialize()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myHingeJoint = GetComponent<HingeJoint2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myDistanceJoint = GetComponent<DistanceJoint2D> ();
		isHooked = false;

		//UnHooked();
	}

	public virtual void GotHooked(Hook hook)
	{
		Rigidbody2D hookRB = hook.GetComponent<Rigidbody2D> ();

		myHingeJoint.enabled = true;
		myHingeJoint.connectedBody = hookRB;
		myHingeJoint.autoConfigureConnectedAnchor = true;
		myHingeJoint.anchor = hook.transform.position - this.transform.position;
		myHingeJoint.autoConfigureConnectedAnchor = false;

		myDistanceJoint.enabled = true;
		myDistanceJoint.connectedBody = hook.GetRope().helicopterRB;
		myDistanceJoint.connectedAnchor = hook.GetRope().helicopterRopeAnchorPoint.localPosition;	//.anchor is where the anchor is on the link, connectedAnchor is where the anchor is on the helicopter RELATIVE TO THE HELICOPTER
		myDistanceJoint.autoConfigureDistance = false;
		myDistanceJoint.distance = hook.GetRope().ropeLength + (hook.transform.position - this.transform.position).magnitude;
		myDistanceJoint.maxDistanceOnly = true;

		isHooked = true;
	
	}

	public virtual void UnHooked()
	{
		myHingeJoint.connectedBody = null;
		myHingeJoint.enabled = false;

		myDistanceJoint.connectedBody = null;
		myDistanceJoint.enabled = false;
		isHooked = false;
	}

	public Vector3 V2toV3(Vector2 v2)
	{
		return new Vector3(v2.x, v2.y, 0);
	}

	public bool IsHooked { get { return isHooked; } }

	public virtual void Activate()
	{
		isActivated = true;
		mySpriteRenderer.sprite = activeSprite;
	}
	public virtual void DeActivate()
	{
		isActivated = false;
		mySpriteRenderer.sprite = unActiveSprite;
		myRigidBody.gravityScale = 1;
	}
	public bool IsActivated
	{
		get { return isActivated; }
	}

}
