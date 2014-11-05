using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public static string p1Name;
	public static string p2Name;
	private static int p1Score = 0;
	private static int p2Score = 0;
	public static bool displaying = false;
	public static bool won;
	private static string winner;
	private static int bestOf = 1;
	public GUIStyle styleLeft;
	public GUIStyle styleRight;
	public GUIStyle styleMid;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void Display() {
		displaying = true;
		p1Score = 0;
		p2Score = 0;
	}

	public void Win(int player) {
		winner = (player == 1 ? p1Name : p2Name);
		if (player == 1) {
			p1Score++;
		} else {
			p2Score++;
		}
		won = true;
	}

	void OnGUI() {
		if (displaying) {
			GUI.Label(new Rect(10, 10, 100, 50), p1Name, styleLeft);
			GUI.Label(new Rect(Screen.width-110, 10, 100, 50), p2Name, styleRight);
			GUI.Label(new Rect(10, 70, 100, 50), p1Score.ToString(), styleLeft);
			GUI.Label(new Rect(Screen.width-110, 70, 100, 50), p2Score.ToString(), styleRight);
			if (won) {
				if ((p1Score == (bestOf + 1) / 2 || p2Score == (bestOf + 1) / 2)) {
					GUI.Label(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 50, 100, 50), winner + " wins!", styleMid);
					if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100, 50), "Best of " + (bestOf + 2).ToString() + "?")) {
						Application.LoadLevel(2);
						won = false;
						bestOf += 2;
					}
				} else {
					if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100, 50), "Next Round")) {
						Application.LoadLevel(2);
						won = false;
					}
				}
			}
		}
	}
}
