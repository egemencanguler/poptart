using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	protected Direction direction;

	protected GridTile tile;

	abstract public void Turn ( );
}