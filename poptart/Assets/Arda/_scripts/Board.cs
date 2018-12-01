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

	[SerializeField]
	private GameObject tilePrefab;

	private Dictionary<Vector2, GridTile> grid;

	static private Board instance;

	static public Board Instance {
		get {
			return instance;
		}
	}

	float cellsize = 3.2f;

	void Awake ( ) {
		if (instance != null) {
			Destroy (this);
			return;
		}

		instance = this;
		grid = new Dictionary<Vector2, GridTile> ( );

		//TODO Generate map
	}

	public void Turn ( ) {
		ArrayList tiles = new ArrayList ( );

		foreach (KeyValuePair<Vector2, GridTile> pair in grid) {
			if (!pair.Value.Empty) {
				tiles.Add (pair.Value);
			}
		}

		for (int i = 0; i < tiles.Count; ++i) {
			((GridTile) tiles[i]).Turn ( );
		}
	}

	public GridTile GetTile (Vector2 position) {
		if (grid.ContainsKey (position))
			return grid[position];
		return null;
	}

	public GridTile GetNeighbour (GridTile tile, Direction direction) {
		Vector2 position = tile.GetNeighbourPosition (direction);

		return GetTile (position);
	}

	public void CreateTile (Vector2 pos, Unit unit = null) {

		GridTile newTile = Instantiate (tilePrefab).GetComponent<GridTile> ( );
		newTile.SetPosition (pos);

		if (unit != null) {
			newTile.Unit = unit;
			unit.tile = newTile;
		}

		grid.Add (pos, newTile);
	}

	public Vector2 boardToWorld (Vector2 pos) {
		float x = cellsize * pos.x + cellsize / 2;
		float y = cellsize * pos.y + cellsize / 2;

		return new Vector2 (x, y);
	}
}