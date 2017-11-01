using UnityEngine;

public class Hook : MonoBehaviour
{
	public Rope rope;
	HookableObject hookedObject;
	[SerializeField] float reHookDelay = 0.5f;
	float reHookDelayTimer = 0f;

	public void SetRope(Rope r) {
		this.rope = r;
	}

	public Rope GetRope() {
		return this.rope;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(hookedObject)
		{
			PlayerHelicopter helicopter = collision.gameObject.GetComponent<PlayerHelicopter>();
			if (helicopter)
			{
				DetatchHookedObject();
			}
			HookableObject flyingObject = collision.gameObject.GetComponent<HookableObject>();
			if (flyingObject && flyingObject != hookedObject)
			{
				//if force > needed force
				DetatchHookedObject();
				flyingObject.DeActivate();
				//TODO apply force to objects
			}
		}
		else
		{
			HookableObject flyingObject = collision.gameObject.GetComponent<HookableObject>();
			if (flyingObject && !flyingObject.IsHooked && reHookDelayTimer < Time.time && flyingObject.IsActivated)
			{
				hookedObject = flyingObject;
				flyingObject.GotHooked(this);
			}
		}
	}

	private void DetatchHookedObject()
	{
		reHookDelayTimer = Time.time + reHookDelay;
		hookedObject.UnHooked();
		hookedObject = null;
	}

}
