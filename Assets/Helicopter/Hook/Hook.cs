using UnityEngine;

public class Hook : MonoBehaviour
{
	HookableObject hookedObject;
	PlayerHelicopter playerHelicopter;
	[SerializeField] float reHookDelay = 0.5f;
	[SerializeField] float defaultMass = 1f;
	[SerializeField] float distanceFromHeliToDisconect = 1f;
	float reHookDelayTimer = 0f;


	private void Start()
	{
		playerHelicopter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelicopter>();

	}

	private void Update()
	{
		if(hookedObject)
		{
			float distance = Vector2.Distance(playerHelicopter.transform.position, transform.position);
			if(distance < distanceFromHeliToDisconect)
			{
				DetatchHookedObject();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!hookedObject)
		{
			HookableObject hookableObject = collision.gameObject.GetComponent<HookableObject>();
			if (hookableObject && !hookableObject.IsHooked && reHookDelayTimer < Time.time && hookableObject.IsActivated)
			{
				AttatchHookedObject(hookableObject);
			}
		}
	}
	private void AttatchHookedObject(HookableObject hookableObject)
	{
		hookedObject = hookableObject;
		hookableObject.GotHooked(this);
		SetWeight(hookableObject.mass);
	}

	public HookableObject DetatchHookedObject()
	{
		HookableObject lastHookedObjected = hookedObject;
		reHookDelayTimer = Time.time + reHookDelay;
		hookedObject.UnHooked();
		hookedObject = null;

		SetWeight(defaultMass);
		return lastHookedObjected;
	}

	private void SetWeight(float mass)
	{
		Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();
		//This seems to make it the most realistic
		myRigidBody.drag = mass;
		myRigidBody.gravityScale = mass;
	}

}
