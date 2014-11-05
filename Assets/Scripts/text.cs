using UnityEngine;
using System.Collections;

public class text : MonoBehaviour {

	public PlayerInfo info;
	public bool isQuit = false;
	public bool isGotIt = false;

	void OnMouseEnter() {
		renderer.material.color = Color.black;
	}

	void OnMouseExit() {
		renderer.material.color = Color.white;
	}

	void OnMouseUp() {
		if (isQuit) {
			Application.Quit();
		} else if (isGotIt) {
			info.Display();
			Application.LoadLevel(2);
		} else {
			Application.LoadLevel(1);
		}
	}

	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}

}
