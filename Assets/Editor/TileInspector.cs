using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Tile)), CanEditMultipleObjects]
public class TileInspector : Editor 
{
	public Tile current
	{
		get
		{
			return (Tile)target;
		}
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		foreach(object obj in targets)
		{
			Tile tile = (Tile)obj;

			if(tile != null)
			{
				tile.Match();
			}
		}
	}
}
