using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelicopterController : MonoBehaviour {
	PlayerHelicopter myHelicopter;
	// Use this for initialization
	void Start () {
		myHelicopter = GetComponent<PlayerHelicopter>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
		{

			Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)); //z gets overwritten. use myHelicopter.transform.position.z - Camera.main.transform.position if you want it to equal 0 anyways

			Vector3 dir = target - myHelicopter.transform.position;
			dir.z = 0;

			bool inRange = dir.magnitude <= myHelicopter.speed * Time.deltaTime;

			if (inRange) {
				myHelicopter.transform.position = new Vector3(target.x, target.y, myHelicopter.transform.position.z);
			} else {
				myHelicopter.transform.Translate (dir.normalized * myHelicopter.speed * Time.deltaTime);
			}



		}
	}
}
