using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
	[SerializeField] Transform[] spawnLocations;
	[SerializeField] GameObject[] enemyPrefabs;

	int prefabIndex = 0;
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
		#region NumberKeys
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Spawn(enemyPrefabs[prefabIndex], 0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Spawn(enemyPrefabs[prefabIndex], 1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Spawn(enemyPrefabs[prefabIndex], 2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Spawn(enemyPrefabs[prefabIndex], 3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Spawn(enemyPrefabs[prefabIndex], 4);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Spawn(enemyPrefabs[prefabIndex], 5);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			Spawn(enemyPrefabs[prefabIndex], 6);
		}
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			Spawn(enemyPrefabs[prefabIndex], 7);
		}
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			Spawn(enemyPrefabs[prefabIndex], 8);
		}
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			Spawn(enemyPrefabs[prefabIndex], 9);
		}
		#endregion

		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (prefabIndex == 0)
			{
				prefabIndex = enemyPrefabs.Length - 1;
			}
			else
			{
				prefabIndex--;
			}
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (prefabIndex == enemyPrefabs.Length - 1)
			{
				prefabIndex = 0;
			}
			else
			{
				prefabIndex++;
			}
		}
	}

	void Spawn(GameObject spawnPrefab)
	{
		Spawn(spawnPrefab, Random.Range(0, 4));
	}

	void Spawn(GameObject spawnPrefab, int spawnIndex)
	{
		GameObject spawnedObject = Instantiate(spawnPrefab, spawnLocations[spawnIndex]);
		HookableObject hookableObject = spawnedObject.GetComponent<HookableObject>();
		hookableObject.transform.parent = null;
		hookableObject.Initialize();
	}



}
