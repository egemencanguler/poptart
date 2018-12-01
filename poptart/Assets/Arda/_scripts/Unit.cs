﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	[SerializeField]
	protected Direction direction;

	public GridTile tile;

	abstract public void Turn ( );
}