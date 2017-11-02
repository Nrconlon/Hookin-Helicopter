using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : Enemy {
	float movementSpeed = 3f;
	[SerializeField] GameObject missilePrefab;
	Vector2 targetDirection = Vector2.zero;
	BoxCollider2D myCollider;
	float distanceBeforeShooting = 4f;

	float missileFireTimer = 0f;
	float timeBetweenShots = 5f;


	Plane[] planes;
	// Use this for initialization
	void Start () {
		base.Initialize();
		planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		myCollider = GetComponent<BoxCollider2D>();
	}


	// Update is called once per frame
	void Update () {

		if (targetDirection != Vector2.zero && !isHooked && isActivated)
		{
			transform.position = transform.position + (Vector3) (targetDirection * movementSpeed * Time.deltaTime);
		}



		if (!isHooked)
		{
			float distanceToPlayer = (playerHelicopter.transform.position - transform.position).magnitude;
			if (!GeometryUtility.TestPlanesAABB(planes, myCollider.bounds))
			{
				targetDirection = (Vector3.zero - transform.position).normalized;
			}
			else if (distanceToPlayer <= distanceBeforeShooting)
			{
				if (missileFireTimer < Time.time)
				{
					SpawnMissile();
				}
				targetDirection = Vector2.zero;
			}
			else
			{
				targetDirection = (playerHelicopter.transform.position - transform.position).normalized;
			}
		}

	}

	void SpawnMissile()
	{
		missileFireTimer = Time.time + timeBetweenShots;
		GameObject missile = Instantiate(missilePrefab, transform);
		missile.transform.parent = null;
		Missile myMissile = missile.GetComponent<Missile>();
		myMissile.Initialize();
		myMissile.TurnOnLaunching();

	}
}
