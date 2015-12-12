using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Soldier : NetworkBehaviour {
	Vector3 startPos;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () {
		if(startPos.x > 0)
			transform.position += new Vector3 ( -2 * Time.deltaTime, 0, 0);
		else
			transform.position += new Vector3 ( 2 * Time.deltaTime, 0, 0);
	}

}
