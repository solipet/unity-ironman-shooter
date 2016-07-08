using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class IronmanBehaviorScript : MonoBehaviour {

	public float speed = 16.0f;

	Vector3 movement;
	Rigidbody playerRigidBody;
	bool isMoving = false;
	Animator anim;
	int floorMask;
	float camRayLength = 2000.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		playerRigidBody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
	}

	void FixedUpdate() {
		// we need to get hold of which keys (input) has been used by the Player
		float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		float v = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		Move (h, v);
		if (h != 0 || v != 0) {
			isMoving = true;
		} else {
			isMoving = false;
		}
		Animating ();
		Turning ();
	}

	void Move( float h, float v) {
		movement.Set (-v, 0f, h);
		movement = movement.normalized * Time.fixedDeltaTime * speed;
		playerRigidBody.MovePosition (transform.position + movement);
	}

	void Animating() {
		// if the character is moving, then play the walking animation
		// if not, go to idle animation
		if (isMoving) {
			anim.SetFloat ("speed", 1);
		} else {
			anim.SetFloat ("speed", 0);
		}
	}

	void Turning() {
		// 1) get to know where the mouse is located at
		// if it is in range, then rotate the character towards the mouse
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0.0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			//playerRigidBody.rotation = newRotation;
			playerRigidBody.MoveRotation (newRotation);
		}
	}
}
