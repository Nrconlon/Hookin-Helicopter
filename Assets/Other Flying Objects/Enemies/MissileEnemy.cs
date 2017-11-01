using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : Enemy {
	float movementSpeed = 3f;
	float distanceBeforeShooting = 4f;
	[SerializeField] GameObject missilePrefab;
	Vector2 targetDirection = Vector2.zero;
	bool launchingMissile = false;
	BoxCollider2D myCollider;
	Missile myMissile;

	Plane[] planes;
	// Use this for initialization
	void Start () {
		base.Initialize();
		planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		myCollider = GetComponent<BoxCollider2D>();
	}

	private void FixedUpdate()
	{
		if(targetDirection != Vector2.zero && !isHooked && isActivated)
		{
			transform.position = transform.position + V2toV3(targetDirection * movementSpeed * 0.02f);
		}


	}


	// Update is called once per frame
	void Update () {  
		if(!isHooked)
		{
			float distanceToPlayer = (playerHelicopter.transform.position - transform.position).magnitude;
			if (GeometryUtility.TestPlanesAABB(planes, myCollider.bounds) && distanceToPlayer <= distanceBeforeShooting)
			{
				SpawnMissile();
				targetDirection = Vector2.zero;
			}
			else
			{
				targetDirection = (Vector3.zero - transform.position).normalized;
			}
		}
		else
		{
			targetDirection = Vector2.zero;
		}
	}

	void SpawnMissile()
	{
		GameObject missile = Instantiate(missilePrefab, transform);

	}


}
