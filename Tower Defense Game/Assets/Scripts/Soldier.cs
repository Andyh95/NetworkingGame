using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Soldier : NetworkBehaviour {
	Vector3 startPos;
	int player;
	bool isMoving = true;
	int health = 4;
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

	void OnTriggerEnter(Collider coll)
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

}
