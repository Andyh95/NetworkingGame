using UnityEngine;
using System.Collections;

public class SpinToWin : MonoBehaviour {

	public float xSpin;
	public float ySpin;
	public float zSpin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		xSpin = xSpin * Time.deltaTime;
		ySpin = ySpin * Time.deltaTime;
		zSpin = zSpin * Time.deltaTime;
	*/
		transform.Rotate (xSpin*Time.deltaTime, ySpin*Time.deltaTime, zSpin*Time.deltaTime);
	
	}
}
