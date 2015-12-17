using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	bool created = true;	
	GameManager manager;
	public GameObject soldierPrefab;
	public GameObject soldierPrefab2;
	public GameObject bulletPrefab;
	GameObject soldier;
	GameObject soldier2;
	public Transform spawnpoint;
	public Transform shootPoint;
	float spawnCooldown, spawnCooldown2;
	int health = 10;
	Vector3 startPos;

	public float fireCooldown1;
	public float fireCooldown2;

	public int player;

	public float money = 500;
	GameObject bullet;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Network Manager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () {
		money += 4 * Time.deltaTime;
		fireCooldown1--;
		fireCooldown2--;
        spawnCooldown--;
        spawnCooldown2--;
        //Debug.Log ("money: " + money);
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

		if (player == 1 && GameObject.Find ("Soldier2(Clone)").GetComponent<Soldier> ().transform.position.x > 19) {
			Destroy(this.gameObject);
		}
		if (player == 2 && GameObject.Find ("Soldier(Clone)").GetComponent<Soldier> ().transform.position.x < -14) {
			Destroy(this.gameObject);
		}

		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)  && money >= 50) {
			CmdSpawnSoldier ();
			money -= 50;
		}

		

		if (health == 0) {
			Destroy(this.gameObject);
		}



	}
	[Command]
	public void CmdSpawnSoldier()
	{
		if (player == 1 && spawnCooldown <= 0) {
			soldier = (GameObject)Instantiate (soldierPrefab, spawnpoint.position, Quaternion.identity);
			NetworkServer.Spawn (soldier);
			spawnCooldown = 100f;

		}
        if (player == 2 && spawnCooldown2 <=0) {
			soldier2 = (GameObject)Instantiate (soldierPrefab2, spawnpoint.position, Quaternion.identity);
			NetworkServer.Spawn (soldier2);
			spawnCooldown2 = 100f;

		}
	}

	/*void OnTriggerEnter(Collider coll)
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
*/

	void OnTriggerEnter(Collider coll)
	{
		if (player == 1 && coll.name == "Soldier2(Clone)" && fireCooldown1 <= 0) {
			bullet = (GameObject)Instantiate (bulletPrefab, shootPoint.position, Quaternion.identity);
			NetworkServer.Spawn (bullet);
			bullet.GetComponent<Bullet> ().target = coll.transform;
			fireCooldown1 = 100f;
		} else if (player == 2 && coll.name == "Soldier(Clone)" && fireCooldown2 <= 0) {
			bullet = (GameObject)Instantiate (bulletPrefab, shootPoint.position, Quaternion.identity);
			NetworkServer.Spawn (bullet);
			bullet.GetComponent<Bullet> ().target = coll.transform;
			fireCooldown2 = 100f;
		}

	}

}

