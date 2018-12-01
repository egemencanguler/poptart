using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadDog : Unit {

	[SerializeField]
	private int interval;
	private int currentProcess;

	public override void Turn ( ) {
		Move ( );

		SwitchDirection ( );
	}

	private void SwitchDirection ( ) {
		if (currentProcess == interval) {
			currentProcess = 0;

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

		if (nextTile == null)
			return;

		if (!nextTile.Empty) {
			Unit unit = nextTile.Unit;
			if (unit is PlayerCharacter) {
				((PlayerCharacter) unit).Die ( );
			} else return;
		}

		++currentProcess;

		tile.Unit = null;
		tile = nextTile;
		tile.Unit = this;

		transform.position = tile.transform.position;
	}

	public override void Init (string[ ] args) {
		direction = (Direction) int.Parse (args[1]);
		interval = int.Parse (args[2]);
		currentProcess = int.Parse (args[3]);
	}
}