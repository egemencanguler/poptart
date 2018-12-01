using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadDog : Unit {

	[SerializeField]
	private int interval;
	private int currentProcess;

	private Direction direction;

	public override void Turn ( ) {
		Move ( );

		SwitchDirection ( );

		Act ( );
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
		switch (direction) {
		case Direction.up:
			//TOOD check if next tile in current direction is empty
			break;
		case Direction.right:
			break;
		case Direction.down:
			break;
		case Direction.left:
			break;
		}
	}

	private void Act ( ) {

	}
}