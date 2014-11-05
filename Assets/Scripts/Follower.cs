using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public GameObject leader;
	public Player enemyPlayer;
	public float followDistance;
	public enum States {Following, Dragged, Held, Thrown};
	public bool thrown;
	private bool farFromLeader;
	public bool dead = false;
	private bool reviving = false;

	// Use this for initialization
	void Start () {
	
		States state = States.Following;
	}
	
	// Update is called once per frame
	void Update () {
		if (!thrown) {
			if (Vector2.Distance(transform.position, leader.transform.position) > followDistance) {
				rigidbody2D.isKinematic = true;
				transform.position *= 0.9f;
				transform.position += 0.1f*leader.transform.position;
			} else {
				rigidbody2D.isKinematic = false;
			}
		} else if (dead) {
            if (transform.rotation.eulerAngles.z < 90) {
                transform.Rotate(0, 0, Mathf.Min(90 - transform.rotation.eulerAngles.z, 5));
            }
		} else if (reviving) {
			if (transform.rotation.eulerAngles.z > 0) {
				transform.Rotate(0, 0, -Mathf.Min(transform.rotation.eulerAngles.z, 5));
			} else {
				reviving = false;
			}
		} else {
			if (Vector2.Distance(transform.position, leader.transform.position) < 3 && farFromLeader) {
				thrown = false;
				farFromLeader = false;
				rigidbody2D.isKinematic = true;
			}
		}
		if (Vector2.Distance(transform.position, leader.transform.position) > 5) {
			farFromLeader = true;
		}
	}

	public void Throw(Vector2 force) {
		rigidbody2D.isKinematic = false;
		thrown = true;
		rigidbody2D.AddForce(force);
	}

	public void Revive() {
		dead = false;
		gameObject.layer = leader.layer;
		reviving = true;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.layer == (gameObject.layer == 8 ? 9 : 8)) {
			if (thrown) {
				rigidbody2D.AddForce((-coll.gameObject.rigidbody2D.velocity) * 10);
			} else {
				Throw((coll.gameObject.rigidbody2D.velocity) * 100);
			}
		}
		if (coll.gameObject.tag.Equals(gameObject.layer == 8 ? "Player2" : "Player1")) {
			if (enemyPlayer.dashing) {
				dead = true;
				gameObject.layer = 11;
			}
		}
	}
}
