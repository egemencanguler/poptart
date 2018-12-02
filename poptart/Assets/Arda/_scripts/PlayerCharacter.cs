using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action {
	move,
	place,
	wait
}

public class PlayerCharacter : Unit {

	static public int userCount = 0;

	[SerializeField]
	private PrefabList prefabList;

	private int id;

	public Color[ ] dogColor;

	public SpriteRenderer renderer;

	void Start ( ) {
		//Listen for action event
		id = userCount++;

		renderer.sprite = Board.Instance.dogSprites[id];

		CommandPanel.SendCommand += Handle_Command;
	}

	public override void Turn ( ) {
		if (HasMoved)
			return;

		HasMoved = true;

		if (direction != Direction.none)
			Move ( );

	}

	public void Move ( ) {
		Debug.Log ("id:" + id + "\t" + "dir:" + direction);

		GridTile nextTile = Board.Instance.GetNeighbour (tile, direction);

		if (nextTile == null)
			return;

		if (!nextTile.Empty) {
			Unit unit = nextTile.Unit;

			if (unit is LaserPart) {
				((LaserPart) unit).Destruct ( );
				Die ( );
				return;
			} else if (unit.HasMoved) {
				if (unit is BadDog)
					Die ( );
				return;
			} else {
				if (unit is BadDog || unit is PlayerCharacter) {
					unit.Turn ( );
					Move ( );
				} else if (unit is Finish) {
					Die ( );
				}

				return;
			}
		}

		tile.Unit = null;
		tile = nextTile;
		tile.Unit = this;

		transform.position = tile.transform.position;

		direction = Direction.none;
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
	private void Handle_Command (Command command) {
		direction = (Direction) command.GetDirection (id);
	}

	public override void Init (string[ ] args) { }
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