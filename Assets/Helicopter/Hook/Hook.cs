using UnityEngine;

public class Hook : MonoBehaviour
{

	public float distanceFromChainEnd = 0.6f;
	FlyingObject hookedObject;
	[SerializeField] float reHookDelay = 0.5f;
	float reHookDelayTimer = 0f;

	public void ConnectRopeEnd(Rigidbody2D endRB)
	{
		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = endRB;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(hookedObject)
		{
			PlayerHelicopter helicopter = collision.gameObject.GetComponent<PlayerHelicopter>();
			if (helicopter)
			{
				reHookDelayTimer = Time.time + reHookDelay;
				hookedObject.UnHooked();
				hookedObject = null;
			}
		}
		else
		{
			FlyingObject flyingObject = collision.gameObject.GetComponent<FlyingObject>();
			if (flyingObject && !flyingObject.IsHooked &&  reHookDelayTimer < Time.time)
			{
				hookedObject = flyingObject;
				flyingObject.GotHooked(GetComponent<Rigidbody2D>());
			}
		}

	}

}
