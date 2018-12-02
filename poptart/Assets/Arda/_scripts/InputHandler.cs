using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	public BoardLoader loader;

	public TextAsset[ ] levels;

	int currentMap = 0;

	void Update ( ) {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			loader.GenerateBoard (levels[0].text);

			currentMap = 0;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			loader.GenerateBoard (levels[1].text);
			currentMap = 1;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			loader.GenerateBoard (levels[2].text);
			currentMap = 2;
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			loader.GenerateBoard (levels[3].text);
			currentMap = 3;
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			loader.GenerateBoard (levels[4].text);
			currentMap = 4;
		} else if (Input.GetKeyDown (KeyCode.R)) {
			loader.GenerateBoard (levels[currentMap].text, false);
		}
	}
}