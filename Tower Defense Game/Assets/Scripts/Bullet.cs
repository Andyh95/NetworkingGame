using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 10f;
	public Transform target;
	public Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if (target) {
			Vector3 dir = target.position - transform.position;
			rigidbody.velocity = dir.normalized * speed;
		} else {
			Destroy(this.gameObject);
		}
	}
}
