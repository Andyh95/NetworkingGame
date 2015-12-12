using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	bool created = true;	
	GameManager manager;
	public GameObject soldierPrefab;
	GameObject soldier;
	public Transform spawnpoint;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Network Manager").GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () {

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

		if (Input.GetKeyDown(KeyCode.Space)) {
			CmdSpawnSoldier();

		}



	}
	[Command]
	public void CmdSpawnSoldier()
	{
		soldier = (GameObject)Instantiate(soldierPrefab,spawnpoint.position,Quaternion.identity);
		NetworkServer.Spawn (soldier);
	}
}
