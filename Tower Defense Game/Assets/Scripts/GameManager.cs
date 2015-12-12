using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool firstPlayer = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(GameObject.Find("Soldier(Clone)(Clone)"));
	}
}
