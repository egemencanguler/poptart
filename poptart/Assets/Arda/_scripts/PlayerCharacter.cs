using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {
	move,
	place,
	wait
}

public enum DogType {
	basic,
	strong
}

public class PlayerCharacter : Unit {
	private Action action;
	private DogType dogToPlace;

	void Start ( ) {
		InputHandler.onMove += Handle_Move;
		InputHandler.onPlace += Handle_Place;
	}

	public override void Turn ( ) {
		switch (action) {
		case Action.move:
			Move ( );
			break;
		case Action.place:
			Place ( );
			break;
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

	private void Place ( ) {
		//TOOD check if next tile in current direction is empty
	}

	private void Handle_Move (Direction direction) {
		this.action = Action.move;
		this.direction = direction;
	}

	private void Handle_Place (DogType dogToPlace) {
		this.action = Action.place;
		this.dogToPlace = dogToPlace;
	}
}