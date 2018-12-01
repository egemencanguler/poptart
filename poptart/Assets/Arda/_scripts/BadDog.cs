using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadDog : Unit {

	[SerializeField]
	private int interval;
	private int currentProcess;

	public override void Turn ( ) {
		Move ( );

		Act ( );

		SwitchDirection ( );
	}

	private void SwitchDirection ( ) {
		currentProcess = (currentProcess + 1) % interval;
		if (currentProcess == 0) {
			switch (direction) {
			case Direction.up:
				direction = Direction.down;
				break;
			case Direction.right:
				direction = Direction.left;
				break;
			case Direction.down:
				direction = Direction.up;
				break;
			case Direction.left:
				direction = Direction.right;
				break;
			}
		}
	}

	private void Move ( ) {
		GridTile nextTile = Board.Instance.GetNeighbour (tile, direction);

		if (nextTile == null || !nextTile.isMovable ( ))
			return;

		tile.Unit = null;
		tile = nextTile;
		tile.Unit = this;
	}

	private void Act ( ) {

	}
}