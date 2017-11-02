using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class HookableObject : MonoBehaviour {
	protected bool isHooked = false;
	protected Rigidbody2D myRigidBody;
	HingeJoint2D myHingeJount;
	SpriteRenderer mySpriteRenderer;

	protected bool isActivated = true;
	[SerializeField] Sprite activeSprite;
	[SerializeField] Sprite unActiveSprite;
	public float mass;

	Hook myHook = null;
	protected bool collidable = false;

	public virtual void Initialize()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myHingeJount = GetComponent<HingeJoint2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		isHooked = false;

	}

	public virtual void GotHooked(Hook hook)
	{
		isHooked = true;
		collidable = true;
		myHook = hook;

		Rigidbody2D hookRigidBody = hook.GetComponent<Rigidbody2D> ();

		myHingeJount.enabled = true;
		myHingeJount.connectedBody = hookRigidBody;

		transform.position = hook.transform.position;


	}

	public virtual void UnHooked()
	{
		myHingeJount.connectedBody = null;
		myHingeJount.enabled = false;
		isHooked = false;
		myHook = null;
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
	}
	public bool IsActivated
	{
		get { return isActivated; }
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collidable)
		{
			HookableObject hookableObject = collision.gameObject.GetComponent<HookableObject>();
			if (hookableObject)
			{
				if(myHook)
				{
					myHook.DetatchHookedObject();
				}
				//if force > needed force
				Destroy(hookableObject.gameObject);
				Destroy(gameObject);
				//TODO apply force to objects
			}
		}
	}


		}
