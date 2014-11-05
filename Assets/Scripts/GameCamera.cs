using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public Player player1;
	public Player player2;
	public PlayerInfo info;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!player1.dead && !player2.dead) {
			transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) * 0.4f, (player1.transform.position.y + player2.transform.position.y) * 0.4f,
				-20);
			camera.orthographicSize = 0.7f*CameraSize(player1, player2);
		}
	}

	private float CameraSize (Player player1, Player player2) {
		float distance = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
		if (distance > 40) {
			return distance / 2;
		} else {
			return 20;
		}
	}

	public void PlayerDied(GameObject player) {
		if (player.layer == 8) {
			transform.LookAt(player1.transform);
			info.Win(2);
		} else if (player.layer == 9) {
			transform.LookAt(player2.transform);
			info.Win(1);
		}
	}

	void OnGUI() {
		if (GUI.Button(new Rect(10, Screen.height - 60, 100, 50), "Quit")) {
			Application.Quit();
		}
	}
}
