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
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
		{

			Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			Vector3 targetDirection = (target - myHelicopter.transform.position).normalized * myHelicopter.speed * Time.deltaTime;
			myHelicopter.transform.Translate(Vector3.MoveTowards(myHelicopter.transform.position, target, myHelicopter.speed * Time.deltaTime) - myHelicopter.transform.position);

		}
	}
}
