using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	public BoardLoader loader;

	public TextAsset[ ] levels;

	void Update ( ) {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			loader.GenerateBoard (levels[0].text);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {

			loader.GenerateBoard (levels[1].text);
		} else
		if (Input.GetKeyDown (KeyCode.Alpha3)) {

			loader.GenerateBoard (levels[2].text);
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {

			loader.GenerateBoard (levels[3].text);
		}
	}
}