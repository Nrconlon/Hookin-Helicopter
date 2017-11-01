using UnityEngine;

public class Rope : MonoBehaviour {

	public Rigidbody2D helicopterRB;
	public Transform helicopterRopeAnchorPoint;
	public GameObject hookPrefab;
	public GameObject linkPrefab;

	public float ropeLength = 2.1f;

	[SerializeField] int numLinks = 7;
	[SerializeField] float linkMass = 100;

	private Hook hook;

	void Start () {
		GenerateRope();
	}

	void GenerateRope ()
	{
		float linkDist = ropeLength / numLinks;
		Rigidbody2D prevRB = helicopterRB;

		for (int i = 0; i < numLinks-1; i++)
		{
			bool isFirstLink = i == 0;
			prevRB = AppendObjectToRope(linkPrefab, prevRB, i, linkDist, isFirstLink);
		}
		//last link is hook
		prevRB = AppendObjectToRope(hookPrefab, prevRB, numLinks, linkDist, false);
		hook = prevRB.gameObject.GetComponent<Hook> ();
		hook.SetRope (this);
	}

	Rigidbody2D AppendObjectToRope(GameObject objPrefab, Rigidbody2D prevLinkRB, int linkNum, float linkDist, bool isFirstLink) {

		GameObject link = (GameObject)GameObject.Instantiate (objPrefab, this.transform.parent);

		Rigidbody2D rb = link.GetComponent<Rigidbody2D> ();
		rb.mass = linkMass;

		DistanceJoint2D dJoint = link.GetComponent<DistanceJoint2D>();
		dJoint.connectedBody = helicopterRB;
		dJoint.connectedAnchor = helicopterRopeAnchorPoint.localPosition;	//.anchor is where the anchor is on the link, connectedAnchor is where the anchor is on the helicopter RELATIVE TO THE HELICOPTER
		//.anchor should be configured on the link prefab itself.
		dJoint.autoConfigureDistance = false;
		dJoint.distance = (1+linkNum) * linkDist;
		dJoint.maxDistanceOnly = true;

		HingeJoint2D hJoint = link.GetComponent<HingeJoint2D> ();
		hJoint.connectedBody = prevLinkRB;
		hJoint.autoConfigureConnectedAnchor = false;
		if (isFirstLink) {
			//hJoint anchor and connected should be in the same spot
			hJoint.anchor = new Vector2 (0, linkDist);
			hJoint.connectedAnchor = helicopterRopeAnchorPoint.localPosition;
		} else {
			//hJoint anchor and connected should be in the same spot
			hJoint.anchor = new Vector2 (0, linkDist);	//Assumes that the rope is being generated in a straight line (x = 0)
			hJoint.connectedAnchor = Vector2.zero;	//assumes prev link's joint is in the middle of prev link
		}

		return rb;
	}

}
