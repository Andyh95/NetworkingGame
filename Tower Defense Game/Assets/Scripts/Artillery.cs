using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Artillery : NetworkBehaviour {
	Vector3 startPos;
	int player;
	bool isMoving = true;
	int health = 4;

	public GameObject pelletPrefab;
	public Transform shootPoint;
	public GameObject pellet;
	public GameObject pellet2;
	public float shootCD1;
	public float shootCD2;

	// Use this for initialization
	void Start () {

		startPos = transform.position;
		if (startPos.x > 0) {
			player = 1;
		} else {
			player = 2;
		}
	}
	// Update is called once per frame
	[ClientCallback]
	void Update () {
		if(player == 1 && isMoving)
			transform.position += new Vector3 ( -2 * Time.deltaTime, 0, 0);
		else if(isMoving)
			transform.position += new Vector3 ( 2 * Time.deltaTime, 0, 0);
		
		if (health == 0) {
			Destroy(this.gameObject);
		}
		
	}

	void OnTriggerEnter(BoxCollider coll)
	{
		Debug.Log ("Collision with : " + coll);
		if (coll.name != "Soldier(Clone)(Clone)")
			//Destroy (this.gameObject);
		if (coll.name == "Soldier2(Clone)") {
			
			if(player == 1)
			{
				transform.position += new Vector3 (1,0,0);
			}else{
				transform.position += new Vector3(-1,0,0);
			}
			if(this.name != "Soldier2(Clone)")
				health--;
		}
		
		if (coll.name == "Soldier(Clone)") {
			
			if(player == 1)
			{
				transform.position += new Vector3 (1,0,0);
			}else{
				transform.position += new Vector3(-1,0,0);
			}
			if(this.name != "Soldier(Clone)")
				health--;
		}
	}

	void OnTriggerEnter(SphereCollider coll)
	{
		Debug.Log ("in shooting range of : " + coll + " Stopping movement");

		isMoving = false;

		//shoot
		if (player == 1 && coll.name == "Soldier2(Clone)" && shootCD1 <= 0) {
			pellet = (GameObject)Instantiate (pelletPrefab, shootPoint.position, Quaternion.identity);
			NetworkServer.Spawn (pellet);
			pellet.GetComponent<Pellet> ().target = coll.transform;
			shootCD1 = 100f;
		} else if (player == 2 && coll.name == "Soldier(Clone)" && shootCD2 <= 0) {
			pellet = (GameObject)Instantiate (pelletPrefab, shootPoint.position, Quaternion.identity);
			NetworkServer.Spawn (pellet);
			pellet.GetComponent<Pellet> ().target = coll.transform;
			shootCD2 = 100f;
		}

			
		
		}
	}



