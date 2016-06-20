using UnityEngine;
using System.Collections;

public class pingPongMotion : MonoBehaviour {

	Transform transform = null;
	Vector3 initialPosition = Vector3.zero;

	public Vector3 moveAxes = Vector3.zero;
	public float distance = 3.0f;
	public float speed = 10.0f;

	void Awake() {
		transform = GetComponent<Transform> ();
		initialPosition = transform.position;
	}

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * Mathf.PingPong(t, length)
		 * ping pong the value between 0 and length
		 */
		transform.position = initialPosition + moveAxes * Mathf.PingPong (Time.time * speed, distance);
	
	}
}
