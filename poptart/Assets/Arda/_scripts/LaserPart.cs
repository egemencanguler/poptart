using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPart : Unit {

	public Laser laser;

	public void Destruct ( ) {
		laser.SetEnd (Board.Instance.boardToWorld (laser.tile.position));
	}

	public override void Init (string[ ] args) { }

	public override void Turn ( ) { }
}