using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	none,
	up,
	right,
	down,
	left
}

public class Board : MonoBehaviour {

	private Dictionary<Vector2, GridTile> grid;

	static private Board instance;

	static public Board Instance {
		get {
			return instance;
		}
	}

	void Start ( ) {
		if (instance != null) {
			Destroy (this);
			return;
		}

		instance = this;

		//TODO Generate map
	}

	public void Turn ( ) {
		foreach (KeyValuePair<Vector2, GridTile> pair in grid) {
			if (!pair.Value.Empty) {
				pair.Value.Turn ( );
			}
		}
	}
}