using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class HookableObject : MonoBehaviour {
	protected bool isHooked = false;
	protected Rigidbody2D myRigidBody;
	FixedJoint2D myFixedJoint;
	DistanceJoint2D myDistanceJoint;
	BoxCollider2D myBoxCollider;
	SpriteRenderer mySpriteRenderer;

	Hook myHook;

	protected bool isActivated = true;
	[SerializeField] Sprite activeSprite;
	[SerializeField] Sprite unActiveSprite;

	public virtual void Initialize()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myFixedJoint = GetComponent<FixedJoint2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myDistanceJoint = GetComponent<DistanceJoint2D> ();
		myBoxCollider = GetComponent<BoxCollider2D> ();
		isHooked = false;

		//UnHooked();
	}

	public virtual void GotHooked(Hook hook)
	{
		myHook = hook;
		isHooked = true;

		Rigidbody2D hookRB = hook.GetComponent<Rigidbody2D> ();

		myFixedJoint.enabled = true;
		myFixedJoint.connectedBody = hookRB;
		myFixedJoint.autoConfigureConnectedAnchor = false;

		//DIRTY HACKS, DIRTY HACKS Because the positions are set to the same, you can say that the anchor and connector anchor ae both at (0, 0)
		this.transform.position = hook.gameObject.transform.position;
		myFixedJoint.autoConfigureConnectedAnchor = false;
		myFixedJoint.anchor = Vector2.zero;
		myFixedJoint.connectedAnchor = Vector2.zero;


		myDistanceJoint.enabled = true;
		myDistanceJoint.connectedBody = hook.GetRope().helicopterRB;
		myDistanceJoint.connectedAnchor = hook.GetRope().helicopterRopeAnchorPoint.localPosition;	//.anchor is where the anchor is on the link, connectedAnchor is where the anchor is on the helicopter RELATIVE TO THE HELICOPTER
		myDistanceJoint.autoConfigureDistance = false;
		myDistanceJoint.distance = hook.GetRope().ropeLength;	//BECAUSE THE POSITIONS OF THE HOOK ARE THE SAME. DIRTY HACKS DIRTY HACKS
		myDistanceJoint.maxDistanceOnly = true;


		//myRigidBody.bodyType = RigidbodyType2D.Kinematic;

		//hookRB.mass += this.myRigidBody.mass;

	}

	public virtual void UnHooked()
	{
		myFixedJoint.connectedBody = null;
		myFixedJoint.enabled = false;

		myDistanceJoint.connectedBody = null;
		myDistanceJoint.enabled = false;

		myHook.gameObject.GetComponent<Rigidbody2D>().mass -= this.myRigidBody.mass;

		myHook = null;
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
