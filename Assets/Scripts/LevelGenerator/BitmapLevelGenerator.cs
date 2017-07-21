
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BitmapLevelGenerator : LevelGenerator {

	public Texture2D testBitmap;

	void Start () {
		Init();
		GenerateLevelFromBitmap (testBitmap);
	}
}