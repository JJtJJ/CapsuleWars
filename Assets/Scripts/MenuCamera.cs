using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {

	public GUIStyle customSkin;
	public GameObject p1Area;
	public GameObject p2Area;

	public string p1Name = "Player1";
	public string p2Name = "Player2";

	void OnGUI() {
		p1Name = GUI.TextArea(new Rect(0.5f * Screen.width, 0.34f * Screen.height, 200, 50), p1Name, 16, customSkin);
		p2Name = GUI.TextArea(new Rect(0.5f * Screen.width, 0.43f * Screen.height, 200, 50), p2Name, 16, customSkin);
		PlayerInfo.p1Name = p1Name;
		PlayerInfo.p2Name = p2Name;
	}

}
