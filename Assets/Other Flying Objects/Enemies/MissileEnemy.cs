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

	float missilePrepareTime = 2f;
	float missileFireTimer = 0f;
	float timeBetweenShots = 5f;

	float missileDownPrepareSpeed = 1f;

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

		if (launchingMissile)
		{
			myMissile.transform.position = myMissile.transform.position + Vector3.down * missileDownPrepareSpeed * Time.deltaTime;
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
		print(transform.position);
		GameObject missile = Instantiate(missilePrefab, transform);
		missile.transform.parent = null;
		myMissile = missile.GetComponent<Missile>();
		myMissile.Initialize();
		myMissile.DeActivate();
		launchingMissile = true;
		StartCoroutine(ActivateMissile());

	}


	IEnumerator ActivateMissile()
	{
		yield return new WaitForSeconds(missilePrepareTime);
		launchingMissile = false;
		myMissile.flyDirection = (playerHelicopter.transform.position - myMissile.transform.position).normalized;
		myMissile.Activate();
	}


}
