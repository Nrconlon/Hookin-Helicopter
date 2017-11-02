using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelicopterController : MonoBehaviour {
	PlayerHelicopter myHelicopter;
	Rigidbody2D myRigidBody;
	// Use this for initialization
	void Start () {
		myHelicopter = GetComponent<PlayerHelicopter>();
		myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
		{

			Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)); //z gets overwritten. use myHelicopter.transform.position.z - Camera.main.transform.position if you want it to equal 0 anyways

			Vector3 dir = target - myHelicopter.transform.position;
			dir.z = 0;

			bool inRange = dir.magnitude <= myHelicopter.speed * Time.deltaTime;



			Vector2 finalPos;

			if (inRange)
			{
				finalPos = new Vector3(target.x, target.y, myHelicopter.transform.position.z);
			}
			else
			{
				finalPos = transform.position + (dir.normalized * myHelicopter.speed * Time.deltaTime);
			}

			myRigidBody.MovePosition(finalPos);
		}
	}
}
