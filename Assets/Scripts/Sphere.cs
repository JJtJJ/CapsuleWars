using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float size = (1 - (player.reincarnateTime / player.fullReincarnateTime));
		Vector3 sphereSize = new Vector3(size, size, size) * 6;
		transform.localScale = sphereSize;
	}
}
