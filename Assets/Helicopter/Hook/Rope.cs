using UnityEngine;

public class Rope : MonoBehaviour {

	public Rigidbody2D helicoper;

	public GameObject linkPrefab;

	public Hook hook;

	public int links = 7;

	void Start () {
		GenerateRope();
	}

	void GenerateRope ()
	{
		Rigidbody2D previousRB = helicoper;
		for (int i = 0; i < links; i++)
		{
			GameObject link = Instantiate(linkPrefab, transform);
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousRB;
			if(i==0)
			{
				joint.connectedAnchor = Vector2.zero;
			}

			if (i < links - 1)
			{
				previousRB = link.GetComponent<Rigidbody2D>();
			} else
			{
				hook.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
			}

			
		}
	}

}
