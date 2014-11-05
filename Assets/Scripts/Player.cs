using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float fullReincarnateTime;
	public float reincarnateTime;
	public float speed = 5;
	public float jumpPower = 2f;
	public bool dashing = false;
	public bool dead = false;
	public bool holdingFollower = false;
	private float wait = 0;
	public GameCamera cam;

	public Follower[] allFollowers;

	public KeyCode right;
	public KeyCode left;
	public KeyCode up;
	public KeyCode throwFollower;
	public KeyCode attackLeft;
	public KeyCode attackRight;
	public KeyCode reincarnate;

	// Use this for initialization
	void Start () {
		reincarnateTime = fullReincarnateTime;
		Physics.IgnoreLayerCollision(8, 9);
	}
	
	// Update is called once per frame
	void Update () {
		if (wait == 0 && !dead) {
			//Move normally
			dashing = false;
			if (Input.GetKey (right)) {
				//Moving right
				transform.position += transform.right * speed * Time.deltaTime;
			}
			if (Input.GetKey (left)) {
				//Moving left
				transform.position -= transform.right * speed * Time.deltaTime;
			}
			if (Input.GetKeyDown (up) 
				&& Physics2D.Raycast(transform.position, -Vector2.up, 1.1f, 1 << 10)) {
				//Jump if the capsule is not touching the floor

	            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
	        }
	        if (Input.GetKey (throwFollower)) {
	        	//Throw a follower
	        	Follower follower = GetClosestFollower();
	        	follower.rigidbody2D.isKinematic = false;
	        	follower.transform.position *= 0.95f;
	        	follower.transform.position += 0.05f * (transform.position + 3*Vector3.up);
	        	if (Vector2.Distance(transform.position, follower.transform.position) < 0.5) {
	        		holdingFollower = true;
	        	}
	        }
	        if (holdingFollower && Input.GetKeyUp (throwFollower)) {
	        	Follower follower = GetClosestFollower();
	        	Vector2 force = Vector2.up * 2;
	        	if (Input.GetKey (right)) {force = new Vector2(2, 1);}
	        	else if (Input.GetKey (left)) {force = new Vector2(-2, 1);}

	        	follower.Throw(force * 1000);
	        }
	    	if (Input.GetKeyDown (attackLeft) || Input.GetKeyDown (attackRight)) {
	    		//Dashing left or right
	    		wait = Mathf.Max(wait, 1);
	    		dashing = true;
	    		rigidbody2D.AddForce((Input.GetKeyDown(attackLeft) ? -Vector2.right : Vector2.right) * 2000);
	    	}
	    	if (Input.GetKey (reincarnate)) {
	    		//Reincarnating a follower
	    		reincarnateTime -= Time.deltaTime;
	    		if (reincarnateTime <= 0) {
	    			CompleteReincarnate();
	    		}
	    	} else {
	    		reincarnateTime = fullReincarnateTime;
	    		// Reset reincarnateTime, as the player is not currently using reincarnate
	    	}
	    } else if (!dead) {
	    	//Decrease wait time
	    	wait -= Mathf.Min(Time.deltaTime, wait);
	    } else {
	    	// Dead
	    	if (transform.rotation.eulerAngles.z < 90) {
                transform.Rotate(0, 0, 5);
            }
	    }
	}

	void CompleteReincarnate() {
		foreach (Follower follower in allFollowers) {
			if (Vector2.Distance(follower.transform.position, transform.position) < 3) {
				follower.Revive();
			}
		}
		reincarnateTime = fullReincarnateTime;
	}

	Follower GetClosestFollower() {
		Follower closestFollower = allFollowers[0];
		float minDistance = Mathf.Infinity;

		foreach (Follower follower in allFollowers) {
			float followerDistance = Vector2.Distance(follower.transform.position, transform.position);
			if (followerDistance < minDistance) {
				closestFollower = follower;
				minDistance = followerDistance;
			}
		}

		return closestFollower;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if ((coll.gameObject.layer == (gameObject.layer==8 ? 9 : 8))
			&& coll.gameObject.tag != (gameObject.layer==8 ? "Player2" : "Player1")) {
			
			Follower follower = coll.gameObject.GetComponent<Follower>();

			if (follower.rigidbody2D.velocity.magnitude > 10 && !dashing) {
				dead = true;
				cam.PlayerDied(gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals("Death")) {
			dead = true;
			cam.PlayerDied(gameObject);
		}
	}
}
