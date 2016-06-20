using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class playerController : MonoBehaviour {

	Transform transform = null;
	Rigidbody2D rb = null;

	public enum FACEDIRECTION
	{
		FACELEFT = -1,
		FACERIGHT = 1

	};
	public FACEDIRECTION facing = FACEDIRECTION.FACERIGHT;

	string horizontalAxisStr = "Horizontal";
	string jumpAxisStr = "Jump";
	public float jumpPower = 600.0f;

	public float speed = 10.0f;

	// check groundedState
	public CircleCollider2D feetCollider = null;
	bool isGrounded = false;
	public LayerMask groundLayer;

	bool GetGrounded() {
		Vector2 circleColliderCenter = new Vector2 (transform.position.x, transform.position.y) + feetCollider.offset;
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (circleColliderCenter, feetCollider.radius, groundLayer);
		if (hitColliders.Length > 0) {
			return true;
		} else {
			return false;
		}
	}

	void Awake() {
		transform = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		// read input using CrossPlatformInputManager not by just using Input class
		float horizontal = CrossPlatformInputManager.GetAxis(horizontalAxisStr);
		Vector2 force = Vector2.right * horizontal * speed;
		// print ("force: " + force);
		rb.AddForce (force);

		if (CrossPlatformInputManager.GetButton (jumpAxisStr)) {
			Jump ();
		}

		// print speed
		// print("speed: " + rb.velocity);

		// clamp velocity
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), Mathf.Clamp(rb.velocity.y, -speed, speed));

		// print clamped speed
		// print("clamped speed: " + rb.velocity);
		if ((horizontal > 0 && facing != FACEDIRECTION.FACERIGHT) || (horizontal < 0 && facing != FACEDIRECTION.FACELEFT)) {
			FlipDirection ();
		}


	}

	void Jump() {
		rb.AddForce (Vector2.up * jumpPower);
	}

	void FlipDirection() {
		facing = (FACEDIRECTION) ((int)facing * -1);
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
