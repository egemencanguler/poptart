using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	public static Action<Direction> onMove;
	public static Action<DogType> onPlace;

	// Update is called once per frame
	void Update ( ) {
		if (Input.GetKeyUp ("UpArrow")) {
			onMove (Direction.up);
			Board.Instance.Turn ( );
		} else if (Input.GetKeyUp ("DownArrow")) {
			onMove (Direction.down);
			Board.Instance.Turn ( );
		} else if (Input.GetKeyUp ("LeftArrow")) {
			onMove (Direction.left);
			Board.Instance.Turn ( );
		} else if (Input.GetKeyUp ("RightArrow")) {
			onMove (Direction.right);
			Board.Instance.Turn ( );
		} else if (Input.GetKeyUp ("Space")) {
			onMove (Direction.none);
			Board.Instance.Turn ( );
		} else if (Input.GetKeyUp ("1")) {
			onPlace (DogType.basic);
			Board.Instance.Turn ( );
		} else if (Input.GetKeyUp ("2")) {
			onPlace (DogType.strong);
			Board.Instance.Turn ( );
		}

	}
}