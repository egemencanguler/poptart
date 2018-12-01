using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {
	move,
	place,
	wait
}

public class PlayerCharacter : Unit {

	[SerializeField]
	private PrefabList prefabList;

	private int id;

	void Start ( ) {
		//Listen for action event
	}

	public override void Turn ( ) {
		if (direction != Direction.none)
			Move ( );

	}

	private void Move ( ) {
		GridTile nextTile = Board.Instance.GetNeighbour (tile, direction);

		if (nextTile == null)
			return;

		if (!nextTile.Empty) {
			Unit unit = nextTile.Unit;
			if (unit is BadDog) {
				Die ( );
			}

			return;
		}

		tile.Unit = null;
		tile = nextTile;
		tile.Unit = this;

		transform.position = tile.transform.position;
	}

	public void Die ( ) {
		tile.Unit = null;

		//unsubscribe to events

		Destroy (gameObject);
	}

	/*
		private void Place ( ) {
			GridTile nextTile = Board.Instance.GetNeighbour (tile, direction);

			if (nextTile == null || !nextTile.isMovable ( ))
				return;

			GameObject dog = prefabList.GetPrefab ((int) dogToPlace);
		}
	*/
	private void Handle_Action (int id, Direction direction) {
		if (this.id == id) {
			this.direction = direction;
		}
	}
	/*
		private void Handle_Move (Direction direction) {
			this.action = Action.move;
			this.direction = direction;
		}

		private void Handle_Place (DogType dogToPlace) {
				this.action = Action.place;
				this.dogToPlace = dogToPlace;
		} */
}