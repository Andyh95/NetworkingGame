using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	bool created = true;	
	GameManager manager;
	public GameObject soldierPrefab;
	public GameObject soldierPrefab2;
	GameObject soldier;
	GameObject soldier2;
	public Transform spawnpoint;
	 float spawnCooldown;
	int health = 10;
	Vector3 startPos;
	int player;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Network Manager").GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () {
		startPos = transform.position;
		if (startPos.x > 0) {
			player = 1;
		} else {
			player = 2;
		}
		if (created) {
			created = false;
			if(manager.firstPlayer){
				transform.position = new Vector3 (22, 4, 0);
				manager.firstPlayer = false;
			}
			else if(!manager.firstPlayer)
			{
				transform.position = new Vector3(-16,4,0);
				transform.Rotate(new Vector3(0,180,0));
			}

		}

		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space) && spawnCooldown <= 0) {
			CmdSpawnSoldier ();
		} else {
			spawnCooldown--;
		}

		if (health == 0) {
			Destroy(this.gameObject);
		}


	}
	[Command]
	public void CmdSpawnSoldier()
	{
		if (player == 1) {
			soldier = (GameObject)Instantiate (soldierPrefab, spawnpoint.position, Quaternion.identity);
			NetworkServer.Spawn (soldier);
			spawnCooldown += 200f;
		} else {
			soldier2 = (GameObject)Instantiate (soldierPrefab2, spawnpoint.position, Quaternion.identity);
			NetworkServer.Spawn (soldier2);
			spawnCooldown += 200f;
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (player == 1) {
			if(coll.name == "Soldier2(Clone)")
			{
				health--;
				Destroy(coll.gameObject);
			}
		}
		if (player == 2) {
			if(coll.name == "Soldier1(Clone)")
			{
				health--;
				Destroy(coll.gameObject);
			}
		}

	}
}
