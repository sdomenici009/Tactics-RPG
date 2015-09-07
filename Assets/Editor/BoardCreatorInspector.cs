using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor 
{
	public BoardCreator current
	{
		get
		{
			return (BoardCreator)target;
		}
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("New"))
			current.CreateBoard();
		if (GUILayout.Button("Clear"))
			current.Clear();
		if (GUILayout.Button("Update"))
			current.UpdateTiles();
		if (GUILayout.Button("Save"))
			current.Save();
		if (GUILayout.Button("Load"))
			current.Load();


	}
}
